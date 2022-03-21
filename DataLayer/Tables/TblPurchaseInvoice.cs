using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Tables
{
   public class TblPurchaseInvoice
    {
        public int id { get; set; }
        public float total { get; set; }
        public DateTime purchase_Added_Time { get; set; }
        [ForeignKey(nameof(TblStore))]
        public int store_id { get; set; }
        public virtual TblStore TblStore { get; set; }
        public virtual List<TblPurchaseInvoiceDetails> PurchaseInvoiceDetailsLst { get; set; }
    }
}
