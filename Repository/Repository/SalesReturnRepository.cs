using DataLayer.DBContext;
using DataLayer.Tables;
using Dto.Dto;
using IRepository.IRepository;
using Repository.Static_Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class SalesReturnRepository : ISalesReturnRepository
    {
        private readonly AccountDbContext db;
        public SalesReturnRepository(AccountDbContext _db)
        {
            db = _db;
        }
        public Response<int> GetMaxReturnSalesCode()
        {
          int numOfRows= db.tblSalesReturn.ToList().Count();
            Response<int> response = new Response<int>();
            if (numOfRows !=0)
            {
                response.code = Static_Data.StaticApiStatus.ApiSuccess.Code;
                response.status = Static_Data.StaticApiStatus.ApiSuccess.Status;
                response.message = Static_Data.StaticApiStatus.ApiSuccess.MessageAr;
                response.payload = db.tblSalesReturn.Max(s => s.sale_return_inv_code); ;
            }
            else
            {
                response.code = Static_Data.StaticApiStatus.ApiSuccess.Code;
                response.status = Static_Data.StaticApiStatus.ApiSuccess.Status;
                response.message = Static_Data.StaticApiStatus.ApiSuccess.MessageAr;
                response.payload = 0;
            }
           
            return response;
        }
        //return the total of products 
        public Response<dtoInvoiceNumbers> GetTotalAfterDiscounts(decimal total, decimal discountNum, decimal discountPer, decimal tax, decimal DiscountTax)
        {
            decimal finalTotal = 0;
            decimal taxAmount = 0;
            decimal discountAmount = 0;
            decimal taxDiscountAmount = 0;
            //حساب نسبه الخصم من قيمه الاصناف
            if ((discountPer != 0 && discountNum != 0) || (discountPer != 0 && discountNum == 0))
            {
                discountPer = discountPer / 100;
                discountAmount = (total * discountPer);
                finalTotal = total - discountAmount;
            }
            else if (discountPer == 0 && discountNum != 0)
            {
                discountAmount = discountNum;
                finalTotal = total - discountNum;
            }
            else
            {
                finalTotal = total;
            }
            //حساب قيمه الضريبه ق.م بعد الخصم
            tax = tax / 100;
            taxAmount = (tax * finalTotal);
            //حساب ضريبه الخصم
            DiscountTax = DiscountTax / 100;
            taxDiscountAmount = (DiscountTax * finalTotal);
            finalTotal = finalTotal + taxAmount;
            finalTotal = finalTotal - taxDiscountAmount;
            Response<dtoInvoiceNumbers> response = new Response<dtoInvoiceNumbers>();
            dtoInvoiceNumbers Numbers = new dtoInvoiceNumbers()
            {
                total = total,
                discount = discountAmount,
                tax = taxAmount,
                tax_discount = taxDiscountAmount,
                final_total = finalTotal,
            };
            response.code = Static_Data.StaticApiStatus.ApiSuccess.Code;
            response.status = Static_Data.StaticApiStatus.ApiSuccess.Status;
            response.message = Static_Data.StaticApiStatus.ApiSuccess.MessageAr;
            response.payload = Numbers;
            return response;
        }

        public Response<dtoSalesReturnInvDetails> AddSalesReturnInvoice(dtoSalesReturnInvDetails dto)
        {
            Response<dtoSalesReturnInvDetails> res = new Response<dtoSalesReturnInvDetails>();
            if (dto != null)
            {
                var availableCode = db.tblSalesReturn.Any(s => s.sale_return_inv_code == dto.sale_return_inv_code);
                if (dto.sale_return_inv_code == 0 || availableCode == true)
                {
                    var code = GetMaxReturnSalesCode().payload;
                    dto.sale_return_inv_code = code + 1;
                }
                using (db.Database.BeginTransaction())
                {
                    try
                    {
                        var sales_return_inv = new TblSalesInvoiceReturn()
                        {
                            total = dto.invoice_return_total,
                            tax = dto.tax,
                            discount = dto.discount,
                            tax_discount = dto.tax_discount,
                            final_total = dto.final_total,
                            store_id = dto.store_id,
                            customer_id = dto.client_id,
                            sale_return_inv_code = dto.sale_return_inv_code,
                            sales_return_Added_Time = DateTime.Now
                        };
                        db.Add(sales_return_inv);
                        db.SaveChanges();
                        dto.id = sales_return_inv.id;
                        for (int i = 0; i < dto.sales_return_invoice_details.Count(); i++)
                        {
                            var row = dto.sales_return_invoice_details[i];
                            var sales_return_inv_details = new TblSalesInvoiceReturnDetails()
                            {
                                product_id = row.product_id,
                                notes = row.notes,
                                sales_price_one_product = row.sales_price_one_product,
                                qty = row.qty,
                                total_sales_price_one_product = row.total_sales_price_one_product,
                                sale_return_inv_code = dto.id
                            };
                            db.Add(sales_return_inv_details);
                            db.SaveChanges();
                            //go increase to store
                            var product_exist = db.tblStoreDetails.Where(s => s.store_id == dto.store_id
                              && s.product_id == row.product_id).FirstOrDefault();

                            if (product_exist == null)
                            {
                                var store_details = new TblStoreDetails()
                                {
                                    product_id = row.product_id,
                                    qty = row.qty,
                                    store_id = dto.store_id
                                };
                                db.Add(store_details);
                                db.SaveChanges();
                            }
                            else
                            {
                                product_exist.qty += row.qty;
                                db.SaveChanges();
                            }
                        }

                        db.Database.CommitTransaction();
                        res.code = Static_Data.SalesIvoices.SuccessSalesInvoice.Code;
                        res.status = Static_Data.SalesIvoices.SuccessSalesInvoice.Status;
                        res.message = Static_Data.SalesIvoices.SuccessSalesInvoice.MessageAr;
                    }
                    catch (Exception ex)
                    {
                        db.Database.RollbackTransaction();
                        res.message = ex.Message.ToString();
                    }
                }
            }
           
            else
            {
                res.code = Static_Data.SalesIvoices.EmptySalesInvoice.Code;
                res.status = Static_Data.SalesIvoices.EmptySalesInvoice.Status;
                res.message = Static_Data.SalesIvoices.EmptySalesInvoice.MessageAr;
            }
            return res;
        }
       
        public Response<bool> Delete(int id)
        {
            Response<bool> response = new Response<bool>();
            if (id != 0)
            {
                var isExist = db.tblSalesReturn.Where(s => s.id == id).FirstOrDefault();
                if (isExist != null)
                {
                    isExist.is_deleted = 1;
                }
                db.SaveChanges();
                var SalesReturnDetails = db.tblSalesReturnDetails.Where(ss => ss.sale_return_inv_code == id).ToList();
                foreach (var item in SalesReturnDetails)
                {
                    item.is_deleted = 1;
                    db.SaveChanges();
                }

                response.code = DeletedData.SuccessDelete.Code;
                response.status = DeletedData.SuccessDelete.Status;
                response.message = DeletedData.SuccessDelete.MessageAr;
            }
            else
            {
                response.code = DeletedData.FailDelete.Code;
                response.status = DeletedData.FailDelete.Status;
                response.message = DeletedData.FailDelete.MessageAr;
            }
            return response;
        
    }

        public Response<List<dtoSalesReturnInvForShow>> FilterSalesReturnInvoices(int customer_id, int store_id, DateTime from_date, DateTime to_date)
        {
            Response<List<dtoSalesReturnInvForShow>> res = new Response<List<dtoSalesReturnInvForShow>>();
            var sales = new List<dtoSalesReturnInvForShow>();
            if (customer_id == 0 && store_id != 0)
            {
                sales = (from s in db.tblSalesReturn
                           .Where(s => s.store_id == store_id)
                           .Where(s => s.sales_return_Added_Time >= from_date && s.sales_return_Added_Time <= to_date)
                         select new dtoSalesReturnInvForShow()
                            {
                                id = s.id,
                                invoice_total = s.total,
                                client_id = s.customer_id,
                                client_name = s.TblCustomer.customer_name,
                                store_id = s.store_id,
                                store_name = s.TblStore.store_name,
                                sales_Added_Time_ar = s.sales_return_Added_Time.ToString("dd dddd , MMMM, yyyy", new CultureInfo("ar-AE")),
                                IsDeleted = s.is_deleted
                            }).ToList();
            }
            else if (customer_id != 0 && store_id == 0)
            {
                sales = (from s in db.tblSalesReturn.Where(s => s.customer_id == customer_id)
                                                    .Where(s => s.sales_return_Added_Time >= from_date && s.sales_return_Added_Time <= to_date)

                         select new dtoSalesReturnInvForShow()
                            {
                                id = s.id,
                                invoice_total = s.total,
                                client_id = s.customer_id,
                                client_name = s.TblCustomer.customer_name,
                                store_id = s.store_id,
                                store_name = s.TblStore.store_name,
                                sales_Added_Time_ar = s.sales_return_Added_Time.ToString("dd dddd , MMMM, yyyy", new CultureInfo("ar-AE")),
                                IsDeleted = s.is_deleted
                            }).ToList();
            }
            else if (customer_id != 0 && store_id != 0)
            {
                sales = (from s in db.tblSalesReturn.Where(s => s.customer_id == customer_id)
                              .Where(s => s.store_id == store_id)
                            .Where(s => s.sales_return_Added_Time >= from_date && s.sales_return_Added_Time <= to_date)
                         select new dtoSalesReturnInvForShow()
                            {
                                id = s.id,
                                invoice_total = s.total,
                                client_id = s.customer_id,
                                client_name = s.TblCustomer.customer_name,
                                store_id = s.store_id,
                                store_name = s.TblStore.store_name,
                                sales_Added_Time_ar = s.sales_return_Added_Time.ToString("dd dddd , MMMM, yyyy", new CultureInfo("ar-AE")),
                                IsDeleted = s.is_deleted
                            }).ToList();
            }else if(customer_id == 0 && store_id == 0)
            {
                sales = (from s in db.tblSalesReturn
                         .Where(s => s.sales_return_Added_Time >= from_date && s.sales_return_Added_Time <= to_date)
                         select new dtoSalesReturnInvForShow()
                         {
                             id = s.id,
                             invoice_total = s.total,
                             client_id = s.customer_id,
                             client_name = s.TblCustomer.customer_name,
                             store_id = s.store_id,
                             store_name = s.TblStore.store_name,
                             sales_Added_Time_ar = s.sales_return_Added_Time.ToString("dd dddd , MMMM, yyyy", new CultureInfo("ar-AE")),
                             IsDeleted = s.is_deleted
                         }).ToList();
            }
            else
            {
                throw new NullReferenceException();
            }

            res.payload = sales;
            res.code = Static_Data.StaticApiStatus.ApiSuccess.Code;
            res.status = Static_Data.StaticApiStatus.ApiSuccess.Status;
            res.message = Static_Data.StaticApiStatus.ApiSuccess.MessageAr;
            return res;
        }

        public Response<List<dtoSalesReturnInvForShow>> ReadSummeryReturnSalesInv()
        {
            Response<List<dtoSalesReturnInvForShow>> res = new Response<List<dtoSalesReturnInvForShow>>();
            var allsales = (from s in db.tblSalesReturn
                               select new dtoSalesReturnInvForShow()
                               {
                                   id = s.id,
                                   invoice_total = s.total,
                                   client_id=s.customer_id,
                                   client_name=s.TblCustomer.customer_name,
                                   store_id=s.store_id,
                                   store_name=s.TblStore.store_name,
                                   sales_Added_Time_ar = s.sales_return_Added_Time.ToString("dd dddd , MMMM, yyyy", new CultureInfo("ar-AE")),
                                   IsDeleted=s.is_deleted
                               }).ToList();
            res.payload = allsales;
             res.code = Static_Data.StaticApiStatus.ApiSuccess.Code;
            res.status = Static_Data.StaticApiStatus.ApiSuccess.Status;
            res.message = Static_Data.StaticApiStatus.ApiSuccess.MessageAr;
            return res;
        }

        public Response<List<dtoSalesReturnInvForShow>> ReadSalesReturnInvWithDetails(int sale_return_inv_id)
        {
            Response<List<dtoSalesReturnInvForShow>> res = new Response<List<dtoSalesReturnInvForShow>>();
            var sales = (from s in db.tblSalesReturn
                            where s.id == sale_return_inv_id
                         join sd in db.tblSalesReturnDetails on s.id equals sd.sale_return_inv_code
                            select new dtoSalesReturnInvForShow()
                            {
                                id = s.id,
                                invoice_total = s.total,
                                discount = s.discount,
                                tax = s.tax,
                                tax_discount = s.tax_discount,
                                final_total = s.final_total,
                                client_id = s.customer_id,
                                client_name = s.TblCustomer.customer_name,
                                store_id = s.store_id,
                                store_name = s.TblStore.store_name,
                                sales_Added_Time_ar = s.sales_return_Added_Time.ToString("dd dddd , MMMM, yyyy", new CultureInfo("ar-AE")),
                                IsDeleted = s.is_deleted,
                                lstProducts=s.SalesInvoiceReturnDetailsLst.Select
                                (s => new dtoSalesReturnDetailsForShow
                                {
                                    id=s.id,
                                   product_id=s.product_id,
                                    product_name = s.TblProduct.product_name,
                                    sales_price_one_product = s.sales_price_one_product,
                                    sales_return__inv_id = s.sale_return_inv_code,
                                    qty = s.qty,
                                    total_sales_price_one_product = s.total_sales_price_one_product,
                                    notes = s.notes,
                                }).ToList()
                            }).ToList();
            
            res.payload = sales;
        res.code = Static_Data.StaticApiStatus.ApiSuccess.Code;
        res.status = Static_Data.StaticApiStatus.ApiSuccess.Status;
        res.message = Static_Data.StaticApiStatus.ApiSuccess.MessageAr;
        return res;
        }
        public List<dtoSalesReturnInvForShow> ReadCustomSalesReturnInvoiceForReport(int sale_return_inv_id)
        {
            var sales = (from s in db.tblSalesReturn
                         where s.id == sale_return_inv_id
                         join sd in db.tblSalesReturnDetails on s.id equals sd.sale_return_inv_code
                         select new dtoSalesReturnInvForShow()
                         {
                             id = s.id,
                             invoice_total = s.total,
                             discount=s.discount,
                             tax=s.tax,
                             tax_discount=s.tax_discount,
                             final_total=s.final_total,
                             client_id = s.customer_id,
                             client_name = s.TblCustomer.customer_name,
                             store_id = s.store_id,
                             store_name = s.TblStore.store_name,
                             sales_Added_Time_ar = s.sales_return_Added_Time.ToString("dd dddd , MMMM, yyyy", new CultureInfo("ar-AE")),
                             IsDeleted = s.is_deleted,
                             lstProducts = s.SalesInvoiceReturnDetailsLst.Select
                             (s => new dtoSalesReturnDetailsForShow
                             {
                                 id = s.id,
                                 product_id = s.product_id,
                                 product_name = s.TblProduct.product_name,
                                 sales_price_one_product = s.sales_price_one_product,
                                 sales_return__inv_id = s.sale_return_inv_code,
                                 qty = s.qty,
                                 total_sales_price_one_product = s.total_sales_price_one_product,
                                 notes = s.notes,
                             }).ToList()
                         }).ToList();
      
            return sales;
        }
        public Response<List<dtoSalesReturnInvForShow>> GetDeletedSalesReturnInvoices()
        {
            Response<List<dtoSalesReturnInvForShow>> res = new Response<List<dtoSalesReturnInvForShow>>();
            var allSales = (from s in db.tblSalesReturn
                               where s.is_deleted == 1
                               select new dtoSalesReturnInvForShow()
                               {
                                   id = s.id,
                                   invoice_total = s.total,
                                   client_id = s.customer_id,
                                   client_name = s.TblCustomer.customer_name,
                                   store_id = s.store_id,
                                   store_name = s.TblStore.store_name,
                                   sales_Added_Time_ar = s.sales_return_Added_Time.ToString("dd dddd , MMMM, yyyy", new CultureInfo("ar-AE")),
                                   IsDeleted = s.is_deleted
                               }).ToList();
            res.payload = allSales;
            res.code = Static_Data.StaticApiStatus.ApiSuccess.Code;
            res.status = Static_Data.StaticApiStatus.ApiSuccess.Status;
            res.message = Static_Data.StaticApiStatus.ApiSuccess.MessageAr;
            return res;
        }

        public Response<List<dtoSalesReturnInvForShow>> GetNotDeletedReturnSalesInvoices()
        {
            Response<List<dtoSalesReturnInvForShow>> res = new Response<List<dtoSalesReturnInvForShow>>();
            var allSales = (from s in db.tblSalesReturn
                               where s.is_deleted == 0
                               select new dtoSalesReturnInvForShow()
                               {
                                   id = s.id,
                                   invoice_total = s.total,
                                   client_id = s.customer_id,
                                   client_name = s.TblCustomer.customer_name,
                                   store_id = s.store_id,
                                   store_name = s.TblStore.store_name,
                                   sales_Added_Time_ar = s.sales_return_Added_Time.ToString("dd dddd , MMMM, yyyy", new CultureInfo("ar-AE")),
                                   IsDeleted = s.is_deleted
                               }).ToList();
            res.payload = allSales;
            res.code = Static_Data.StaticApiStatus.ApiSuccess.Code;
            res.status = Static_Data.StaticApiStatus.ApiSuccess.Status;
            res.message = Static_Data.StaticApiStatus.ApiSuccess.MessageAr;
            return res;
        }
    }
}
