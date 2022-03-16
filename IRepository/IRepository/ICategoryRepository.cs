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
        public dtoCategory Add(dtoCategory cat);
        public dtoCategory Edit(dtoCategory cat);
        public bool Delete(int id);
        public List<dtoCategory> Read();
        public string ReadExcelFile();
    }
}
