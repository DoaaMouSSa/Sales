using Dto.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepository.IRepository
{
   public interface ICategoryRepository
    {
        public Response<dtoCategory> Add(dtoCategory cat);
        public dtoCategory Edit(dtoCategory cat);
        public Response<bool> Delete(int id);
        public List<dtoCategory> Read();

    }
}
