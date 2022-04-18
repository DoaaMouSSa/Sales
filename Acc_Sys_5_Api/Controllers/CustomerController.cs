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
            var customer = customerRepository.Edit(dto);
            return customer;
        }
        [Route("DeleteCustomer")]
        [HttpGet]
        public bool DeleteCustomer(int id)
        {
            var deleted = customerRepository.Delete(id);
            return deleted;
        }
        [Route("GetAllCustomer")]
        [HttpGet]
        public List<dtoCustomer> GetAllCustomer()
        {
            var allcustomer = customerRepository.Read();
            return allcustomer;
        }
    }
}
