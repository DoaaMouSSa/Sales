using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Dto
{
   public class dtoSupplier
    {
        public int id { get; set; }
        public string supplier_name { get; set; }
        public string supplier_phone { get; set; }
        public string supplier_email { get; set; }
        public string supplier_type { get; set; }
        public int supplier_card { get; set; }
        public int supplier_commercial_record { get; set; }
        public int supplier_code { get; set; }
        public string supplier_address { get; set; }
    }
    public class dtoSupplierDD
    {
        public int id { get; set; }
        public string supplier_name { get; set; }
    }
}
