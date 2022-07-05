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
    public class SalesRepository : ISalesRepository
    {
        private readonly AccountDbContext db;
        public SalesRepository(AccountDbContext _db)
        {
            db = _db;
        }
        bool CheckProductsQtyInStore(List<int> products, int store_id)
        {
            bool isValid = true;
            for (int i = 0; i < products.Count; i++)
            {
                var product = db.tblStoreDetails.Where(s => s.product_id == products[i]
                              ).FirstOrDefault();
                if (product.qty == 0 || product.qty < 0)
                {
                    isValid = false;
                }
            }
            return isValid;
        }
        public Tuple<int, int> getMinMaxCode()
        {
            int minCode = db.tblSales.Min(s => s.sale_inv_code);
            int maxCode = db.tblSales.Max(s => s.sale_inv_code);
            Tuple<int, int> Dictionary = new Tuple<int, int>(minCode, maxCode);
            return Dictionary;
        }
        public Response<int> GetMaxCode()
        {
            int numOfRows = db.tblSales.ToList().Count();
            Response<int> response = new Response<int>();
            if (numOfRows != 0)
            {
                response.code = Static_Data.StaticApiStatus.ApiSuccess.Code;
                response.status = Static_Data.StaticApiStatus.ApiSuccess.Status;
                response.message = Static_Data.StaticApiStatus.ApiSuccess.MessageAr;
                response.payload = db.tblSales.Max(s => s.sale_inv_code); ;
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

        public Response<dtoSalesInvDetails> Add(dtoSalesInvDetails dto)
        {
            Response<dtoSalesInvDetails> res = new Response<dtoSalesInvDetails>();
            if (dto != null)
            {
                List<int> products = new List<int>();
                for (int i = 0; i < dto.sales_invoice_details.Count; i++)
                {
                    products.Add(dto.sales_invoice_details[i].product_id);
                }

                bool isValidInv = CheckProductsQtyInStore(products, dto.store_id);

                if (isValidInv == true)
                {
                    var availableCode = db.tblSales.Any(s => s.sale_inv_code == dto.sale_inv_code);
                    if (dto.sale_inv_code == 0 || availableCode == true)
                    {
                        var code = GetMaxCode().payload;
                        dto.sale_inv_code = code + 1;
                    }
                    using (db.Database.BeginTransaction())
                    {
                        try
                        {
                            var sales_inv = new TblSalesInvoice()
                            {
                                total = dto.invoice_total,
                                tax = dto.tax,
                                discount = dto.discount,
                                tax_discount = dto.tax_discount,
                                final_total = dto.final_total,
                                store_id = dto.store_id,
                                customer_id = dto.client_id,
                                sale_inv_code = dto.sale_inv_code,
                                sales_Added_Time = DateTime.Now
                            };
                            db.Add(sales_inv);
                            db.SaveChanges();
                            dto.id = sales_inv.id;
                            for (int i = 0; i < dto.sales_invoice_details.Count(); i++)
                            {
                                var row = dto.sales_invoice_details[i];
                                var sales_inv_details = new TblSalesInvoiceDetails()
                                {
                                    product_id = row.product_id,
                                    notes = row.notes,
                                    sales_price_one_product = row.sales_price_one_product,
                                    qty = row.qty,
                                    total_sales_price_one_product = row.total_sales_price_one_product,
                                    sale_inv_id = dto.id
                                };
                                db.Add(sales_inv_details);
                                db.SaveChanges();
                                //go decrease from store
                                var product_exist = db.tblStoreDetails.Where(s => s.store_id == dto.store_id
                                  && s.product_id == row.product_id).FirstOrDefault();

                                if (product_exist != null)
                                {
                                    product_exist.qty -= row.qty;
                                    db.SaveChanges();
                                }
                                else
                                {
                                    //alert there is no product
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
                    res.code = Static_Data.SalesIvoices.FailSalesInvoice.Code;
                    res.status = Static_Data.SalesIvoices.FailSalesInvoice.Status;
                    res.message = Static_Data.SalesIvoices.FailSalesInvoice.MessageAr;
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
                var isExist = db.tblSales.Where(s => s.id == id).FirstOrDefault();
                if (isExist != null)
                {
                    isExist.is_deleted = 1;
                }
                db.SaveChanges();
                var SalesDetails = db.tblSalesDetails.Where(ss => ss.sale_inv_id == id).ToList();
                foreach (var item in SalesDetails)
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

        public Response<List<dtoSalesInvForShow>> FilterSalesInvoices(int customer_id, int store_id, DateTime from_date, DateTime to_date, int from_code, int to_code, int deleted_val)
        {
            Response<List<dtoSalesInvForShow>> res = new Response<List<dtoSalesInvForShow>>();
            var sales = new List<dtoSalesInvForShow>();
            if (customer_id == 0 && store_id != 0)
            {
                sales = (from s in db.tblSales
                           .Where(s => s.store_id == store_id)
                           .Where(s => s.sales_Added_Time >= from_date && s.sales_Added_Time <= to_date)
                           .Where(s => s.sale_inv_code >= from_code && s.sale_inv_code <= to_code)
                       .Where(s => deleted_val == 2 ? (true) : (s.is_deleted == deleted_val))
                         orderby s.sale_inv_code descending
                         select new dtoSalesInvForShow()
                         {
                             id = s.id,
                             sale_inv_code = s.sale_inv_code,
                             invoice_total = s.total,
                             client_id = s.customer_id,
                             client_name = s.TblCustomer.customer_name,
                             store_id = s.store_id,
                             store_name = s.TblStore.store_name,
                             sales_Added_Time_ar = s.sales_Added_Time.ToString("dd dddd , MMMM, yyyy", new CultureInfo("ar-AE")),
                             IsDeleted = s.is_deleted
                         }).ToList();
            }
            else if (customer_id != 0 && store_id == 0)
            {
                sales = (from s in db.tblSales.Where(s => s.customer_id == customer_id)
                        .Where(s => s.sales_Added_Time >= from_date && s.sales_Added_Time <= to_date)
                       .Where(s => s.sale_inv_code >= from_code && s.sale_inv_code <= to_code)
                       .Where(s => deleted_val == 2 ? (true) : (s.is_deleted == deleted_val))
                         orderby s.sale_inv_code descending
                         select new dtoSalesInvForShow()
                         {
                             id = s.id,
                             sale_inv_code = s.sale_inv_code,
                             invoice_total = s.total,
                             client_id = s.customer_id,
                             client_name = s.TblCustomer.customer_name,
                             store_id = s.store_id,
                             store_name = s.TblStore.store_name,
                             sales_Added_Time_ar = s.sales_Added_Time.ToString("dd dddd , MMMM, yyyy", new CultureInfo("ar-AE")),
                             IsDeleted = s.is_deleted
                         }).ToList();
            }
            else if (customer_id != 0 && store_id != 0)
            {
                sales = (from s in db.tblSales.Where(s => s.customer_id == customer_id)
                              .Where(s => s.store_id == store_id)
                            .Where(s => s.sales_Added_Time >= from_date && s.sales_Added_Time <= to_date)
                             .Where(s => s.sale_inv_code >= from_code && s.sale_inv_code <= to_code)
                       .Where(s => deleted_val == 2 ? (true) : (s.is_deleted == deleted_val))
                         orderby s.sale_inv_code descending
                         select new dtoSalesInvForShow()
                         {
                             id = s.id,
                             sale_inv_code = s.sale_inv_code,
                             invoice_total = s.total,
                             client_id = s.customer_id,
                             client_name = s.TblCustomer.customer_name,
                             store_id = s.store_id,
                             store_name = s.TblStore.store_name,
                             sales_Added_Time_ar = s.sales_Added_Time.ToString("dd dddd , MMMM, yyyy", new CultureInfo("ar-AE")),
                             IsDeleted = s.is_deleted
                         }).ToList();
            } else if (customer_id == 0 && store_id == 0)
            {
                sales = (from s in db.tblSales
                         .Where(s => s.sales_Added_Time >= from_date && s.sales_Added_Time <= to_date)
                          .Where(s => s.sale_inv_code >= from_code && s.sale_inv_code <= to_code)
                       .Where(s => deleted_val == 2 ? (true) : (s.is_deleted == deleted_val))
                         orderby s.sale_inv_code descending
                         select new dtoSalesInvForShow()
                         {
                             id = s.id,
                             sale_inv_code = s.sale_inv_code,
                             invoice_total = s.total,
                             client_id = s.customer_id,
                             client_name = s.TblCustomer.customer_name,
                             store_id = s.store_id,
                             store_name = s.TblStore.store_name,
                             sales_Added_Time_ar = s.sales_Added_Time.ToString("dd dddd , MMMM, yyyy", new CultureInfo("ar-AE")),
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

        public Response<List<dtoSalesInvForShow>> Read()
        {
            Response<List<dtoSalesInvForShow>> res = new Response<List<dtoSalesInvForShow>>();
            var allsales = (from s in db.tblSales
                            orderby s.sale_inv_code descending
                            select new dtoSalesInvForShow()
                            {
                                id = s.id,
                                sale_inv_code=s.sale_inv_code,
                                invoice_total = s.total,
                                client_id = s.customer_id,
                                client_name = s.TblCustomer.customer_name,
                                store_id = s.store_id,
                                store_name = s.TblStore.store_name,
                                sales_Added_Time_ar = s.sales_Added_Time.ToString("dd dddd , MMMM, yyyy", new CultureInfo("ar-AE")),
                                IsDeleted = s.is_deleted
                            }).ToList();
            res.payload = allsales;
            res.code = Static_Data.StaticApiStatus.ApiSuccess.Code;
            res.status = Static_Data.StaticApiStatus.ApiSuccess.Status;
            res.message = Static_Data.StaticApiStatus.ApiSuccess.MessageAr;
            return res;
        }

        public Response<List<dtoSalesInvForShow>> ReadCustomSalesInvoiceByCustomer(int customer_id)
        {
            Response<List<dtoSalesInvForShow>> res = new Response<List<dtoSalesInvForShow>>();
            var sales = (from s in db.tblSales
                         where s.customer_id == customer_id
                         join sd in db.tblSalesDetails on s.id equals sd.sale_inv_id
                         select new dtoSalesInvForShow()
                         {
                             id = s.id,
                             sale_inv_code = s.sale_inv_code,
                             invoice_total = s.total,
                             client_id = s.customer_id,
                             client_name = s.TblCustomer.customer_name,
                             store_id = s.store_id,
                             store_name = s.TblStore.store_name,
                             sales_Added_Time_ar = s.sales_Added_Time.ToString("dd dddd , MMMM, yyyy", new CultureInfo("ar-AE")),
                             IsDeleted = s.is_deleted,
                             lstProducts = s.SalesInvoiceDetailsLst.Select
                             (s => new dtoSalesDetailsForShow
                             {
                                 id = s.id,
                                 product_id = s.product_id,
                                 product_name = s.TblProduct.product_name,
                                 sales_price_one_product = s.sales_price_one_product,
                                 sales_inv_id = s.sale_inv_id,
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
    
        public Response<List<dtoSalesInvForShow>> ReadSalesInvDetails(int sale_inv_id)
        {
            Response<List<dtoSalesInvForShow>> res = new Response<List<dtoSalesInvForShow>>();
            var sales = (from s in db.tblSales
                            where s.id == sale_inv_id
                            select new dtoSalesInvForShow()
                            {
                                id = s.id,
                                sale_inv_code = s.sale_inv_code,
                                invoice_total = s.total,
                                client_id = s.customer_id,
                                client_name = s.TblCustomer.customer_name,
                                store_id = s.store_id,
                                store_name = s.TblStore.store_name,
                                discount=s.discount,
                                tax=s.tax,
                                tax_discount=s.tax,
                                final_total=s.final_total,
                                sales_Added_Time_ar = s.sales_Added_Time.ToString("dd dddd , MMMM, yyyy", new CultureInfo("ar-AE")),
                                IsDeleted = s.is_deleted,
                                lstProducts=s.SalesInvoiceDetailsLst.Select
                                (s => new dtoSalesDetailsForShow
                                {
                                    id=s.id,
                                   product_id=s.product_id,
                                    product_name = s.TblProduct.product_name,
                                    sales_price_one_product = s.sales_price_one_product,
                                    sales_inv_id = s.sale_inv_id,
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
        public Response<List<dtoSalesInvForShow>> GetDeletedSalesInvoices()
        {
            Response<List<dtoSalesInvForShow>> res = new Response<List<dtoSalesInvForShow>>();
            var allSales = (from s in db.tblSales
                               where s.is_deleted == 1
                               select new dtoSalesInvForShow()
                               {
                                   id = s.id,
                                   invoice_total = s.total,
                                   client_id = s.customer_id,
                                   client_name = s.TblCustomer.customer_name,
                                   store_id = s.store_id,
                                   store_name = s.TblStore.store_name,
                                   sales_Added_Time_ar = s.sales_Added_Time.ToString("dd dddd , MMMM, yyyy", new CultureInfo("ar-AE")),
                                   IsDeleted = s.is_deleted
                               }).ToList();
            res.payload = allSales;
            res.code = Static_Data.StaticApiStatus.ApiSuccess.Code;
            res.status = Static_Data.StaticApiStatus.ApiSuccess.Status;
            res.message = Static_Data.StaticApiStatus.ApiSuccess.MessageAr;
            return res;
        }

        public Response<List<dtoSalesInvForShow>> GetNotDeletedSalesInvoices()
        {
            Response<List<dtoSalesInvForShow>> res = new Response<List<dtoSalesInvForShow>>();
            var allSales = (from s in db.tblSales
                               where s.is_deleted == 0
                               select new dtoSalesInvForShow()
                               {
                                   id = s.id,
                                   invoice_total = s.total,
                                   client_id = s.customer_id,
                                   client_name = s.TblCustomer.customer_name,
                                   store_id = s.store_id,
                                   store_name = s.TblStore.store_name,
                                   sales_Added_Time_ar = s.sales_Added_Time.ToString("dd dddd , MMMM, yyyy", new CultureInfo("ar-AE")),
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
