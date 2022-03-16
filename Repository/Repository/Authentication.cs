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
   public class Authentication: IAuthentication
    {

        private readonly AccountDbContext db;
        public Authentication(AccountDbContext _db)
        {
            db = _db;
        }
        public Authentication( )
        {
        }
        public bool Login(dtoUser dto)
        {
            return db.tblUser.Any(user => user.user_name==dto.user_name
            && user.password == dto.password);

        }
    }

}