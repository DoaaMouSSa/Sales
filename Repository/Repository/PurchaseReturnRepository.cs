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
    public class PurchaseReturnRepository : IPurchaseReturnRepository
    {
        private readonly AccountDbContext db;
        public PurchaseReturnRepository(AccountDbContext _db)
        {
            db = _db;
        }
        public Response<int> GetMaxCode()
        {
            int numOfRows = db.tblPurchaseReturn.ToList().Count();
            Response<int> response = new Response<int>();
            if (numOfRows != 0)
            {
                response.code = Static_Data.StaticApiStatus.ApiSuccess.Code;
                response.status = Static_Data.StaticApiStatus.ApiSuccess.Status;
                response.message = Static_Data.StaticApiStatus.ApiSuccess.MessageAr;
                response.payload = db.tblPurchaseReturn.Max(p => p.pur_return_inv_code); ;
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
        public Response<dtoPurchaseReturnStoreDetails> AddPurchaseReturnInvoice(dtoPurchaseReturnStoreDetails dto)
        {
            Response<dtoPurchaseReturnStoreDetails> res = new Response<dtoPurchaseReturnStoreDetails>();

            if (dto != null)
            {
                var availableCode = db.tblPurchaseReturn.Any(p => p.pur_return_inv_code == dto.pur_return_inv_code);
                if (dto.pur_return_inv_code == 0 || availableCode == true)
                {
                    var code = GetMaxCode().payload;
                    dto.pur_return_inv_code = code + 1;
                }
                    using (db.Database.BeginTransaction())
                    {
                        try
                        {                                                                                                                               
                            var purchase_return_inv = new TblPurchaseInvoiceReturn()
                            {
                                total = dto.invoice_total,
                                final_total=dto.final_total,
                                tax=dto.tax,
                                discount=dto.discount,
                                tax_discount=dto.tax_discount,
                                store_id = dto.store_id,
                                supplier_id = dto.supplier_id,
                                pur_return_inv_code = dto.pur_return_inv_code,
                                purchase_return_Added_Time = DateTime.Now
                            };
                            db.Add(purchase_return_inv);
                            db.SaveChanges();
                            dto.id = purchase_return_inv.id;
                            for (int i = 0; i < dto.purchase_return_invoice_details.Count(); i++)
                            {
                            //add-products-details
                                var row = dto.purchase_return_invoice_details[i];
                                var purchase_return_inv_details = new TblPurchaseInvoiceReturnDetails()
                                {
                                    product_id = row.product_id,
                                    notes = row.notes,
                                    purchase_price_one_product = row.purchase_price_one_product,
                                    qty = row.qty,
                                    total_purchase_price_one_product = row.total_purchase_price_one_product,
                                    purchase_return_inv_id = dto.id
                                };
                                db.Add(purchase_return_inv_details);
                                db.SaveChanges();
                          
                            //decrease qty from store
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
                                    product_exist.qty -= row.qty;
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
                        res.message = ex.InnerException.Message.ToString();
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
        public Response<dtoInvoiceNumbers> GetTotalAfterDiscounts(decimal  total, decimal  discountNum, decimal  discountPer, decimal  tax, decimal  DiscountTax)
        {
            decimal finalTotal = 0;
            decimal taxAmount = 0;
            decimal discountAmount = 0;
            decimal taxDiscountAmount = 0;
            //حساب نسبه الخصم من قيمه الاصناف
            if ((discountPer != 0 && discountNum != 0)||(discountPer != 0 && discountNum == 0))
            {
                discountPer = discountPer / 100;
                discountAmount = (total * discountPer);
                finalTotal = total - discountAmount;
            }else if(discountPer == 0 && discountNum != 0)
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
            dtoInvoiceNumbers Numbers = new dtoInvoiceNumbers() { 
                total= total,
                discount= discountAmount,
                tax= taxAmount,
                tax_discount= taxDiscountAmount,
                final_total= finalTotal,
            };
            response.code = Static_Data.StaticApiStatus.ApiSuccess.Code;
            response.status = Static_Data.StaticApiStatus.ApiSuccess.Status;
            response.message = Static_Data.StaticApiStatus.ApiSuccess.MessageAr;
            response.payload = Numbers;
            return response;
        }
        public Response<bool> Delete(int id)
        {
            Response<bool> response = new Response<bool>();
            if (id != 0)
            {
                var isExist = db.tblPurchaseReturn.Where(p => p.id == id).FirstOrDefault();
                if (isExist != null)
                {
                    isExist.is_deleted = 1;
                }
                db.SaveChanges();
                   var purchaseReturnDetails = db.tblPurchaseReturnDetails.Where(pp => pp.purchase_return_inv_id == id).ToList();
                   foreach(var item in purchaseReturnDetails)
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
        public Response<List<dtoPurchaseInvReturnForShow>> ReadAllPurchaseReturnInvoices()
        {
            Response<List<dtoPurchaseInvReturnForShow>> res = new Response<List<dtoPurchaseInvReturnForShow>>();
            var allpurchase = (from p in db.tblPurchaseReturn
                               select new dtoPurchaseInvReturnForShow()
                               {
                                   id = p.id,
                                   invoice_total = p.total,
                                   discount = p.discount,
                                   tax = p.tax,
                                   tax_discount = p.tax_discount,
                                   final_total = p.final_total,
                                   supplier_id = p.supplier_id,
                                   supplier_name = p.TblSupplier.supplier_name,
                                   store_id = p.store_id,
                                   store_name = p.TblStore.store_name,
                                   purchase_Added_Time_ar = p.purchase_return_Added_Time.ToString("dd dddd , MMMM, yyyy", new CultureInfo("ar-AE")),
                                   IsDeleted = p.is_deleted,
                                 
        }).ToList();
            res.payload = allpurchase;
            res.code = Static_Data.StaticApiStatus.ApiSuccess.Code;
            res.status = Static_Data.StaticApiStatus.ApiSuccess.Status;
            res.message = Static_Data.StaticApiStatus.ApiSuccess.MessageAr;
            return res;
        }
        public Response<List<dtoPurchaseInvReturnForShow>> ReadCustomPurchaseReturnInvoice(int pur_return_inv_id)
        {
            Response<List<dtoPurchaseInvReturnForShow>> res = new Response<List<dtoPurchaseInvReturnForShow>>();
            var purchase = (from p in db.tblPurchaseReturn.Where(p=>p.id==pur_return_inv_id)
                               select new dtoPurchaseInvReturnForShow()
                               {
                                   id = p.id,
                                   invoice_total = p.total,
                                   discount = p.discount,
                                   tax = p.tax,
                                   tax_discount = p.tax_discount,
                                   final_total = p.final_total,
                                   supplier_id = p.supplier_id,
                                   supplier_name = p.TblSupplier.supplier_name,
                                   store_id = p.store_id,
                                   store_name = p.TblStore.store_name,
                                   purchase_Added_Time_ar = p.purchase_return_Added_Time.ToString("dd dddd , MMMM, yyyy", new CultureInfo("ar-AE")),
                                   IsDeleted = p.is_deleted,
                                   lstProducts = p.PurchaseInvoiceReturnDetailsLst.Select(p =>
                                       new dtoPurchaseDetialsReturnForShow
                                       {
                                           id = p.id,
                                           product_id = p.product_id,
                                           product_name=p.TblProduct.product_name,
                                           purchase_price_one_product = p.purchase_price_one_product,
                                           purchase_inv_id = p.purchase_return_inv_id,
                                           qty = p.qty,
                                           total_purchase_price_one_product = p.total_purchase_price_one_product,
                                           notes = p.notes,
                                       }).ToList()

                               }).ToList();
            res.payload = purchase;
            res.code = Static_Data.StaticApiStatus.ApiSuccess.Code;
            res.status = Static_Data.StaticApiStatus.ApiSuccess.Status;
            res.message = Static_Data.StaticApiStatus.ApiSuccess.MessageAr;
            return res;
        }
        public List<dtoPurchaseInvReturnForShow> ReadCustomPurchaseReturnInvoiceForReport(int pur_return_inv_id)
        {
            var purchase = (from p in db.tblPurchaseReturn.Where(p => p.pur_return_inv_code == pur_return_inv_id)
                            select new dtoPurchaseInvReturnForShow()
                            {
                                id = p.id,
                                invoice_total = p.total,
                                discount=p.discount,
                                tax=p.tax,
                                tax_discount=p.tax_discount,
                                final_total=p.final_total,
                                supplier_id = p.supplier_id,
                                supplier_name = p.TblSupplier.supplier_name,
                                store_id = p.store_id,
                                store_name = p.TblStore.store_name,
                                purchase_Added_Time_ar = p.purchase_return_Added_Time.ToString("dd dddd , MMMM, yyyy", new CultureInfo("ar-AE")),
                                IsDeleted = p.is_deleted,
                                lstProducts = p.PurchaseInvoiceReturnDetailsLst.Select(p =>
                                    new dtoPurchaseDetialsReturnForShow
                                    {
                                        id = p.id,
                                        product_id = p.product_id,
                                        product_name = p.TblProduct.product_name,
                                        purchase_price_one_product = p.purchase_price_one_product,
                                        purchase_inv_id = p.purchase_return_inv_id,
                                        qty = p.qty,
                                        total_purchase_price_one_product = p.total_purchase_price_one_product,
                                        notes = p.notes,
                                    }).ToList()

                            }).ToList();

            return purchase;
        }
        public Response<List<dtoPurchaseInvReturnForShow>> FilterPurchaseReturnInvoices(int supplier_id,int store_id, DateTime from_date, DateTime to_date)
        {
            Response<List<dtoPurchaseInvReturnForShow>> res = new Response<List<dtoPurchaseInvReturnForShow>>();
            var purchase = new List<dtoPurchaseInvReturnForShow>();
            if (supplier_id == 0 && store_id != 0)
            {
                purchase = (from p in db.tblPurchaseReturn
                           .Where(p => p.store_id == store_id)
                            .Where(p => p.purchase_return_Added_Time >= from_date && p.purchase_return_Added_Time <= to_date)
                            select new dtoPurchaseInvReturnForShow()
                            {
                                id = p.id,
                                invoice_total = p.total,
                                supplier_id = p.supplier_id,
                                supplier_name = p.TblSupplier.supplier_name,
                                store_id = p.store_id,
                                store_name = p.TblStore.store_name,
                                purchase_Added_Time=p.purchase_return_Added_Time,
                                purchase_Added_Time_ar = p.purchase_return_Added_Time.ToString("dd dddd , MMMM, yyyy", new CultureInfo("ar-AE")),
                                IsDeleted = p.is_deleted,
                            }).ToList();
            }
            else if(supplier_id !=0 && store_id == 0)
            {
                purchase = (from p in db.tblPurchaseReturn.Where(p => p.supplier_id == supplier_id)
                    .Where(p => p.purchase_return_Added_Time >= from_date && p.purchase_return_Added_Time <= to_date)
                            select new dtoPurchaseInvReturnForShow()
                            {
                                id = p.id,
                                invoice_total = p.total,
                                supplier_id = p.supplier_id,
                                supplier_name = p.TblSupplier.supplier_name,
                                store_id = p.store_id,
                                store_name = p.TblStore.store_name,
                                purchase_Added_Time = p.purchase_return_Added_Time,

                                purchase_Added_Time_ar = p.purchase_return_Added_Time.ToString("dd dddd , MMMM, yyyy", new CultureInfo("ar-AE")),
                                IsDeleted = p.is_deleted,
                            }).ToList();
            }
            else if(supplier_id != 0 && store_id !=0)
            {
              purchase = (from p in db.tblPurchaseReturn.Where(p => p.supplier_id == supplier_id)
                            .Where(p => p.store_id == store_id)
                           .Where(p => p.purchase_return_Added_Time >= from_date && p.purchase_return_Added_Time <= to_date)
                          select new dtoPurchaseInvReturnForShow()
                                {
                                    id = p.id,
                                    invoice_total = p.total,
                                    supplier_id = p.supplier_id,
                                    supplier_name = p.TblSupplier.supplier_name,
                                    store_id = p.store_id,
                                    store_name = p.TblStore.store_name,
                              purchase_Added_Time = p.purchase_return_Added_Time,

                              purchase_Added_Time_ar = p.purchase_return_Added_Time.ToString("dd dddd , MMMM, yyyy", new CultureInfo("ar-AE")),
                                    IsDeleted = p.is_deleted,
                                }).ToList();
            }
            else if (supplier_id == 0 && store_id == 0)
            {
                purchase = (from p in db.tblPurchaseReturn
                             .Where(p => from_date <= p.purchase_return_Added_Time && p.purchase_return_Added_Time <= to_date)
                            select new dtoPurchaseInvReturnForShow()
                            {
                                id = p.id,
                                invoice_total = p.total,
                                supplier_id = p.supplier_id,
                                supplier_name = p.TblSupplier.supplier_name,
                                store_id = p.store_id,
                                store_name = p.TblStore.store_name,
                                purchase_Added_Time = p.purchase_return_Added_Time,
                                purchase_Added_Time_ar = p.purchase_return_Added_Time.ToString("dd dddd , MMMM, yyyy", new CultureInfo("ar-AE")),
                                IsDeleted = p.is_deleted,
                            }).ToList();
            }
            else
            {
                throw new NullReferenceException();
            }
            
            res.payload = purchase;
            res.code = Static_Data.StaticApiStatus.ApiSuccess.Code;
            res.status = Static_Data.StaticApiStatus.ApiSuccess.Status;
            res.message = Static_Data.StaticApiStatus.ApiSuccess.MessageAr;
            return res;
        }

        public Response<List<dtoPurchaseInvReturnForShow>> GetDeletedPurchaseReturnInvoices()
        {
            Response<List<dtoPurchaseInvReturnForShow>> res = new Response<List<dtoPurchaseInvReturnForShow>>();
            var allpurchase = (from p in db.tblPurchaseReturn
                               where p.is_deleted==1
                               select new dtoPurchaseInvReturnForShow()
                               {
                                   id = p.id,
                                   invoice_total = p.total,
                                   supplier_id = p.supplier_id,
                                   supplier_name = p.TblSupplier.supplier_name,
                                   store_id = p.store_id,
                                   store_name = p.TblStore.store_name,
                                   purchase_Added_Time_ar = p.purchase_return_Added_Time.ToString("dd dddd , MMMM, yyyy", new CultureInfo("ar-AE")),
                                   IsDeleted = p.is_deleted,
                               }).ToList();
            res.payload = allpurchase;
            res.code = Static_Data.StaticApiStatus.ApiSuccess.Code;
            res.status = Static_Data.StaticApiStatus.ApiSuccess.Status;
            res.message = Static_Data.StaticApiStatus.ApiSuccess.MessageAr;
            return res;
        }

        public Response<List<dtoPurchaseInvReturnForShow>> GetNotDeletedPurchaseReturnInvoices()
        {
            Response<List<dtoPurchaseInvReturnForShow>> res = new Response<List<dtoPurchaseInvReturnForShow>>();
            var allpurchase = (from p in db.tblPurchaseReturn
                               where p.is_deleted == 0
                               select new dtoPurchaseInvReturnForShow()
                               {
                                   id = p.id,
                                   invoice_total = p.total,
                                   supplier_id = p.supplier_id,
                                   supplier_name = p.TblSupplier.supplier_name,
                                   store_id = p.store_id,
                                   store_name = p.TblStore.store_name,
                                   purchase_Added_Time_ar = p.purchase_return_Added_Time.ToString("dd dddd , MMMM, yyyy", new CultureInfo("ar-AE")),
                                   IsDeleted = p.is_deleted,
                               }).ToList();
            res.payload = allpurchase;
            res.code = Static_Data.StaticApiStatus.ApiSuccess.Code;
            res.status = Static_Data.StaticApiStatus.ApiSuccess.Status;
            res.message = Static_Data.StaticApiStatus.ApiSuccess.MessageAr;
            return res;
        }
    }
}
