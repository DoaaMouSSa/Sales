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
    public class dtoPurchaseDetialsForShow
    {
        public int id { get; set; }
        public int product_id { get; set; }
        public string product_name { get; set; }
        public int qty { get; set; }
        public float purchase_price_one_product { get; set; }
        public float total_purchase_price_one_product { get; set; }
        public int purchase_inv_id { get; set; }
        public string notes { get; set; }
    }
    public class dtoPurchaseInvForShow
    {
        public int id { get; set; }
        public int pur_inv_code { get; set; }
        public float invoice_total { get; set; }
        public float discount { get; set; }
        public float tax { get; set; }
        public float tax_discount { get; set; }
        public float final_total { get; set; }

        public int supplier_id { get; set; }
        public string supplier_name { get; set; }
        public int store_id { get; set; }
        public string store_name { get; set; }
        public DateTime purchase_Added_Time { get; set; }
        public string purchase_Added_Time_ar { get; set; }
        public int IsDeleted { get; set; }
        public List<dtoPurchaseDetialsForShow> lstProducts { get; set; }

    }
   
}
