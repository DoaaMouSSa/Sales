using DataLayer.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Main_Func
{
    public class Validation
    {
        private readonly AccountDbContext db;
        public Validation(AccountDbContext _db)
        {
            db = _db;
        }
        //public bool IsExist(object tbl,object field)
        //{
        //    db.tbl.Any(c => c.supplier_name == sup.supplier_name)
        //}
    }
}
