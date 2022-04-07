using Dto.Dto;
using IRepository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Acc_Sys_5_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProuductRepository prouductRepository;
        public ProductController(IProuductRepository _prouductRepository)
        {
            prouductRepository = _prouductRepository;
        }
        [Route("AddProduct")]
        [HttpPost]
        public Response<dtoProductForAdd> AddProduct(dtoProductForAdd dto)
        {
            var newPro = prouductRepository.Add(dto);
            return newPro;
        }
        [Route("EditProduct")]
        [HttpPost]
        public dtoProductForAdd EditProduct(dtoProductForAdd dto)
        {
            var pro = prouductRepository.Edit(dto);
            return pro;
        }
        [Route("DeleteProduct")]
        [HttpGet]
        public bool DeleteProduct(int id)
        {
            var deleted = prouductRepository.Delete(id);
            return deleted;
        }
        [Route("GetAllProduct")]
        [HttpGet]
        public List<dtoProductForShow> GetAllProduct()
        {
            var allpro = prouductRepository.Read();
            return allpro;
        }
    }
}
