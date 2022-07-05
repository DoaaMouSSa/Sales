using Dto.Dto;
using IRepository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Acc_Sys_5_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        private readonly ISubCategoryRepository subCategoryRepository;
        public SubCategoryController(ISubCategoryRepository _subCategoryRepository)
        {
            subCategoryRepository = _subCategoryRepository;
        }
        [Route("AddSubCategory")]
        [HttpPost]
        public Response<dtoSubCategoryForAdd> AddCat(dtoSubCategoryForAdd dto)
        {
            var newSubcat = subCategoryRepository.Add(dto);
            return newSubcat;
        }
        [Route("EditSubCategory")]
        [HttpPost]
        public dtoSubCategoryForAdd EditCat(dtoSubCategoryForAdd dto)
        {
            var cat = subCategoryRepository.Edit(dto);
            return cat;
        }
        [Route("DeleteSubCategory")]
        [HttpGet]
        public Response<bool> DeleteCat(int id)
        {
            var deleted = subCategoryRepository.Delete(id);
            return deleted;
        }
        [Route("GetAllSubCategory")]
        [HttpGet]
        public List<dtoSubCategoryForShow> GetAllCat()
        {
            var allcat = subCategoryRepository.Read();
            return allcat;
        }
        [Route("FilterSubCatOnCat")]
        [HttpGet]
        public List<dtoSubCategoryForShow> FilterSubCatOnCat(int id)
        {
            var allcat = subCategoryRepository.FilterSubCatOnCat(id);
            return allcat;
        }
    }
}
