using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Tables
{
   public class TblSalesInvoice
    {
        public int id { get; set; }
        public float total { get; set; }
        public DateTime sales_Added_Time { get; set; }
        [ForeignKey(nameof(TblStore))]
        public int store_id { get; set; }
        public virtual TblStore TblStore { get; set; }
        public virtual List<TblSalesInvoiceDetails> SalesInvoiceDetailsLst { get; set; }
    }
}
