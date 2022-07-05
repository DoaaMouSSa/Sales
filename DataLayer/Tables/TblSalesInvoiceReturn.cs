using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Tables
{
   public class TblSalesInvoiceReturn
    {
        public int id { get; set; }
        public float total { get; set; }
        public float discount { get; set; }
        public float tax { get; set; }
        public float tax_discount { get; set; }
        public float final_total { get; set; }

        public int sale_return_inv_code { get; set; }
        public DateTime sales_return_Added_Time { get; set; }
        [ForeignKey(nameof(TblStore))]
        public int store_id { get; set; }
        public virtual TblStore TblStore { get; set; }
        [ForeignKey(nameof(TblCustomer))]
        public int? customer_id { get; set; }
        public virtual TblCustomer TblCustomer { get; set; }
        public int is_deleted { get; set; }
        public DateTime? AddedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public virtual List<TblSalesInvoiceReturnDetails> SalesInvoiceReturnDetailsLst { get; set; }
    }
}
