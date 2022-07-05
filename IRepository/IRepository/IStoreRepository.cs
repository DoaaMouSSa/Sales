using Dto.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepository.IRepository
{
    public interface IStoreRepository
    {
        public Response<dtoStore> Add(dtoStore dto);
        public dtoStore Edit(dtoStore dto);
        public Response<bool> Delete(int id);
        public Response<List<dtoStore>> Read();
    }
}
