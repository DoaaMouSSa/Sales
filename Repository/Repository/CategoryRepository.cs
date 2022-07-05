using DataLayer.DBContext;
using DataLayer.Tables;
using Dto.Dto;
using IRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ExcelDataReader;
using System.Data;
using System.IO;
using System.Net;
using System.Web.Http;
using Microsoft.AspNetCore.Http;
using System.Web;
namespace Repository.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AccountDbContext db;
        public CategoryRepository(AccountDbContext _db)
        {
            db = _db;
        }
        public Response<dtoCategory> Add(dtoCategory cat)
        {
            Response<dtoCategory> res = new Response<dtoCategory>();
            if (cat != null)
            {
                if(cat.cat_name != "")
                {
                    if (db.tblCategory.Any(c => c.cat_name == cat.cat_name))
                    {
                        res.code = Static_Data.DuplicateData.DuplicateName.Code;
                        res.status = Static_Data.DuplicateData.DuplicateName.Status;
                        res.message = Static_Data.DuplicateData.DuplicateName.MessageAr;
                    }
                    else
                    {
                        var newcat = new TblCategory()
                        {
                            id = cat.id,
                            cat_name = cat.cat_name
                        };
                        db.tblCategory.Add(newcat);
                        db.SaveChanges();
                        cat.id = newcat.id;
                        cat.cat_name = newcat.cat_name;
                        res.code = Static_Data.StaticApiStatus.ApiSaveSuccess.Code;
                        res.status = Static_Data.StaticApiStatus.ApiSaveSuccess.Status;
                        res.message = Static_Data.StaticApiStatus.ApiSaveSuccess.MessageAr;
                        res.payload = cat;
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

        public dtoCategory Edit(dtoCategory cat)
        {
            if (cat != null)
            {
                var isExist = db.tblCategory.Where(c => c.id == cat.id).FirstOrDefault();
                if (isExist != null)
                {
                    isExist.cat_name = cat.cat_name;
                }
                db.SaveChanges();
                isExist.id = cat.id;
                isExist.cat_name = cat.cat_name;
            }
            return cat;
        }

        public List<dtoCategory> Read()
        {
            var allcat = (from c in db.tblCategory
                          select new dtoCategory()
                                       {
                                      id = c.id,
                                      cat_name = c.cat_name
                                      }).ToList();
                                          return allcat;
        }



        public Response<bool> Delete(int id)
        {
            Response<bool> response = new Response<bool>();
            bool isDeleted = false;
            if (id != 0)
            {
                var isExist = db.tblCategory.Where(c => c.id == id).FirstOrDefault();
                if (isExist != null)
                {
                    isExist.is_deleted = 1;
                }
                db.SaveChanges();
               
            }
            return response;
         
        }

      
    }
    }



