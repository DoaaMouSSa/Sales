using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Dto
{
  public  class dtoSalesReturnInvDetails
    {
        public int id { get; set; }
        public float invoice_return_total { get; set; }
        public float discount { get; set; }
        public float tax { get; set; }
        public float tax_discount { get; set; }
        public float final_total { get; set; }
        public int sale_return_inv_code { get; set; }
        public int store_id { get; set; }
        public int? client_id { get; set; }
        public string store_name { get; set; }
        public string client_name { get; set; }
        public DateTime sales_Added_Time { get; set; }

        public List<dtoSalesReturnForAdd> sales_return_invoice_details { get; set; }
    }
}
