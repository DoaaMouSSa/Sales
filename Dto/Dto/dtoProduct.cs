using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Dto
{
   public class dtoProductForAdd
    {
        public int id { get; set; }
        public string code { get; set; }
        public string barcode { get; set; }
        public string product_name { get; set; }
        public float purchase_price { get; set; }
        public float sale_price { get; set; }
        public int sub_cat_id { get; set; }
    }
    public class dtoProductForShow
    {
        public int id { get; set; }
        public string code { get; set; }
        public string barcode { get; set; }
        public string product_name { get; set; }
        public float purchase_price { get; set; }
        public float sale_price { get; set; }
        public int sub_cat_id { get; set; }
        public string sub_cat_name { get; set; }
    }
   public class dtoProductForFilter
    {
        public int id { get; set; }
        public string code { get; set; }
        public string barcode { get; set; }
        public string product_name { get; set; }
        public string sub_cat_name { get; set; }
        public float purchase_price { get; set; }
        public float sale_price { get; set; }
    }
}
