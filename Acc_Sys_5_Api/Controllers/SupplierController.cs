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
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierRepository supplierRepository;

        public SupplierController(ISupplierRepository _supplierRepository)
        {
            supplierRepository = _supplierRepository;
        }
        [Route("AddSupplier")]
        [HttpPost]
        public Response<dtoSupplier> AddSupplier(dtoSupplier dto)
        {
            var response = supplierRepository.Add(dto);
            return response;
        }
        [Route("EditSupplier")]
        [HttpPost]
        public dtoSupplier EditSupplier(dtoSupplier dto)
        {
            var sup = supplierRepository.Edit(dto);
            return sup;
        }
        [Route("DeleteSupplier")]
        [HttpGet]
        public bool DeleteSupplier(int id)
        {
            var deleted = supplierRepository.Delete(id);
            return deleted;
        }
        [Route("GetAllSupplier")]
        [HttpGet]
        public List<dtoSupplier> GetAllSupplier()
        {
            var allsup = supplierRepository.Read();
            return allsup;
        }
    }
}
