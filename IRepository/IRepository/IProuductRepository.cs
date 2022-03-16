using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto.Dto;

namespace IRepository.IRepository
{
   public interface IProuductRepository
    {
        public dtoProductForAdd Add(dtoProductForAdd dto);
        public dtoProductForAdd Edit(dtoProductForAdd dto);
        public bool Delete(int id);
        public List<dtoProductForShow> Read();
    }
}
