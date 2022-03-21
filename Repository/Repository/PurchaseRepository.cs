using DataLayer.DBContext;
using DataLayer.Tables;
using Dto.Dto;
using IRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
        public string Add(dtoPurchaseStoreDetails dto)
        {
            string msg="";
            if (dto != null)
            {
             
                    using (db.Database.BeginTransaction())
                {
                    try
                    {
                        var purchase_inv = new TblPurchaseInvoice()
                        {
                            total = dto.invoice_total,
                            store_id = dto.store_id,
                            purchase_Added_Time = DateTime.Now
                        };
                        db.Add(purchase_inv);
                        db.SaveChanges();
                        dto.id = purchase_inv.id;
                        for (int i = 0; i < dto.purchase_invoice_details.Count(); i++)
                        {
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
                        }
                        for (int x = 0; x < dto.purchase_store_details.Count(); x++)
                        {
                            var rowStore = dto.purchase_store_details[x];
                            var product_exist = db.tblStoreDetails.Where(s => s.store_id == dto.store_id
                              && s.product_id == rowStore.product_id).FirstOrDefault();
                            if (product_exist == null)
                            {
                                var store_details = new TblStoreDetails()
                                {
                                    product_id = rowStore.product_id,
                                    qty = rowStore.qty,
                                    store_id = dto.store_id
                                };
                                db.Add(store_details);
                                db.SaveChanges();
                            }
                            else{
                                product_exist.qty += rowStore.qty;
                                db.SaveChanges();
                            }
                           
                            
                        }
                        db.Database.CommitTransaction();
                        msg = "success";
                        
                    }
                    catch (Exception ex)
                    {
                        db.Database.RollbackTransaction();
                        msg= ex.Message.ToString();
                    }
                }
            }
            return msg;
        }
        public bool Delete(int id)
        {
            bool deleted = false;
            if (id != 0)
            {
                var purchase = db.tblPurchase.Where(c => c.id == id).FirstOrDefault();
                db.tblPurchase.Remove(purchase);
                db.SaveChanges();
                deleted = true;
            }
            return deleted;
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

        //public List<dtoPurchaseForShow> Read()
        //{
        //    var allpurchase = (from p in db.tblPurchase
        //                  select new dtoPurchaseForShow()
        //                  {
        //                      id = p.id,
        //                      product_id = p.product_id,
        //                      product_name = p.TblProduct.product_name,
        //                      store_id = p.store_id,
        //                      store_name = p.TblStore.store_name,
        //                      purchase_price_one_product = p.purchase_price_one_product,
        //                      total_purchase_price_one_product = p.total_purchase_price_one_product,
        //                      notes = p.notes,
        //                      qty = p.qty,
        //                      purchase_Added_Time=p.purchase_Added_Time
        //                  }).ToList();
        //    return allpurchase;
        //}
    }
}
