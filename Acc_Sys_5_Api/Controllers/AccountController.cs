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
    public class AccountController : ControllerBase
    {
        private readonly ILoginRepository loginRepository;
        public AccountController(ILoginRepository _loginRepository)
        {
            loginRepository = _loginRepository;
        }
        [Route("Login")]
        [HttpPost]
        public Response<bool> Login(dtoLogin dto)
        {
            var response = loginRepository.Login(dto);
            return response;
        }
    }
}
