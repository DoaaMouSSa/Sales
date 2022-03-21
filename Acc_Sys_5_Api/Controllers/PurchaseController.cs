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
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseRepository purchaseRepository;
       
        public PurchaseController(IPurchaseRepository _purchaseRepository)
        {
            purchaseRepository = _purchaseRepository;
           
        }
        [Route("AddPurchase")]
        [HttpPost]
        public string AddPurchase(dtoPurchaseStoreDetails dto)
        {
           var response =purchaseRepository.Add(dto);
            return response;
        }
        //[Route("EditPurchase")]
        //[HttpPost]
        //public dtoPurchaseForShow EditPurchase(dtoPurchaseForAdd dto)
        //{
        //    var pur = purchaseRepository.Edit(dto);
        //    return pur;
        //}
        [Route("DeletePurchase")]
        [HttpGet]
        public bool DeleteCat(int id)
        {
            var deleted = purchaseRepository.Delete(id);
            return deleted;
        }
        //[Route("GetAllPurchase")]
        //[HttpGet]
        //public List<dtoPurchaseForShow> GetAllCat()
        //{
        //    var allpurchase = purchaseRepository.Read();
        //    return allpurchase;
        //}
     
    }
}
