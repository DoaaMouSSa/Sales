using DataLayer.DBContext;
using Dto.Dto;
using IRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly AccountDbContext db;
        public LoginRepository()
        {
            
        }
        public LoginRepository(AccountDbContext _db)
        {
            db = _db;
        }

        public  Response<bool> Login(dtoLogin dto)
        {
            Response<bool> res = new Response<bool>();
            bool isExist = db.tblUser.Any(u => u.user_name.Equals(dto.user_name) && u.password.Equals(dto.password));
            if (isExist)
            {
                res.code = Static_Data.StaticApiStatus.ApiSaveSuccess.Code;
                res.status = Static_Data.StaticApiStatus.ApiSaveSuccess.Status;
                res.message = Static_Data.StaticApiStatus.ApiSaveSuccess.MessageAr;
            }
            return res;
        }
    }

}
