using Dto.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepository.IRepository
{
    public interface ISupplierRepository
    {
        public Response<dtoSupplier> Add(dtoSupplier sup);
        public dtoSupplier Edit(dtoSupplier sup);
        public bool Delete(int id);
        public List<dtoSupplier> Read();
    }
}
