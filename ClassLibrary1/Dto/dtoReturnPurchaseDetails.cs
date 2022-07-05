using System;
using System.Collections.Generic;
using System.Text;

namespace DtoReport.Dto
{
    public class dtoReturnPurchaseDetails
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
}
