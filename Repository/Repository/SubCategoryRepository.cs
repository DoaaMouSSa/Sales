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
    public class SubCategoryRepository : ISubCategoryRepository
    {
        private readonly AccountDbContext db;
        public SubCategoryRepository(AccountDbContext _db)
        {
            db = _db;
        }
        public Response<dtoSubCategoryForAdd>  Add(dtoSubCategoryForAdd dto)
        {
            Response<dtoSubCategoryForAdd> res = new Response<dtoSubCategoryForAdd>();

            if (dto != null)
            {
                if (dto.subcat_name != "")
                {
                    if (db.tblSubCategory.Where(c => c.cat_id == dto.cat_id).Any(c => c.subcat_name == dto.subcat_name))
                    {
                        res.code = Static_Data.DuplicateData.DuplicateName.Code;
                        res.status = Static_Data.DuplicateData.DuplicateName.Status;
                        res.message = Static_Data.DuplicateData.DuplicateName.MessageAr;
                    }
                    else
                    {
                        var newSubCat = new TblSubCategory()
                        {
                            id = dto.id,
                            subcat_name = dto.subcat_name,
                            cat_id = dto.cat_id
                        };
                        db.tblSubCategory.Add(newSubCat);
                        db.SaveChanges();
                        dto.id = newSubCat.id;
                        dto.subcat_name = newSubCat.subcat_name;
                        dto.cat_id = newSubCat.cat_id;
                        res.code = Static_Data.StaticApiStatus.ApiSaveSuccess.Code;
                        res.status = Static_Data.StaticApiStatus.ApiSaveSuccess.Status;
                        res.message = Static_Data.StaticApiStatus.ApiSaveSuccess.MessageAr;
                        res.payload = dto;
                    }                         
                }
                else
                {
                    res.code = Static_Data.StaticApiStatus.ApiRequired.Code;
                    res.status = Static_Data.StaticApiStatus.ApiRequired.Status;
                    res.message = Static_Data.StaticApiStatus.ApiRequired.MessageAr;
                }
            }
            return res;
        }

        public Response<bool> Delete(int id)
        {
            Response<bool> response = new Response<bool>();

            bool deleted = false;
            if (id != 0)
            {
                var Subcat = db.tblSubCategory.Where(c => c.id == id).FirstOrDefault();
                db.tblSubCategory.Remove(Subcat);
                db.SaveChanges();
                deleted = true;
            }
            return response;
        }

        public dtoSubCategoryForAdd Edit(dtoSubCategoryForAdd dto)
        {
            if (dto != null)
            {
                var isExist = db.tblSubCategory.Where(c => c.id == dto.id).FirstOrDefault();
                if (isExist != null)
                {
                    isExist.subcat_name = dto.subcat_name;
                    isExist.cat_id = dto.cat_id;
                }
                db.SaveChanges();
                isExist.id = dto.id;
                isExist.subcat_name = dto.subcat_name;
                isExist.cat_id = dto.cat_id;
            }
            return dto;
        }

        public List<dtoSubCategoryForShow> Read()
        {
            var allSubCat = (from c in db.tblSubCategory select new dtoSubCategoryForShow()
                    {
                          id = c.id,
                          subcat_name = c.subcat_name,
                          cat_id=c.cat_id,
                          cat_name=c.TblCategory.cat_name
                            }).ToList();
            return allSubCat;
        }
        public List<dtoSubCategoryForShow> FilterSubCatOnCat(int cat)
        {
            var filteredPro =
               (from p in db.tblSubCategory
                .Where(p => p.cat_id == cat)
                select new dtoSubCategoryForShow()
                {
                    id = p.id,
                    subcat_name=p.subcat_name
                }).ToList();
            return filteredPro;
        }

    }
}
