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
    public class SalesReturnController : ControllerBase
    {
        private readonly ISalesReturnRepository salesReturnRepository;

        public SalesReturnController(ISalesReturnRepository _salesReturnRepository)
        {
            salesReturnRepository = _salesReturnRepository;

        }
        [Route("GetTotalAfterDiscounts")]
        [HttpGet]
        public Response<dtoInvoiceNumbers> GetTotalAfterDiscounts(decimal total, decimal discountNum, decimal discountPer, decimal tax, decimal taxM)
        {
            var response = salesReturnRepository.GetTotalAfterDiscounts(total, discountNum, discountPer, tax, taxM);
            return response;
        }
        [Route("AddSalesReturnInvoice")]
        [HttpPost]
        public Response<dtoSalesReturnInvDetails> AddSalesReturnInvoice(dtoSalesReturnInvDetails dto)
        {
            var response = salesReturnRepository.AddSalesReturnInvoice(dto);
            return response;
        }
        [Route("GetMaxReturnSalesCode")]
        [HttpGet]
        public Response<int> GetMaxReturnSalesCode()
        {
            var response = salesReturnRepository.GetMaxReturnSalesCode();
            return response;
        }
        [Route("GetAllReturnSales")]
        [HttpGet]
        public Response<List<dtoSalesReturnInvForShow>> GetAllReturnSales()
        {
            var allsales = salesReturnRepository.ReadSummeryReturnSalesInv();
            return allsales;
        }
        [Route("ReadSalesReturnInvWithDetails")]
        [HttpGet]
        public Response<List<dtoSalesReturnInvForShow>> ReadSalesReturnInvWithDetails(int sale_inv_id)
        {
            var sales = salesReturnRepository.ReadSalesReturnInvWithDetails(sale_inv_id);
            return sales;
        }
        [Route("ReadCustomSalesReturnInvoiceForReport")]
        [HttpGet]
        public List<dtoSalesReturnInvForShow> ReadCustomSalesReturnInvoiceForReport(int sale_inv_id)
        {
            var sales = salesReturnRepository.ReadCustomSalesReturnInvoiceForReport(sale_inv_id);
            return sales;
        }
        [Route("FilterSalesReturnInvoices")]
        [HttpGet]
        public Response<List<dtoSalesReturnInvForShow>> FilterSalesReturnInvoices(int customer_id,int store_id, DateTime from_date, DateTime to_date)
        {
            var sales = salesReturnRepository.FilterSalesReturnInvoices(customer_id, store_id,from_date,to_date);
            return sales;
        }
        [Route("DeleteReturnSales")]
        [HttpGet]
        public Response<bool> DeleteSales(int id)
        {
            var deleted = salesReturnRepository.Delete(id);
            return deleted;
        }
        [Route("GetDeletedSalesReturnInvoices")]
        [HttpGet]
        public Response<List<dtoSalesReturnInvForShow>> GetDeletedSalesReturnInvoices()
        {
            var response = salesReturnRepository.GetDeletedSalesReturnInvoices();
            return response;
        }
        [Route("GetNotDeletedSales")]
        [HttpGet]
        public Response<List<dtoSalesReturnInvForShow>> GetNotDeletedReturnSalesInvoices()
        {
            var response = salesReturnRepository.GetNotDeletedReturnSalesInvoices();
            return response;
        }
    }
}
