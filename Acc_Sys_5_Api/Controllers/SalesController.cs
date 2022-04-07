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
        [Route("AddSales")]
        [HttpPost]
        public string AddSales(dtoSalesInvDetails dto)
        {
            var response = salesRepository.Add(dto);
            return response;
        }
    }
}
