using System;
using System.Collections.Generic;
using System.Text;

namespace DtoReport.Dto
{
   public class dtoCustomReturnSalesInvoice
    {
            public int id { get; set; }
            public float invoice_total { get; set; }
            public float discount { get; set; }
            public float tax { get; set; }
            public float tax_discount { get; set; }
            public float final_total { get; set; }
            public int sale_inv_code { get; set; }
            public int store_id { get; set; }
            public int? client_id { get; set; }
            public string store_name { get; set; }
            public string client_name { get; set; }
            public DateTime sales_Added_Time { get; set; }
            public List<dtoSalesReturnDetails> sales_invoice_details { get; set; }
        
    }
   
}
