using DataLayer.DBContext;
using DataLayer.Tables;
using Dto.Dto;
using IRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DtoReport.Dto;

namespace Repository.Repository
{
    public class ProductRepository : IProuductRepository
    {
        private readonly AccountDbContext db;
        public ProductRepository(AccountDbContext _db)
        {
            db = _db;
        }
        public Response<dtoProductForAdd> Add(dtoProductForAdd dto)
        {
            Response<dtoProductForAdd> res = new Response<dtoProductForAdd>();

            if (dto != null)
            {
                if (dto.product_name != "")
                {

                    if (db.tblProduct.Where(p => p.sub_cat_id == dto.sub_cat_id).Where(p=>p.code!="").Any(p => p.code == dto.code))

                    {
                        res.code = Static_Data.DuplicateData.DuplicateCode.Code;
                        res.status = Static_Data.DuplicateData.DuplicateCode.Status;
                        res.message = Static_Data.DuplicateData.DuplicateCode.MessageAr;
                    }
                    else if (db.tblProduct.Where(p => p.sub_cat_id == dto.sub_cat_id).Where(p => p.barcode != "").Any(p => p.barcode == dto.barcode))

                    {
                        res.code = Static_Data.DuplicateData.DuplicateBarCode.Code;
                        res.status = Static_Data.DuplicateData.DuplicateBarCode.Status;
                        res.message = Static_Data.DuplicateData.DuplicateBarCode.MessageAr;
                    }
                    else
                    {
                        var newPro = new TblProduct()
                        {
                            id = dto.id,
                            code = dto.code,
                            barcode = dto.barcode,
                            product_name = dto.product_name,
                            purchase_price = dto.purchase_price,
                            sale_price = dto.sale_price,
                            sub_cat_id = dto.sub_cat_id
                        };
                        db.tblProduct.Add(newPro);
                        db.SaveChanges();
                        dto.id = newPro.id;
                        dto.code = newPro.code;
                        dto.barcode = newPro.barcode;
                        dto.product_name = newPro.product_name;
                        dto.purchase_price = newPro.purchase_price;
                        dto.sale_price = newPro.sale_price;
                        dto.sub_cat_id = newPro.sub_cat_id;
                        res.code = Static_Data.StaticApiStatus.ApiSaveSuccess.Code;
                        res.status = Static_Data.StaticApiStatus.ApiSaveSuccess.Status;
                        res.message = Static_Data.StaticApiStatus.ApiSaveSuccess.MessageAr;
                        res.payload = dto;
                    }
                }
            }
            else
            {
                res.code = Static_Data.StaticApiStatus.ApiRequired.Code;
                res.status = Static_Data.StaticApiStatus.ApiRequired.Status;
                res.message = Static_Data.StaticApiStatus.ApiRequired.MessageAr;
            }

            
            return res;
        }

        public Response<bool> Delete(int id)
        {
            Response<bool> response = new Response<bool>();

            bool deleted = false;
            if (id != 0)
            {
                var Pro = db.tblProduct.Where(c => c.id == id).FirstOrDefault();
                db.tblProduct.Remove(Pro);
                db.SaveChanges();
                deleted = true;
            }
            return response;
        }

        public dtoProductForAdd Edit(dtoProductForAdd dto)
        {
            if (dto != null)
            {
                var isExist = db.tblProduct.Where(p => p.id == dto.id).FirstOrDefault();
                if (isExist != null)
                {
                    isExist.product_name = dto.product_name;
                    isExist.code = dto.code;
                    isExist.barcode = dto.barcode;
                    isExist.purchase_price = dto.purchase_price;
                    isExist.sale_price = dto.sale_price;
                    isExist.sub_cat_id = dto.sub_cat_id;
                }
                db.SaveChanges();
                dto.id = isExist.id;
                dto.code = isExist.code;
                dto.barcode = isExist.barcode;
                dto.product_name = isExist.product_name;
                dto.purchase_price = isExist.purchase_price;
                dto.sale_price = isExist.sale_price;
                dto.sub_cat_id = isExist.sub_cat_id;
            }
            return dto;
        }

        public List<dtoProductForShow> Read()
        {
            var allPro = (from p in db.tblProduct
                             select new dtoProductForShow()
                             {
                                 id = p.id,
                                 product_name = p.product_name,
                                 code = p.code,
                                 barcode = p.barcode,
                                 purchase_price=p.purchase_price,
                                 sale_price=p.sale_price,
                                 sub_cat_id=p.TblSubCategory.id,
                                 sub_cat_name=p.TblSubCategory.subcat_name,
                             }).ToList();
            return allPro;
        }
        public List<dtoProductReport> ReadForReport()
        {
            var allPro = (from p in db.tblProduct
                          select new dtoProductReport()
                          {
                              id = p.id,
                              product_name = p.product_name,
                              code = p.code,
                              barcode = p.barcode,
                              purchase_price = p.purchase_price,
                              sale_price = p.sale_price,
                              sub_cat_id = p.TblSubCategory.id,
                              sub_cat_name = p.TblSubCategory.subcat_name,
                          }).ToList();
            return allPro;
        }

        public Response<List<dtoProductForShowBeforeAddToInvoice>> ReadForInvoiceAdd(string character,int store_id)
        {
            Response<List<dtoProductForShowBeforeAddToInvoice>> response = new Response<List<dtoProductForShowBeforeAddToInvoice>>();
            var allPro = (from p in db.tblProduct
                          join s in db.tblStoreDetails on p.id equals s.product_id
                          where s.store_id == store_id && p.product_name.Contains(character) 
                    select new dtoProductForShowBeforeAddToInvoice()
                          {
                              id = p.id,
                              product_name = p.product_name,
                              purchase_price = p.purchase_price,
                              sale_price=p.sale_price,
                              qty=s.qty,
                          }).ToList();
            response.code = Static_Data.StaticApiStatus.ApiSuccess.Code;
            response.status = Static_Data.StaticApiStatus.ApiSuccess.Status;
            response.payload = allPro;
            return response;
        }
    }
}
