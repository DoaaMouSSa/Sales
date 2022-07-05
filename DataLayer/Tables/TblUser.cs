using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Tables
{
   public class TblUser
    {
        public int id { get; set; }
        public string user_name { get; set; }
        public string password { get; set; }
        public int is_deleted { get; set; }
    }
}
