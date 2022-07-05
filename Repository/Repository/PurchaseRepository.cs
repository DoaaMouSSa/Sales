using DataLayer.DBContext;
using DataLayer.Tables;
using Dto.Dto;
using IRepository.IRepository;
using Repository.Static_Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{

    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly AccountDbContext db;
        public PurchaseRepository(AccountDbContext _db)
        {
            db = _db;
        }
        public Tuple<int,int> getMinMaxCode()
        {
            int minCode = db.tblPurchase.Min(p => p.pur_inv_code);
            int maxCode = db.tblPurchase.Max(p => p.pur_inv_code);
            Tuple<int, int> Dictionary = new Tuple<int, int>(minCode, maxCode);
            return Dictionary;
        }
        public Response<int> GetMaxCode()
        {
            int numOfRows = db.tblPurchase.ToList().Count();
            Response<int> response = new Response<int>();
            if (numOfRows != 0)
            {
                response.code = Static_Data.StaticApiStatus.ApiSuccess.Code;
                response.status = Static_Data.StaticApiStatus.ApiSuccess.Status;
                response.message = Static_Data.StaticApiStatus.ApiSuccess.MessageAr;
                response.payload = db.tblPurchase.Max(p => p.pur_inv_code); 
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
        public Response<dtoPurchaseInvForShow> Add(dtoPurchaseStoreDetails dto)
        {
            Response<dtoPurchaseInvForShow> res = new Response<dtoPurchaseInvForShow>();

            if (dto != null)
            {
                var availableCode = db.tblPurchase.Any(p => p.pur_inv_code == dto.pur_inv_code);
                if (dto.pur_inv_code == 0 || availableCode == true)
                {
                    var code = GetMaxCode().payload;
                    dto.pur_inv_code = code + 1;
                }
                    using (db.Database.BeginTransaction())
                    {
                        try
                        {
                            var purchase_inv = new TblPurchaseInvoice()
                            {
                                total = dto.invoice_total,
                                final_total=dto.final_total,
                                tax=dto.tax,
                                discount=dto.discount,
                                tax_discount=dto.tax_discount,
                                store_id = dto.store_id,
                                supplier_id = dto.supplier_id,
                                pur_inv_code = dto.pur_inv_code,
                                purchase_Added_Time = DateTime.Now
                            };
                            db.Add(purchase_inv);
                            db.SaveChanges();
                            dto.id = purchase_inv.id;
                            for (int i = 0; i < dto.purchase_invoice_details.Count(); i++)
                            {
                            //add-products-details
                                var row = dto.purchase_invoice_details[i];
                                var purchase_inv_details = new TblPurchaseInvoiceDetails()
                                {
                                    product_id = row.product_id,
                                    notes = row.notes,
                                    purchase_price_one_product = row.purchase_price_one_product,
                                    qty = row.qty,
                                    total_purchase_price_one_product = row.total_purchase_price_one_product,
                                    purchase_inv_id = dto.id
                                };
                                db.Add(purchase_inv_details);
                                db.SaveChanges();
                            //update purchase_price to products
                            var product = db.tblProduct.Where(p => p.id == row.product_id).FirstOrDefault();
                            product.purchase_price = row.purchase_price_one_product;
                            //increase qty to store
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
          var purchase = (from p in db.tblPurchase.Where(p => p.id == dto.id)
                                        select new dtoPurchaseInvForShow()
                                        {
                                            id = p.id,
                                            pur_inv_code = p.pur_inv_code,
                                            invoice_total = p.total,
                                            final_total = p.final_total,
                                            discount = p.discount,
                                            tax = p.tax,
                                            tax_discount = p.tax_discount,
                                            supplier_id = p.supplier_id,
                                            supplier_name = p.TblSupplier.supplier_name,
                                            store_id = p.store_id,
                                            store_name = p.TblStore.store_name,
                                            purchase_Added_Time_ar = p.purchase_Added_Time.ToString("dd dddd , MMMM, yyyy", new CultureInfo("ar-AE")),
                                            IsDeleted = p.is_deleted,
                                            lstProducts = p.PurchaseInvoiceDetailsLst.Select(p =>
                                                new dtoPurchaseDetialsForShow
                                                {
                                                    id = p.id,
                                                    product_id = p.product_id,
                                                    product_name = p.TblProduct.product_name,
                                                    purchase_price_one_product = p.purchase_price_one_product,
                                                    purchase_inv_id = p.purchase_inv_id,
                                                    qty = p.qty,
                                                    total_purchase_price_one_product = p.total_purchase_price_one_product,
                                                    notes = p.notes,
                                                }).ToList()

                                        }).FirstOrDefault();
                        res.code = Static_Data.SalesIvoices.SuccessSalesInvoice.Code;
                        res.status = Static_Data.SalesIvoices.SuccessSalesInvoice.Status;
                        res.message = Static_Data.SalesIvoices.SuccessSalesInvoice.MessageAr;
                        res.payload = purchase;
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
                var isExist = db.tblPurchase.Where(p => p.id == id).FirstOrDefault();
                if (isExist != null)
                {
                    isExist.is_deleted = 1;
                }
                db.SaveChanges();
                   var purchaseDetails = db.tblPurchaseDetails.Where(pp => pp.purchase_inv_id == id).ToList();
                   foreach(var item in purchaseDetails)
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

        //public dtoPurchaseForShow Edit(dtoPurchaseForAdd dto)
        //{
        //    dtoPurchaseForShow dto_Edited = new dtoPurchaseForShow();

        //    if (dto != null)
        //    {
        //        var isExist = db.tblPurchase.Where(c => c.id == dto.id).FirstOrDefault();
        //        if (isExist != null)
        //        {
        //            if (dto.product_id != 0)
        //            {
        //                isExist.product_id = dto.product_id;
        //            }
        //            if (dto.store_id != 0)
        //            {
        //                isExist.store_id = dto.store_id;
        //            }
        //            if (dto.qty != 0)
        //            {
        //                isExist.qty = dto.qty;
        //            }
        //            if (dto.purchase_price_one_product != 0)
        //            {
        //                isExist.purchase_price_one_product = dto.purchase_price_one_product;
        //            }
        //            if (dto.total_purchase_price_one_product != 0)
        //            {
        //                isExist.total_purchase_price_one_product = dto.total_purchase_price_one_product;
        //            }
        //            if (dto.notes != null)
        //            {
        //                isExist.notes = dto.notes;
        //            }
        //        }
        //        db.SaveChanges();
        //        dto_Edited.id = isExist.id;
        //        dto_Edited.product_id = isExist.product_id;

        //        dto_Edited.product_name = isExist.TblProduct.product_name;
        //        dto_Edited.store_id = isExist.store_id;
        //        dto_Edited.store_name = isExist.TblStore.store_name;
        //        dto_Edited.qty = isExist.qty;
        //        dto_Edited.purchase_price_one_product = isExist.purchase_price_one_product;
        //        dto_Edited.total_purchase_price_one_product = isExist.total_purchase_price_one_product;
        //        dto_Edited.notes = isExist.notes;
        //        dto_Edited.purchase_Added_Time = isExist.purchase_Added_Time;
        //    }
        //    return dto_Edited;
        //}
        public Response<List<dtoPurchaseInvForShow>> Read()
        {
            Response<List<dtoPurchaseInvForShow>> res = new Response<List<dtoPurchaseInvForShow>>();
            var allpurchase = (from p in db.tblPurchase
                               orderby p.pur_inv_code descending
                               select new dtoPurchaseInvForShow()
                               {
                                   id = p.id,
                                   pur_inv_code = p.pur_inv_code,
                                   invoice_total = p.total,
                                   final_total=p.final_total,
                                   discount=p.discount,
                                   tax=p.tax,
                                   tax_discount=p.tax_discount,
                                   supplier_id = p.supplier_id,
                                   supplier_name = p.TblSupplier.supplier_name,
                                   store_id = p.store_id,
                                   store_name = p.TblStore.store_name,
                                   purchase_Added_Time_ar = p.purchase_Added_Time.ToString("dd dddd , MMMM, yyyy", new CultureInfo("ar-AE")),
                                   IsDeleted = p.is_deleted,
                                   

        }).ToList();
            res.payload = allpurchase;
            res.code = Static_Data.StaticApiStatus.ApiSuccess.Code;
            res.status = Static_Data.StaticApiStatus.ApiSuccess.Status;
            res.message = Static_Data.StaticApiStatus.ApiSuccess.MessageAr;
            return res;
        }
        public Response<List<dtoPurchaseInvForShow>> ReadCustomPurchaseInvoice(int pur_inv_id)
        {
            Response<List<dtoPurchaseInvForShow>> res = new Response<List<dtoPurchaseInvForShow>>();
            var purchase = (from p in db.tblPurchase.Where(p=>p.id==pur_inv_id)
                               select new dtoPurchaseInvForShow()
                               {
                                   id = p.id,
                                   pur_inv_code = p.pur_inv_code,
                                   invoice_total = p.total,
                                   final_total = p.final_total,
                                   discount = p.discount,
                                   tax = p.tax,
                                   tax_discount = p.tax_discount,
                                   supplier_id = p.supplier_id,
                                   supplier_name = p.TblSupplier.supplier_name,
                                   store_id = p.store_id,
                                   store_name = p.TblStore.store_name,
                                   purchase_Added_Time_ar = p.purchase_Added_Time.ToString("dd dddd , MMMM, yyyy", new CultureInfo("ar-AE")),
                                   IsDeleted = p.is_deleted,
                                   lstProducts = p.PurchaseInvoiceDetailsLst.Select(p =>
                                       new dtoPurchaseDetialsForShow
                                       {
                                           id = p.id,
                                           product_id = p.product_id,
                                           product_name=p.TblProduct.product_name,
                                           purchase_price_one_product = p.purchase_price_one_product,
                                           purchase_inv_id = p.purchase_inv_id,
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
        public List<dtoPurchaseInvForShow> ReadPurchaseInvDetailsForReport(int pur_inv_id)
        {
            List<dtoPurchaseInvForShow> res = new List<dtoPurchaseInvForShow>();
            var purchase = (from p in db.tblPurchase
                         where p.pur_inv_code == pur_inv_id
                         select new dtoPurchaseInvForShow()
                         {
                             id = p.id,
                             pur_inv_code = p.pur_inv_code,
                             invoice_total = p.total,
                             final_total = p.final_total,
                             discount = p.discount,
                             tax = p.tax,
                             tax_discount = p.tax_discount,
                             supplier_id = p.supplier_id,
                             supplier_name = p.TblSupplier.supplier_name,
                             store_id = p.store_id,
                             store_name = p.TblStore.store_name,
                             purchase_Added_Time_ar = p.purchase_Added_Time.ToString("dd dddd , MMMM, yyyy", new CultureInfo("ar-AE")),
                             IsDeleted = p.is_deleted,
                             lstProducts = p.PurchaseInvoiceDetailsLst.Select(p =>
                                 new dtoPurchaseDetialsForShow
                                 {
                                     id = p.id,
                                     product_id = p.product_id,
                                     product_name = p.TblProduct.product_name,
                                     purchase_price_one_product = p.purchase_price_one_product,
                                     purchase_inv_id = p.purchase_inv_id,
                                     qty = p.qty,
                                     total_purchase_price_one_product = p.total_purchase_price_one_product,
                                     notes = p.notes,
                                 }).ToList()

                         }).ToList();
            res = purchase;
            return res;
        }

        public Response<List<dtoPurchaseInvForShow>> FilterPurchaseInvoices(int supplier_id,int store_id, DateTime from_date, DateTime to_date, int from_code, int to_code,int deleted_val)
        {
            Response<List<dtoPurchaseInvForShow>> res = new Response<List<dtoPurchaseInvForShow>>();
            var purchase = new List<dtoPurchaseInvForShow>();
           
            if (supplier_id == 0 && store_id != 0)
            {

                purchase = (from p in db.tblPurchase
                           .Where(p => p.store_id == store_id)
                            .Where(p => p.purchase_Added_Time >= from_date && p.purchase_Added_Time <= to_date)
                            .Where(p => p.pur_inv_code >= from_code && p.pur_inv_code <= to_code)
                       .Where(p => deleted_val == 2 ? (true) : (p.is_deleted == deleted_val))
                            orderby p.pur_inv_code descending
                            select new dtoPurchaseInvForShow()
                            {
                                id = p.id,
                                pur_inv_code = p.pur_inv_code,
                                invoice_total = p.total,
                                supplier_id = p.supplier_id,
                                supplier_name = p.TblSupplier.supplier_name,
                                store_id = p.store_id,
                                store_name = p.TblStore.store_name,
                                purchase_Added_Time = p.purchase_Added_Time,
                                purchase_Added_Time_ar = p.purchase_Added_Time.ToString("dd dddd , MMMM, yyyy", new CultureInfo("ar-AE")),
                                IsDeleted = p.is_deleted,
                            }).ToList();
            }
            else if(supplier_id !=0 && store_id == 0)
            {
                purchase = (from p in db.tblPurchase.Where(p => p.supplier_id == supplier_id)                        
                    .Where(p => p.purchase_Added_Time >= from_date && p.purchase_Added_Time <= to_date)
                     .Where(p => p.pur_inv_code >= from_code && p.pur_inv_code <= to_code)
                       .Where(p => deleted_val == 2 ? (true) : (p.is_deleted == deleted_val))
                            orderby p.pur_inv_code descending
                            select new dtoPurchaseInvForShow()
                            {
                                id = p.id,
                                pur_inv_code = p.pur_inv_code,
                                invoice_total = p.total,
                                supplier_id = p.supplier_id,
                                supplier_name = p.TblSupplier.supplier_name,
                                store_id = p.store_id,
                                store_name = p.TblStore.store_name,
                                purchase_Added_Time = p.purchase_Added_Time,
                                purchase_Added_Time_ar = p.purchase_Added_Time.ToString("dd dddd , MMMM, yyyy", new CultureInfo("ar-AE")),
                                IsDeleted = p.is_deleted,
                            }).ToList();
            }
            else if(supplier_id != 0 && store_id !=0)
            {
              purchase = (from p in db.tblPurchase.Where(p => p.supplier_id == supplier_id)
                            .Where(p => p.store_id == store_id)
                           .Where(p => p.purchase_Added_Time >= from_date && p.purchase_Added_Time <= to_date)
                            .Where(p => p.pur_inv_code >= from_code && p.pur_inv_code <= to_code)
                    .Where(p => deleted_val == 2  ? (true) : (p.is_deleted== deleted_val))
                          orderby p.pur_inv_code descending
                          select new dtoPurchaseInvForShow()
                                {
                                    id = p.id,
                                    pur_inv_code = p.pur_inv_code,
                                    invoice_total = p.total,
                                    supplier_id = p.supplier_id,
                                    supplier_name = p.TblSupplier.supplier_name,
                                    store_id = p.store_id,
                                    store_name = p.TblStore.store_name,
                              purchase_Added_Time = p.purchase_Added_Time,
                              purchase_Added_Time_ar = p.purchase_Added_Time.ToString("dd dddd , MMMM, yyyy", new CultureInfo("ar-AE")),
                                    IsDeleted = p.is_deleted,
                                }).ToList();
            }
            else if (supplier_id == 0 && store_id == 0)
            {
                purchase = (from p in db.tblPurchase
                             .Where(p => from_date <= p.purchase_Added_Time && p.purchase_Added_Time <= to_date)
                              .Where(p => p.pur_inv_code >= from_code && p.pur_inv_code <= to_code)
  .Where(p => deleted_val == 2 ? (true) : (p.is_deleted == deleted_val))
                            orderby p.pur_inv_code descending
                            select new dtoPurchaseInvForShow()
                            {
                                id = p.id,
                                pur_inv_code = p.pur_inv_code,
                                invoice_total = p.total,
                                supplier_id = p.supplier_id,
                                supplier_name = p.TblSupplier.supplier_name,
                                store_id = p.store_id,
                                store_name = p.TblStore.store_name,
                                purchase_Added_Time = p.purchase_Added_Time,
                                purchase_Added_Time_ar = p.purchase_Added_Time.ToString("dd dddd , MMMM, yyyy", new CultureInfo("ar-AE")),
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

        public Response<List<dtoPurchaseInvForShow>> GetDeletedPurchaseInvoices()
        {
            Response<List<dtoPurchaseInvForShow>> res = new Response<List<dtoPurchaseInvForShow>>();
            var allpurchase = (from p in db.tblPurchase
                               where p.is_deleted==1
                               select new dtoPurchaseInvForShow()
                               {
                                   id = p.id,
                                   pur_inv_code = p.pur_inv_code,
                                   invoice_total = p.total,
                                   supplier_id = p.supplier_id,
                                   supplier_name = p.TblSupplier.supplier_name,
                                   store_id = p.store_id,
                                   store_name = p.TblStore.store_name,
                                   purchase_Added_Time_ar = p.purchase_Added_Time.ToString("dd dddd , MMMM, yyyy", new CultureInfo("ar-AE")),
                                   IsDeleted = p.is_deleted,
                               }).ToList();
            res.payload = allpurchase;
            res.code = Static_Data.StaticApiStatus.ApiSuccess.Code;
            res.status = Static_Data.StaticApiStatus.ApiSuccess.Status;
            res.message = Static_Data.StaticApiStatus.ApiSuccess.MessageAr;
            return res;
        }

        public Response<List<dtoPurchaseInvForShow>> GetNotDeletedPurchaseInvoices()
        {
            Response<List<dtoPurchaseInvForShow>> res = new Response<List<dtoPurchaseInvForShow>>();
            var allpurchase = (from p in db.tblPurchase
                               where p.is_deleted == 0
                               select new dtoPurchaseInvForShow()
                               {
                                   id = p.id,
                                   pur_inv_code = p.pur_inv_code,
                                   invoice_total = p.total,
                                   supplier_id = p.supplier_id,
                                   supplier_name = p.TblSupplier.supplier_name,
                                   store_id = p.store_id,
                                   store_name = p.TblStore.store_name,
                                   purchase_Added_Time_ar = p.purchase_Added_Time.ToString("dd dddd , MMMM, yyyy", new CultureInfo("ar-AE")),
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
