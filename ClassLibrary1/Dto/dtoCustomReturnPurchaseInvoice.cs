using System;
using System.Collections.Generic;
using System.Text;

namespace DtoReport.Dto
{
    public class dtoCustomReturnPurchaseInvoice
    {
        public int id { get; set; }
        public float invoice_total { get; set; }
        public float discount { get; set; }
        public float tax { get; set; }
        public float tax_discount { get; set; }
        public float final_total { get; set; }
        public int purchase_inv_code { get; set; }
        public int? supplier_id { get; set; }
        public string supplier_name { get; set; }
        public int store_id { get; set; }
        public string store_name { get; set; }
        public DateTime purchase_Added_Time { get; set; }
        public List<dtoReturnPurchaseDetails> purchase_invoice_details { get; set; }
    }
}
