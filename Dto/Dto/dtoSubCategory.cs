using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Dto
{

    public class dtoSubCategoryForAdd
        {
        public int id { get; set; }
        public string subcat_name { get; set; }
            public int? cat_id { get; set; }
        }
        public class dtoSubCategoryForShow
        {
        public int id { get; set; }
        public string subcat_name { get; set; }
            public int? cat_id { get; set; }
            public string cat_name { get; set; }
        }
    
    
}
