using Dto.Dto;
using IRepository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Acc_Sys_5_Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationRepository _authenticationRepository;
        public AuthenticationController(IAuthenticationRepository authenticationRepository)
        {
            _authenticationRepository = authenticationRepository;
        }
        [Route("Register")]
        [HttpPost]
        public async Task<Response<RegisterModel>> Register([FromBody]RegisterModel dto)
        {
            var response = await _authenticationRepository.Register(dto);
            return response;
        }
        [Route("Login")]
        [HttpPost]
        public async Task<Token> Login([FromBody] LoginModel dto)
        {
            var response = await _authenticationRepository.Login(dto);
            return response;
        }
    }
}
