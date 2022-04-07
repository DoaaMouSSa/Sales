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
                        res.code = Static_Data.StaticApiStatus.ApiDuplicate.Code;
                        res.status = Static_Data.StaticApiStatus.ApiDuplicate.Status;
                        res.message = Static_Data.StaticApiStatus.ApiDuplicate.MessageAr;
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

        public bool Delete(int id)
        {
            bool deleted = false;
            if (id != 0)
            {
                var Pro = db.tblProduct.Where(c => c.id == id).FirstOrDefault();
                db.tblProduct.Remove(Pro);
                db.SaveChanges();
                deleted = true;
            }
            return deleted;
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
    }
}
