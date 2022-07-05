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
        public Response<bool> Delete(int id);
        public List<dtoSupplier> Read();
        public Response<List<dtoSupplier>> SearchSupplier(string val);
        public Response<List<dtoSupplierDD>> ReadForDD();
    }
}
