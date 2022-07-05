using Dto.Dto;
using IRepository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Acc_Sys_5_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStoreRepository storeRepository;
        public StoreController(IStoreRepository _storeRepository)
        {
            storeRepository = _storeRepository;
        }
        [Route("AddStore")]
        [HttpPost]
        public Response<dtoStore> AddStore(dtoStore dto)
        {
            var newStore = storeRepository.Add(dto);
            return newStore;
        }
        [Route("EditStore")]
        [HttpPost]
        public dtoStore EditStore(dtoStore dto)
        {
            var store = storeRepository.Edit(dto);
            return store;
        }
        [Route("DeleteStore")]
        [HttpGet]
        public Response<bool> DeleteStore(int id)
        {
            var deleted = storeRepository.Delete(id);
            return deleted;
        }
        [Route("GetAllStore")]
        [HttpGet]
        //[BasicAuthentication]
        public Response<List<dtoStore>> GetAllStore()
        {
            //string Role = Thread.CurrentPrincipal.Identity.Name;
            //if (Role == "admin")
            //{
            //    var allStore = storeRepository.Read();
            //    return allStore;
            //}
            //else
            //{
            var allStore = storeRepository.Read();
              return allStore;
            //}
        }
    }
}
