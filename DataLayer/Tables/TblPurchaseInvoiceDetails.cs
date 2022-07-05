using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace DataLayer.Tables
{
   public class TblPurchaseInvoiceDetails
    {
        public int id { get; set; }

        [ForeignKey(nameof(TblProduct))]
        public int product_id { get; set; }
        public virtual TblProduct TblProduct { get; set; }
       
        [ForeignKey(nameof(TblPurchaseInvoice))]
        public int purchase_inv_id { get; set; }
        public virtual TblPurchaseInvoice TblPurchaseInvoice { get; set; }
        public int qty { get; set; }
        public float purchase_price_one_product { get; set; }
        public float total_purchase_price_one_product { get; set; }
        [MaxLength(255)]
        public string notes { get; set; }
        public int is_deleted { get; set; }
        public DateTime? AddedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
