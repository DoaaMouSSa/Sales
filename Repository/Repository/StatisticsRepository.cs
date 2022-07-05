using DataLayer.DBContext;
using Dto.Dto;
using Repository.Static_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private readonly AccountDbContext db;
        public StatisticsRepository(AccountDbContext _db)
        {
            db = _db;
        }
        public Response<int> GetCountClient()
        {
            Response<int> res = new Response<int>();
            var countCustomer = db.TblCustomer.Count();
            if(countCustomer != 0)
            {
                res.code = StaticApiStatus.ApiSuccess.Code;
                res.status = StaticApiStatus.ApiSuccess.Status;
                res.payload = countCustomer;
            }
            else
            {
                res.code = StaticApiStatus.ApiNoCount.Code;
                res.status = StaticApiStatus.ApiNoCount.Status;
            }
            return res;
        }

        public Response<int> GetCountCategory()
        {
            Response<int> res = new Response<int>();
            var countCategory = db.tblCategory.Count();
            if (countCategory != 0)
            {
                res.code = StaticApiStatus.ApiSuccess.Code;
                res.status = StaticApiStatus.ApiSuccess.Status;
                res.payload = countCategory;
            }
            else
            {
                res.code = StaticApiStatus.ApiNoCount.Code;
                res.status = StaticApiStatus.ApiNoCount.Status;
            }
            return res;
        }

        public Response<int> GetCountProduct()
        {
            Response<int> res = new Response<int>();
            var countProduct = db.tblProduct.Count();
            if (countProduct != 0)
            {
                res.code = StaticApiStatus.ApiSuccess.Code;
                res.status = StaticApiStatus.ApiSuccess.Status;
                res.payload = countProduct;
            }
            else
            {
                res.code = StaticApiStatus.ApiNoCount.Code;
                res.status = StaticApiStatus.ApiNoCount.Status;
            }
            return res;
        }

        public Response<int> GetCountSubCategory()
        {
            Response<int> res = new Response<int>();
            var countSubCategory = db.tblSubCategory.Count();
            if (countSubCategory != 0)
            {
                res.code = StaticApiStatus.ApiSuccess.Code;
                res.status = StaticApiStatus.ApiSuccess.Status;
                res.payload = countSubCategory;
            }
            else
            {
                res.code = StaticApiStatus.ApiNoCount.Code;
                res.status = StaticApiStatus.ApiNoCount.Status;
            }
            return res;
        }

        public Response<int> GetCountSupplier()
        {
            Response<int> res = new Response<int>();
            var countSupplier = db.tblSupplier.Count();
            if (countSupplier != 0)
            {
                res.code = StaticApiStatus.ApiSuccess.Code;
                res.status = StaticApiStatus.ApiSuccess.Status;
                res.payload = countSupplier;
            }
            else
            {
                res.code = StaticApiStatus.ApiNoCount.Code;
                res.status = StaticApiStatus.ApiNoCount.Status;
            }
            return res;
        }
    }
}
