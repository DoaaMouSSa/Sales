using DataLayer.DBContext;
using DataLayer.Tables;
using Dto.Dto;
using IRepository.IRepository;
using System;
using System.Collections.Generic;
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
        public string Add(dtoSalesInvDetails dto)
        {
            string msg = "";
            if (dto != null)
            {

                using (db.Database.BeginTransaction())
                {
                    try
                    {
                        var sales_inv = new TblSalesInvoice()
                        {
                            total = dto.invoice_total,
                            store_id = dto.store_id,
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
                        msg = "success";

                    }
                    catch (Exception ex)
                    {
                        db.Database.RollbackTransaction();
                        msg = ex.Message.ToString();
                    }
                }
            }
            return msg;
        }
    }
}
