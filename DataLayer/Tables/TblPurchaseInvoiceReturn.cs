using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Tables
{
   public class TblPurchaseInvoiceReturn
    {
        public int id { get; set; }
        public float total { get; set; }
        public float discount { get; set; }
        public float tax { get; set; }
        public float tax_discount { get; set; }
        public float final_total { get; set; }
        public int pur_return_inv_code { get; set; }
        public DateTime purchase_return_Added_Time { get; set; }
        [ForeignKey(nameof(TblStore))]
        public int store_id { get; set; }
        public virtual TblStore TblStore { get; set; }
        [ForeignKey(nameof(TblSupplier))]
        public int supplier_id { get; set; }
        public virtual TblSupplier TblSupplier { get; set; }
        public int is_deleted { get; set; }
        public DateTime? AddedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public virtual List<TblPurchaseInvoiceReturnDetails> PurchaseInvoiceReturnDetailsLst { get; set; }
    }
}
