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
    public class dtoSalesDetailsForShow
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
    public class dtoSalesInvForShow
        {
            public int id { get; set; }
        public int sale_inv_code { get; set; }
        public float invoice_total { get; set; }
        public float discount { get; set; }
        public float tax { get; set; }
        public float tax_discount { get; set; }
        public float final_total { get; set; }
        public int store_id { get; set; }
            public string store_name { get; set; }
        public int? client_id { get; set; }
        public string client_name { get; set; }
        public DateTime sales_Added_Time { get; set; }
        public string sales_Added_Time_ar { get; set; }
        public int IsDeleted { get; set; }
        public List<dtoSalesDetailsForShow> lstProducts { get; set; }

    }
}
