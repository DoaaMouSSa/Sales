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
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository customerRepository;

        public CustomerController(ICustomerRepository _customerRepository)
        {
            customerRepository = _customerRepository;
        }
        [Route("AddCustomer")]
        [HttpPost]
        public Response<dtoCustomer> AddCustomer(dtoCustomer dto)
        {
            var response = customerRepository.Add(dto);
            return response;
        }
        [Route("EditCustomer")]
        [HttpPost]
        public dtoCustomer EditCustomer(dtoCustomer dto)
        {
            var response = customerRepository.Edit(dto);
            return response;
        }
        [Route("DeleteCustomer")]
        [HttpGet]
        public Response<bool> DeleteCustomer(int id)
        {
            var response = customerRepository.Delete(id);
            return response;
        }
        [Route("GetAllCustomer")]
        [HttpGet]
        public List<dtoCustomer> GetAllCustomer()
        {
            var response = customerRepository.Read();
            return response;
        }
        [Route("GetAllCustomerForDD")]
        [HttpGet]
        public Response<List<dtoCustomerDD>> GetAllCustomerForDD()
        {
            var response = customerRepository.ReadForDD();
            return response;
        }
        [Route("SearchCustomer")]
        [HttpGet]
        public Response<List<dtoCustomer>> SearchCustomer(string val)
        {
            var response = customerRepository.SearchCustomer(val);
            return response;
        }
    }
}
