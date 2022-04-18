using Dto.Dto;
using IRepository.IRepository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Acc_Sys_5_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
 
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryController(ICategoryRepository _categoryRepository)
        {
            categoryRepository = _categoryRepository;
        }
        [Route("AddCategory")]
        [HttpPost]
        public Response<dtoCategory> AddCat(dtoCategory dto)
        {
         var response= categoryRepository.Add(dto);
            return response;
        }
        [Route("EditCategory")]
        [HttpPost]
        public dtoCategory EditCat(dtoCategory dto)
        {
            var cat = categoryRepository.Edit(dto);
            return cat;
        }
        [Route("DeleteCategory")]
        [HttpGet]
        public bool DeleteCat(int id)
        {
            var deleted = categoryRepository.Delete(id);
            return deleted;
        }
        [Route("GetAllCategory")]
        [HttpGet]
        public List<dtoCategory> GetAllCat()
        {      
                var allcat = categoryRepository.Read();
                return allcat;
        }
        [HttpPost]
        public string readexcel()
        {
                DataTable dt = new DataTable();

                using (StreamReader sr = new StreamReader("ExcelFiles/oneCatApi.xlsx"))
                {
                    string[] headers = sr.ReadLine().Split(',');
                    foreach (string header in headers)
                    {
                        dt.Columns.Add(header);
                    }
                    while (!sr.EndOfStream)
                    {
                        string[] rows = sr.ReadLine().Split(',');
                        DataRow dr = dt.NewRow();
                        for (int i = 0; i < headers.Length; i++)
                        {
                            dr[i] = rows[i];
                        }
                        dt.Rows.Add(dr);
                    }

                }
            var result = Newtonsoft.Json.JsonConvert.SerializeObject(dt);
            return result;

        }
    }
}
