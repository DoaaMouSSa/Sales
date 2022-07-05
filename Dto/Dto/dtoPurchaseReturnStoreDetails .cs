using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Dto
{
    public class dtoPurchaseReturnStoreDetails
    {
        public int id { get; set; }
        public float invoice_total { get; set; }
        public float discount { get; set; }
        public float tax { get; set; }
        public float tax_discount { get; set; }
        public float final_total { get; set; }

        public int pur_return_inv_code { get; set; }

        public int store_id { get; set; }
        public int supplier_id { get; set; }
        
        public DateTime purchase_Added_Time { get; set; }
     
        public List<dtoPurchaseReturnForAdd> purchase_return_invoice_details { get; set; }
    }
}
