using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayer.Tables
{
    public class TblCategory
    {
        public int id { get; set; }
        public string cat_name { get; set; }
        public virtual List<TblSubCategory> TblSubCategory { get; set; }
    }
}
