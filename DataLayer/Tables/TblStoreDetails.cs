using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Tables
{
    public class TblStoreDetails
    {
        public int id { get; set; }
        public int qty { get; set; }

        [ForeignKey(nameof(TblProduct))]
        public int product_id { get; set; }
        public virtual TblProduct TblProduct { get; set; }

        [ForeignKey(nameof(TblStore))]
        public int store_id { get; set; }
        public virtual TblStore TblStore { get; set; }
    }
}
