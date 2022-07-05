using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Tables
{
  public class TblProduct
    {
        public int id { get; set; }
        public string code { get; set; }
        //onlyNumber
        public string barcode { get; set; }
        public string product_name { get; set; }
        public float purchase_price { get; set; }
        public float sale_price { get; set; }
        [ForeignKey(nameof(TblSubCategory))]
        public int sub_cat_id { get; set; }
        public int is_deleted { get; set; }
        public DateTime? AddedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public virtual TblSubCategory TblSubCategory { get; set; }
    }
}
