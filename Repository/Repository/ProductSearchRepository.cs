using DataLayer.DBContext;
using Dto.Dto;
using IRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class ProductSearchRepository : IProductSearchRepository
    {
        private readonly AccountDbContext db;
        public ProductSearchRepository(AccountDbContext _db)
        {
            db = _db;
        }
        public List<dtoProductForFilter> FilterByBarcode(string barcode, int sub_cat_id)
        {
            if (sub_cat_id != 0)
            {
                var filteredPro =
                 (from p in db.tblProduct
                  .Where(p => p.barcode.StartsWith(barcode) && p.sub_cat_id == sub_cat_id)
                  select new dtoProductForFilter()
                  {
                      id = p.id,
                      product_name = p.product_name,
                      code = p.code,
                      barcode = p.barcode,
                      purchase_price = p.purchase_price,
                      sale_price = p.sale_price,
                  }).ToList();
                return filteredPro;
            }
            else
            {
                var filteredPro =
                    (from p in db.tblProduct
                     .Where(p => p.barcode.StartsWith(barcode))
                     select new dtoProductForFilter()
                     {
                         id = p.id,
                         product_name = p.product_name,
                         code = p.code,
                         barcode = p.barcode,
                         purchase_price = p.purchase_price,
                         sale_price = p.sale_price,
                     }).ToList();
                return filteredPro;
            }
        }

      
        public List<dtoProductForFilter> FilterByCode(string code, int sub_cat_id)
        {
            if (sub_cat_id != 0)
            {
                var filteredPro =
                 (from p in db.tblProduct
                  .Where(p => p.code.Contains(code) && p.sub_cat_id == sub_cat_id)
                  select new dtoProductForFilter()
                  {
                      id = p.id,
                      product_name = p.product_name,
                      code = p.code,
                      barcode = p.barcode,
                      purchase_price = p.purchase_price,
                      sale_price = p.sale_price,
                  }).ToList();
                return filteredPro;
            }
            else
            {
                var filteredPro =
                    (from p in db.tblProduct
                     .Where(p => p.code.Contains(code))
                     select new dtoProductForFilter()
                     {
                         id = p.id,
                         product_name = p.product_name,
                         code = p.code,
                         barcode = p.barcode,
                         purchase_price = p.purchase_price,
                         sale_price = p.sale_price,
                     }).ToList();
                return filteredPro;
            }
        }

       

        public List<dtoProductForFilter> FilterByName(string character, int sub_cat_id)
        {
            if (sub_cat_id !=0 )
            {
                var filteredPro =
                 (from p in db.tblProduct
                  .Where(p => p.product_name.Contains(character) && p.sub_cat_id==sub_cat_id)
                  select new dtoProductForFilter()
                  {
                      id = p.id,
                      product_name = p.product_name,
                      code = p.code,
                      barcode = p.barcode,
                      purchase_price = p.purchase_price,
                      sale_price = p.sale_price,
                  }).ToList();
                return filteredPro;
            }
            else
            {
                var filteredPro =
                    (from p in db.tblProduct
                     .Where(p => p.product_name.Contains(character))
                     select new dtoProductForFilter()
                     {
                         id = p.id,
                         product_name = p.product_name,
                         code = p.code,
                         barcode = p.barcode,
                         purchase_price = p.purchase_price,
                         sale_price = p.sale_price,
                     }).ToList();
                return filteredPro;
            }
        }

  
    }
}
