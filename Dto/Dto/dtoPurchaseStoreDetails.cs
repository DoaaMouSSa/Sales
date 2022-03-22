using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Dto
{
    public class dtoPurchaseStoreDetails
    {
        public int id { get; set; }
        public float invoice_total { get; set; }
        public int store_id { get; set; }
        public DateTime purchase_Added_Time { get; set; }
        public List<dtoPurchaseForAdd> purchase_invoice_details { get; set; }
    }
}
