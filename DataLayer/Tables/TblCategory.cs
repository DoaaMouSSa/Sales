using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayer.Tables
{
    public class TblCategory
    {
        public int id { get; set; }
     
        public string cat_name { get; set; }
        public DateTime? AddedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public int is_deleted { get; set; }
        public virtual List<TblSubCategory> TblSubCategory { get; set; }
    }
}
