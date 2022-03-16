using DataLayer.DBContext;
using DataLayer.Tables;
using Dto.Dto;
using IRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto.Dto;
namespace Repository.Repository
{
    public class SubCategoryRepository : ISubCategoryRepository
    {
        private readonly AccountDbContext db;
        public SubCategoryRepository(AccountDbContext _db)
        {
            db = _db;
        }
        public dtoSubCategoryForAdd Add(dtoSubCategoryForAdd dto)
        {
            if (dto != null)
            {
                var newSubCat = new TblSubCategory()
                {
                    id = dto.id,
                    subcat_name = dto.subcat_name,
                    cat_id=dto.cat_id
                };
                db.tblSubCategory.Add(newSubCat);
                db.SaveChanges();
                dto.id = newSubCat.id;
                dto.subcat_name = newSubCat.subcat_name;
                dto.cat_id = newSubCat.cat_id;
            }
            return dto;
        }

        public bool Delete(int id)
        {
            bool deleted = false;
            if (id != 0)
            {
                var Subcat = db.tblSubCategory.Where(c => c.id == id).FirstOrDefault();
                db.tblSubCategory.Remove(Subcat);
                db.SaveChanges();
                deleted = true;
            }
            return deleted;
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
