using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Dto
{
  public  class dtoSalesInvDetails
    {
        public int id { get; set; }
        public float invoice_total { get; set; }
        public int store_id { get; set; }
        public DateTime sales_Added_Time { get; set; }
        public List<dtoSalesForAdd> sales_invoice_details { get; set; }
    }
}
