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

        public int customer_code { get; set; }
        public string customer_address { get; set; }
    }
}
