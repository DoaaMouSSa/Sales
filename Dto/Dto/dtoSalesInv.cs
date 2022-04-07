using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Dto
{
        public class dtoSalesForAdd
        {
            public int id { get; set; }
            public int product_id { get; set; }
            public int qty { get; set; }
            public float sales_price_one_product { get; set; }
            public float total_sales_price_one_product { get; set; }
            public int sales_inv_id { get; set; }
            public string notes { get; set; }
        }
        public class dtoSalesForShow
        {
            public int id { get; set; }
            public int product_id { get; set; }
            public string product_name { get; set; }
            public int store_id { get; set; }
            public string store_name { get; set; }
            public float product_price { get; set; }
            public int qty { get; set; }
            public float sales_price_one_product { get; set; }
            public float total_sales_price_one_product { get; set; }
            public string notes { get; set; }
    }
}
