﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Tables
{
    public class TblStore
    {
        public int id { get; set; }
        public string store_name { get; set; }
    }
}

