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
        public dtoStore Add(dtoStore dto);
        public dtoStore Edit(dtoStore dto);
        public bool Delete(int id);
        public List<dtoStore> Read();
    }
}
