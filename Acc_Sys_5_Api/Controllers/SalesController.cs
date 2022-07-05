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
    public class SalesController : ControllerBase
    {
        private readonly ISalesRepository salesRepository;

        public SalesController(ISalesRepository _salesRepository)
        {
                salesRepository = _salesRepository;
        }
        [Route("GetTotalAfterDiscounts")]
        [HttpGet]
        public Response<dtoInvoiceNumbers> GetTotalAfterDiscounts(decimal total, decimal discountNum, decimal discountPer, decimal tax, decimal taxM)
        {
            var response = salesRepository.GetTotalAfterDiscounts(total, discountNum, discountPer, tax, taxM);
            return response;
        }
        [Route("AddSales")]
        [HttpPost]
        public Response<dtoSalesInvDetails> AddSales(dtoSalesInvDetails dto)
        {
            var response = salesRepository.Add(dto);
            return response;
        }
        [Route("GetMaxSalesCode")]
        [HttpGet]
        public Response<int> GetMaxSalesCode()
        {
            var response = salesRepository.GetMaxCode();
            return response;
        }
        [Route("GetMinMaxInvCode")]
        [HttpGet]
        public Tuple<int, int> getMinMaxCode()
        {
            var response = salesRepository.getMinMaxCode();
            return response;
        }
        [Route("GetAllSales")]
        [HttpGet]
        public Response<List<dtoSalesInvForShow>> GetAllSales()
        {
            var allsales = salesRepository.Read();
            return allsales;
        }
        [Route("GetCustomSalesInvoice")]
        [HttpGet]
        public Response<List<dtoSalesInvForShow>> GetCustomSalesInvoice(int sale_inv_id)
        {
            var sales = salesRepository.ReadSalesInvDetails(sale_inv_id);
            return sales;
        }
  
        [Route("FilterSalesInvoices")]
        [HttpGet]
        public Response<List<dtoSalesInvForShow>> FilterSalesInvoices(int customer_id,int store_id, DateTime from_date, DateTime to_date, int from_code, int to_code, int is_deleted)
        {
            var sales = salesRepository.FilterSalesInvoices(customer_id, store_id,from_date,to_date, from_code, to_code, is_deleted);
            return sales;
        }
        [Route("DeleteSales")]
        [HttpGet]
        public Response<bool> DeleteSales(int id)
        {
            var deleted = salesRepository.Delete(id);
            return deleted;
        }
        [Route("GetDeletedSales")]
        [HttpGet]
        public Response<List<dtoSalesInvForShow>> GetDeletedSales()
        {
            var response = salesRepository.GetDeletedSalesInvoices();
            return response;
        }
        [Route("GetNotDeletedSales")]
        [HttpGet]
        public Response<List<dtoSalesInvForShow>> GetNotDeletedSales()
        {
            var response = salesRepository.GetNotDeletedSalesInvoices();
            return response;
        }
    }
}
