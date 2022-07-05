using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Dto
{
   public class dtoProductsDataInStore
    {
        public int id { get; set; }
        public string product_name { get; set; }
        public int product_qty { get; set; }
        public string code { get; set; }
        public string barcode { get; set; }
        public float purchase_price { get; set; }
        public float sale_price { get; set; }
        public int sub_cat_id { get; set; }
        public string sub_cat_name { get; set; }
        public int store_id { get; set; }
        public string store_name { get; set; }

    }
}
