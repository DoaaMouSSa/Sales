using DataLayer.DBContext;
using Dto.Dto;
using IRepository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository.Static_Data;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly AccountDbContext db;
        private readonly UserManager<Dto.Dto.TblApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;
        public AuthenticationRepository(AccountDbContext _db, UserManager<Dto.Dto.TblApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            db = _db;
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
        }
        public async Task<Token> Login(LoginModel dto)
        {
            Token T = new Token();
            Response<LoginModel> res = new Response<LoginModel>();

            var user = await userManager.FindByNameAsync(dto.Username);
            if (user != null && await userManager.CheckPasswordAsync(user, dto.Password))
            {
                var userRoles = await userManager.GetRolesAsync(user);

               
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    T.role = userRole;
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );
                T.token = new JwtSecurityTokenHandler().WriteToken(token);
            }
            else
            {
                T.token = "0";
            }
            return T;

        }

        public async Task<Response<RegisterModel>> Register(RegisterModel dto)
        {
            Response<RegisterModel> res = new Response<RegisterModel>();
            var userExists = await userManager.FindByNameAsync(dto.Username);
            if (userExists != null)
            {
                res.code = Static_Data.StaticApiStatus.ApiDuplicate.Code;
                res.status = Static_Data.StaticApiStatus.ApiDuplicate.Status;
                res.message = Static_Data.StaticApiStatus.ApiDuplicate.MessageAr;
            }
            Dto.Dto.TblApplicationUser user = new Dto.Dto.TblApplicationUser()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = dto.Username
            };
            var result = await userManager.CreateAsync(user,dto.Password);
            if (result.Succeeded)
            {
                res.code = Static_Data.StaticApiStatus.ApiSuccess.Code;
                res.status = Static_Data.StaticApiStatus.ApiSuccess.Status;
                res.payload = dto;
                res.message = Static_Data.StaticApiStatus.ApiSuccess.MessageAr;
            }
            else
            {
                res.code = Static_Data.StaticApiStatus.ApiFaild.Code;
                res.status = Static_Data.StaticApiStatus.ApiFaild.Status;
                res.message = Static_Data.StaticApiStatus.ApiFaild.MessageAr;
            }
            return res;
        }

        private Response<RegisterModel> StatusCode<T>(object status500InternalServerError, Response<T> response)
        {
            throw new NotImplementedException();
        }

        public Response<RegisterModel> RegisterAdmin(RegisterModel dto)
        {
            throw new NotImplementedException();
        }
    }
}
