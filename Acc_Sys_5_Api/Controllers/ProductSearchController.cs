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
    public class ProductSearchController : ControllerBase
    {
        private readonly IProductSearchRepository productSearchRepository;
        public ProductSearchController(IProductSearchRepository _productSearchRepository)
        {
            productSearchRepository = _productSearchRepository;
        }
        [HttpGet]
        [Route("SearchByName")]
        public List<dtoProductForFilter> FilterProductByName(string character,int sub_cat_id)
        {
            var Pro = productSearchRepository.FilterByName(character, sub_cat_id);
            return Pro;
        }
        [HttpGet]
        [Route("SearchByCode")]
        public List<dtoProductForFilter> FilterProductByCode(string character, int sub_cat_id)
        {
            var Pro = productSearchRepository.FilterByCode(character, sub_cat_id);
            return Pro;
        }
        [HttpGet]
        [Route("SearchByBarcode")]
        public List<dtoProductForFilter> FilterProductByBarcode(string character, int sub_cat_id)
        {
            var Pro = productSearchRepository.FilterByBarcode(character, sub_cat_id);
            return Pro;
        }
    }
}
