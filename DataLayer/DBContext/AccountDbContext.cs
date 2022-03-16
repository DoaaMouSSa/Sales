using DataLayer.Tables;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.DBContext
{
   public class AccountDbContext : DbContext
    {
        public AccountDbContext(DbContextOptions<AccountDbContext> options) : base(options)
        { }
        public DbSet<TblCategory> tblCategory { get; set; }
        public DbSet<TblSubCategory> tblSubCategory { get; set; }
        public DbSet<TblProduct> tblProduct { get; set; }
        public DbSet<TblUser> tblUser { get; set; }
        public DbSet<TblStore> tblStore { get; set; }
        public DbSet<TblPurchase> tblPurchase { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-7SI5D2G;Database=ACCDB;Trusted_Connection=True;MultipleActiveResultSets=true;");
        }
    }
}
