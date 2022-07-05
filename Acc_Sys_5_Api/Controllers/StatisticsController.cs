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
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsRepository StatisticsRepository;

        public StatisticsController(IStatisticsRepository _StatisticsRepository)
        {
            StatisticsRepository = _StatisticsRepository;
        }
        [Route("GetCountOfSupplier")]
        [HttpGet]
        public Response<int> GetCountOfSupplier()
        {
            var response = StatisticsRepository.GetCountSupplier();
            return response;
        }
        [Route("GetCountOfCategory")]
        [HttpGet]
        public Response<int> GetCountOfCategory()
        {
            var response = StatisticsRepository.GetCountCategory();
            return response;
        }
        [Route("GetCountOfSubCategory")]
        [HttpGet]
        public Response<int> GetCountOfSubCategory()
        {
            var response = StatisticsRepository.GetCountSubCategory();
            return response;
        }
        [Route("GetCountOfProduct")]
        [HttpGet]
        public Response<int> GetCountOfProduct()
        {
            var response = StatisticsRepository.GetCountProduct();
            return response;
        }
        [Route("GetCountClient")]
        [HttpGet]
        public Response<int> GetCountClient()
        {
            var response = StatisticsRepository.GetCountClient();
            return response;
        }
    }
}
