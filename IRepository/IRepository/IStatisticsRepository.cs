using Dto.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public interface IStatisticsRepository
    {
        public Response<int> GetCountCategory();
        public Response<int> GetCountSubCategory();
        public Response<int> GetCountProduct();
        public Response<int> GetCountSupplier();
        public Response<int> GetCountClient();
    }
}
