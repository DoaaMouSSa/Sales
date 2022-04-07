using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto.Dto;
namespace IRepository.IRepository
{
   public interface ISubCategoryRepository
    {
        public Response<dtoSubCategoryForAdd> Add(dtoSubCategoryForAdd dto);
        public dtoSubCategoryForAdd Edit(dtoSubCategoryForAdd dto);
        public bool Delete(int id);
        public List<dtoSubCategoryForShow> Read();
        public List<dtoSubCategoryForShow> FilterSubCatOnCat(int cat);
    }
}
