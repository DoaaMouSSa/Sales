using Dto.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepository.IRepository
{
   public interface ICustomerRepository
    {
        public Response<dtoCustomer> Add(dtoCustomer customer);
        public dtoCustomer Edit(dtoCustomer customer);
        public Response<bool> Delete(int id);
        public List<dtoCustomer> Read();
        public Response<List<dtoCustomer>> SearchCustomer(string val);

        public Response<List<dtoCustomerDD>> ReadForDD();

    }
}
