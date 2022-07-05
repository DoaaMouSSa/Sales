using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayer.Tables
{
    public class TblSubCategory
    {
        public int id { get; set; }
        public string subcat_name { get; set; }
        [ForeignKey(nameof(TblCategory))]
        public int? cat_id { get; set; }
        public int is_deleted { get; set; }
        public DateTime? AddedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public virtual TblCategory TblCategory { get; set; }
        public virtual List<TblProduct> TblProduct { get; set; }
    }
}
