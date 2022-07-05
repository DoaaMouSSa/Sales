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
        public Response<bool> DeleteProduct(int id)
        {
            Response<bool> response = new Response<bool>();
            
            var deleted = prouductRepository.Delete(id);
            return response;
        }
        [Route("GetAllProduct")]
        [HttpGet]
        public List<dtoProductForShow> GetAllProduct()
        {
            var allpro = prouductRepository.Read();
            return allpro;
        }
        [Route("GetAllProductBeforeAddToInvoice")]
        [HttpGet]
        public Response<List<dtoProductForShowBeforeAddToInvoice>> GetAllProductBeforeAddToInvoice(string character, int store_id)
        {
            var allpro = prouductRepository.ReadForInvoiceAdd(character, store_id);
            return allpro;
        }

    }
}
