using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Dto
{
    public class dtoStore
    {
        public int id { get; set; }
        public string store_name { get; set; }
       
    }
    public class dtoStoreDetails
    {
        public int id { get; set; }
        public int product_id { get; set; }
        public int qty { get; set; }    
        public int store_id { get; set; }

    }
}

