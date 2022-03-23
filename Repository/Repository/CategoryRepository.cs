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
                        res.code = Static_Data.StaticApiStatus.ApiDuplicate.Code;
                        res.status = Static_Data.StaticApiStatus.ApiDuplicate.Status;
                        res.message = Static_Data.StaticApiStatus.ApiDuplicate.MessageAr;
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

        public bool Delete(int id)
        {
            bool deleted = false;
            if (id != 0)
            {
                var cat = db.tblCategory.Where(c => c.id == id).FirstOrDefault();
                db.tblCategory.Remove(cat);
                db.SaveChanges();
                deleted = true;
            }
            return deleted;
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

        public string ReadExcelFile()
        {
            return "...";

            // #region Variable Declaration  
            // string message = "";
            // //HttpResponseMessage ResponseMessage = null;
            //// var httpRequest = HttpContext.Current.Request;
            // DataSet dsexcelRecords = new DataSet();
            // IExcelDataReader reader = null;
            // IFormFile Inputfile = null;
            // Stream FileStream = null;
            // #endregion
            // #region Save Student Detail From Excel  
            // if (httpRequest.Files.Count > 0)
            // {
            //     Inputfile = httpRequest.Files[0];
            //     FileStream = Inputfile.OpenReadStream();

            //     if (Inputfile != null && FileStream != null)
            //     {
            //         if (Inputfile.FileName.EndsWith(".xls"))
            //             reader = ExcelReaderFactory.CreateBinaryReader(FileStream);
            //         else if (Inputfile.FileName.EndsWith(".xlsx"))
            //             reader = ExcelReaderFactory.CreateOpenXmlReader(FileStream);
            //         else
            //             message = "The file format is not supported.";

            //         dsexcelRecords = reader.AsDataSet();
            //         reader.Close();

            //         if (dsexcelRecords != null && dsexcelRecords.Tables.Count > 0)
            //         {
            //             DataTable dt = dsexcelRecords.Tables[0];
            //             for (int i = 0; i < dt.Rows.Count; i++)
            //             {
            //                 dtoCategory objCateogy = new dtoCategory();
            //                 objCateogy.cat_name = (dt.Rows[i][0]).ToString();
            //             }

            //             int output = db.SaveChanges();
            //             if (output > 0)
            //                 message = "The Excel file has been successfully uploaded.";
            //             else
            //                 message = "Something Went Wrong!, The Excel file uploaded has fiald.";
            //         }
            //         else
            //             message = "Selected file is empty.";
            //     }
            //     else
            //         message = "Invalid File.";
            // }
            // else
            //     message = "bad request";
              }
        }
    }



