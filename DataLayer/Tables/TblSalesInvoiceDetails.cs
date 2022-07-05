using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Tables
{
    public class TblSalesInvoiceDetails
    {
        public int id { get; set; }

        [ForeignKey(nameof(TblProduct))]
        public int product_id { get; set; }
        public virtual TblProduct TblProduct { get; set; }

        [ForeignKey(nameof(TblSalesInvoice))]
        public int sale_inv_id { get; set; }
        public virtual TblSalesInvoice TblSalesInvoice { get; set; }
        public int qty { get; set; }
        public float sales_price_one_product { get; set; }
        public float total_sales_price_one_product { get; set; }
        public int is_deleted { get; set; }
        public DateTime? AddedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        [MaxLength(255)]
        public string notes { get; set; }
    }
}
