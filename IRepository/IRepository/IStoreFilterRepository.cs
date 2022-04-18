using Dto.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepository.IRepository
{
   public interface IStoreFilterRepository
    {
        public Response<List<dtoCategory>> GetCatsDependOnStore(int store_id);
        public Response<List<dtoProductsDataInStore>> GetAllproductFromMainStore();
        public Response<List<dtoProductsDataInStore>> GetAllproductSpecificStore(int store_id,int cat_id,int subcat_id);
        public Response<List<dtoProductsDataInStore>> GetAllproductSpecificStoreSearchByName(int store_id, int cat_id, int subcat_id,string val);
        public Response<List<dtoProductsDataInStore>> GetAllproductSpecificStoreSearchByCode(int store_id, int cat_id, int subcat_id, string val);
        public Response<List<dtoProductsDataInStore>> GetAllproductSpecificStoreSearchByBarCode(int store_id, int cat_id, int subcat_id, string val);


    }
}
