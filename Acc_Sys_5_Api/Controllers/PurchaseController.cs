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
        public Response<dtoPurchaseInvForShow> AddPurchase(dtoPurchaseStoreDetails dto)
        {
           var response =purchaseRepository.Add(dto);
            return response;
        }
        [Route("GetTotalAfterDiscounts")]
        [HttpGet]
        public Response<dtoInvoiceNumbers> GetTotalAfterDiscounts(decimal  total, decimal  discountNum, decimal  discountPer, decimal  tax, decimal  taxM)
        {
            var response = purchaseRepository.GetTotalAfterDiscounts(total, discountNum, discountPer, tax, taxM);
            return response;
        }
        [Route("GetMaxPurchaseCode")]
        [HttpGet]
        public Response<int> GetMaxPurchaseCode()
        {
            var response = purchaseRepository.GetMaxCode();
            return response;
        }
        [Route("GetMinMaxInvCode")]
        [HttpGet]
        public Tuple<int, int> getMinMaxCode()
        {
            var response = purchaseRepository.getMinMaxCode();
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
        public Response<bool> DeletePurchase(int id)
        {
            var deleted = purchaseRepository.Delete(id);
            return deleted;
        }
        [Route("GetAllPurchase")]
        [HttpGet]
        public Response<List<dtoPurchaseInvForShow>> GetAllPurchase()
        {
            var allpurchase = purchaseRepository.Read();
            return allpurchase;
        }
  
      
        [Route("GetDeletedPurchase")]
        [HttpGet]
        public Response<List<dtoPurchaseInvForShow>> GetDeletedPurchase()
        {
            var purchase = purchaseRepository.GetDeletedPurchaseInvoices();
            return purchase;
        }
        [Route("GetNotDeletedPurchase")]
        [HttpGet]
        public Response<List<dtoPurchaseInvForShow>> GetNotDeletedPurchase()
        {
            var purchase = purchaseRepository.GetNotDeletedPurchaseInvoices();
            return purchase;
        }
        [Route("GetCustomPurchaseInvoice")]
        [HttpGet]
        public Response<List<dtoPurchaseInvForShow>> GetCustomPurchaseInvoice(int pur_inv_id)
        {
            var purchase = purchaseRepository.ReadCustomPurchaseInvoice(pur_inv_id);
            return purchase;
        }
  
        [Route("FilterPurchaseInvoices")]
        [HttpGet]
        public Response<List<dtoPurchaseInvForShow>> FilterPurchaseInvoices(int supplier_id,int store_id, DateTime from_date, DateTime to_date, int from_code, int to_code, int is_deleted)
        {
            var purchase = purchaseRepository.FilterPurchaseInvoices(supplier_id, store_id,from_date,to_date,from_code,to_code,is_deleted);
            return purchase;
        }

    }
}
