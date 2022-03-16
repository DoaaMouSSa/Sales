using IRepository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto.Dto;
namespace Acc_Sys_5_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenicationController : ControllerBase
    {
        private readonly IAuthentication authentication;
        public AuthenicationController(IAuthentication _authentication)
        {
            authentication = _authentication;
        }
        [Route("Login")]
        [HttpPost]
        public bool Login(dtoUser dto)
        {
            var response = authentication.Login(dto);
            return response;
        }
    }
}
