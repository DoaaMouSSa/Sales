﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Tables
{
   public class TblSupplier
    {
        public int id { get; set; }
        public string supplier_name { get; set; }
        public string supplier_phone { get; set; }

        public int supplier_code { get; set; }
        public string supplier_address { get; set; }
    }
}
