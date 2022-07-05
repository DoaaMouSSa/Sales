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
    public class PurchaseReturnController : ControllerBase
    {
        private readonly IPurchaseReturnRepository purchaseReturnRepository;
       
        public PurchaseReturnController(IPurchaseReturnRepository _purchaseReturnRepository)
        {
            purchaseReturnRepository = _purchaseReturnRepository;
           
        }
        [Route("AddPurchaseReturnInvoice")]
        [HttpPost]
        public Response<dtoPurchaseReturnStoreDetails> AddPurchaseReturnInvoice(dtoPurchaseReturnStoreDetails dto)
        {
           var response = purchaseReturnRepository.AddPurchaseReturnInvoice(dto);
            return response;
        }
        [Route("GetTotalAfterDiscounts")]
        [HttpGet]
        public Response<dtoInvoiceNumbers> GetTotalAfterDiscounts(decimal  total, decimal  discountNum, decimal  discountPer, decimal  tax, decimal  taxM)
        {
            var response = purchaseReturnRepository.GetTotalAfterDiscounts(total, discountNum, discountPer, tax, taxM);
            return response;
        }
        [Route("GetMaxPurchaseCode")]
        [HttpGet]
        public Response<int> GetMaxPurchaseCode()
        {
            var response = purchaseReturnRepository.GetMaxCode();
            return response;
        }
      
        [Route("DeleteReturnPurchase")]
        [HttpGet]
        public Response<bool> DeletePurchase(int id)
        {
            var deleted = purchaseReturnRepository.Delete(id);
            return deleted;
        }
        [Route("ReadAllPurchaseReturnInvoices")]
        [HttpGet]
        public Response<List<dtoPurchaseInvReturnForShow>> ReadAllPurchaseReturnInvoices()
        {
            var allpurchase = purchaseReturnRepository.ReadAllPurchaseReturnInvoices();
            return allpurchase;
        }
        [Route("GetDeletedPurchaseReturnInvoices")]
        [HttpGet]
        public Response<List<dtoPurchaseInvReturnForShow>> GetDeletedPurchaseReturnInvoices()
        {
            var purchase = purchaseReturnRepository.GetDeletedPurchaseReturnInvoices();
            return purchase;
        }
        [Route("GetNotDeletedPurchaseReturnInvoices")]
        [HttpGet]
        public Response<List<dtoPurchaseInvReturnForShow>> GetNotDeletedPurchaseReturnInvoices()
        {
            var purchase = purchaseReturnRepository.GetNotDeletedPurchaseReturnInvoices();
            return purchase;
        }
        [Route("ReadCustomPurchaseReturnInvoice")]
        [HttpGet]
        public Response<List<dtoPurchaseInvReturnForShow>> ReadCustomPurchaseReturnInvoice(int pur_return_inv_id)
        {
            var purchase = purchaseReturnRepository.ReadCustomPurchaseReturnInvoice(pur_return_inv_id);
            return purchase;
        }
        [Route("ReadCustomPurchaseReturnInvoiceForReport")]
        [HttpGet]
        public List<dtoPurchaseInvReturnForShow> ReadCustomPurchaseReturnInvoiceForReport(int pur_return_inv_id)
        {
            var purchase = purchaseReturnRepository.ReadCustomPurchaseReturnInvoiceForReport(pur_return_inv_id);
            return purchase;
        }
        [Route("FilterPurchaseReturnInvoices")]
        [HttpGet]
        public Response<List<dtoPurchaseInvReturnForShow>> FilterPurchaseReturnInvoices(int supplier_id,int store_id, DateTime from_date, DateTime to_date)
        {
            var purchase = purchaseReturnRepository.FilterPurchaseReturnInvoices(supplier_id, store_id,from_date,to_date);
            return purchase;
        }
    }
}
