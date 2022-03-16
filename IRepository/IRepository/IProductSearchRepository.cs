using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto.Dto;
namespace IRepository.IRepository
{
    public interface IProductSearchRepository
    {
        public List<dtoProductForFilter> FilterByName(string character,int sub_cat_id);
        public List<dtoProductForFilter> FilterByCode(string code, int sub_cat_id);
        public List<dtoProductForFilter> FilterByBarcode(string barcode,int sub_cat_id);
   
    }
}

