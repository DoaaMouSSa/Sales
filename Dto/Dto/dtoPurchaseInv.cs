using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Dto
{
    public class dtoPurchaseForAdd
    {
        public int id { get; set; }
        public int product_id { get; set; }    
        public int qty { get; set; }
        public float purchase_price_one_product { get; set; }
        public float total_purchase_price_one_product { get; set; }
        public int purchase_inv_id { get; set; }
        public string notes { get; set; }
    }
    public class dtoPurchaseForShow
    {
        public int id { get; set; }
        public int product_id { get; set; }
        public string product_name { get; set; }
        public int store_id { get; set; }
        public string store_name { get; set; }
        public float product_price { get; set; }
        public int qty { get; set; }
        public float purchase_price_one_product { get; set; }
        public float total_purchase_price_one_product { get; set; }
        public string notes { get; set; }
      
       
    }
   
}
