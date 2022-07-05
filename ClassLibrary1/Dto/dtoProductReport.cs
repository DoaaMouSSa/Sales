using System;
using System.Collections.Generic;
using System.Text;

namespace DtoReport.Dto
{
 
        public class dtoProductReport
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
    
}
