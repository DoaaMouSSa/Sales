using Dto.Dto;
using IRepository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Dto.Dto.dtoProductFilterStore;

namespace Acc_Sys_5_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreDetailsController : ControllerBase
    {
        private readonly IStoreFilterRepository storeFilterRepository;
        public StoreDetailsController(IStoreFilterRepository _storeFilterRepository)
        {
            storeFilterRepository = _storeFilterRepository;
        }
        [Route("GetAllCatsByStore")]
        [HttpGet]
        public Response<List<dtoCategory>> GetAllStore(int store_id)
        {

            var all = storeFilterRepository.GetCatsDependOnStore(store_id);
            return all;
          
        }
        [Route("GetAllProductsFromMainStore")]
        [HttpGet]
        public Response<List<dtoProductsDataInStore>> getproductsMainStore()
        {

            var all = storeFilterRepository.GetAllproductFromMainStore();
            return all;

        }
        [Route("GetAllProductsFromSpecificStore")]
        [HttpGet]
        public Response<List<dtoProductsDataInStore>> getproducts(int store_id,int cat_id,int subcat_id)
        {

            var all = storeFilterRepository.GetAllproductSpecificStore(store_id,cat_id,subcat_id);
            return all;

        }
        [Route("GetAllProductsFromSpecificStoreSearchByName")]
        [HttpGet]
        public Response<List<dtoProductsDataInStore>> getproductsByNameInStore(int store_id, int cat_id, int subcat_id,string val)
        {

            var all = storeFilterRepository.GetAllproductSpecificStoreSearchByName(store_id, cat_id, subcat_id,val);
            return all;

        }
        [Route("GetAllProductsFromSpecificStoreSearchByCode")]
        [HttpGet]
        public Response<List<dtoProductsDataInStore>> getproductsByCodeInStore(int store_id, int cat_id, int subcat_id, string val)
        {

            var all = storeFilterRepository.GetAllproductSpecificStoreSearchByCode(store_id, cat_id, subcat_id, val);
            return all;

        }
        [Route("GetAllProductsFromSpecificStoreSearchByBarCode")]
        [HttpGet]
        public Response<List<dtoProductsDataInStore>> getproductsByBarcodeInStore(int store_id, int cat_id, int subcat_id, string val)
        {

            var all = storeFilterRepository.GetAllproductSpecificStoreSearchByBarCode(store_id, cat_id, subcat_id, val);
            return all;

        }
    }
}
