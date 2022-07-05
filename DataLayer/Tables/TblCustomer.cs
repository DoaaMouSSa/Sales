using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Tables
{
    public class TblCustomer
    {
        public int id { get; set; }
        public string customer_name { get; set; }
        public string customer_phone { get; set; }
        public string customer_type { get; set; }
        public string customer_email { get; set; }
        public int customer_card { get; set; }
        public int customer_commercial_record { get; set; }
        public int customer_code { get; set; }
        public string customer_address { get; set; }
        public int is_deleted { get; set; }
        public DateTime? AddedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
