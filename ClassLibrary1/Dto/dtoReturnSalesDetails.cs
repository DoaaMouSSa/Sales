using System;
using System.Collections.Generic;
using System.Text;

namespace DtoReport.Dto
{
    public class dtoReturnSalesDetails
    {
        public int id { get; set; }
        public int product_id { get; set; }
        public string product_name { get; set; }
        public int qty { get; set; }
        public float sales_price_one_product { get; set; }
        public float total_sales_price_one_product { get; set; }
        public int sales_inv_id { get; set; }
        public string notes { get; set; }
    }
}
