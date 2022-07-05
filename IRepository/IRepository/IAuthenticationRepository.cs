using Dto.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepository.IRepository
{
   public interface IAuthenticationRepository
    {
        public Task<Token> Login(LoginModel dto);
        public Task<Response<RegisterModel>> Register(RegisterModel dto);
        public Response<RegisterModel> RegisterAdmin(RegisterModel dto);

    }
}
