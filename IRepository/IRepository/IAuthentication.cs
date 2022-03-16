using Dto.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepository.IRepository
{
  public  interface IAuthentication
    {
        public bool Login(dtoUser dto);
    }
}
