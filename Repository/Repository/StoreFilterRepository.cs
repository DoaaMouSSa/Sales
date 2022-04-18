using DataLayer.DBContext;
using Dto.Dto;
using IRepository.IRepository;
using Repository.Static_Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dto.Dto.dtoProductFilterStore;

namespace Repository.Repository
{
    public class StoreFilterRepository : IStoreFilterRepository
    {
        private readonly AccountDbContext db;
        public StoreFilterRepository(AccountDbContext _db)
        {
            db = _db;
        }

        public Response<List<dtoProductsDataInStore>> GetAllproductFromMainStore()
        {
            Response<List<dtoProductsDataInStore>> response = new Response<List<dtoProductsDataInStore>>();
            var products =
                         (from p in db.tblProduct
                         join s in db.tblStoreDetails on p.id equals s.product_id
                          where s.store_id == 1
                         select new dtoProductsDataInStore
                         {
                             id = p.id,
                             store_id=s.store_id,
                             store_name=s.TblStore.store_name,
                             sub_cat_id=p.TblSubCategory.id,
                             sub_cat_name=p.TblSubCategory.subcat_name,
                             product_name = p.product_name,
                             product_qty = s.qty,
                             code = p.code,
                             barcode = p.barcode,
                             sale_price = p.sale_price,
                             purchase_price = p.purchase_price,

                         }).ToList();
            if(products != null)
            {
                response.code = StaticApiStatus.ApiSuccess.Code;
                response.status = StaticApiStatus.ApiSuccess.Status;
                response.message = StaticApiStatus.ApiSuccess.MessageAr;
                response.payload = products;
                return response;
            }
            else
            {
                response.code = StaticApiStatus.ApiStoreEmpty.Code;
                response.status = StaticApiStatus.ApiStoreEmpty.Status;
                response.message = StaticApiStatus.ApiStoreEmpty.MessageAr;
            }
            return response;
        }

        public Response<List<dtoProductsDataInStore>> GetAllproductSpecificStore(int store_id,int cat_id,int subcat_id)
        {
            Response<List<dtoProductsDataInStore>> response = new Response<List<dtoProductsDataInStore>>();
            List<dtoProductsDataInStore> products = new List<dtoProductsDataInStore>();
            if (subcat_id==0 && cat_id==0)
            {
                 products =
                                        (from p in db.tblProduct
                                         join s in db.tblStoreDetails on p.id equals s.product_id
                                         where s.store_id == store_id
                                         select new dtoProductsDataInStore
                                         {
                                             id = p.id,
                                             store_id = s.store_id,
                                             store_name = s.TblStore.store_name,
                                             sub_cat_id = p.TblSubCategory.id,
                                             sub_cat_name = p.TblSubCategory.subcat_name,
                                             product_name = p.product_name,
                                             product_qty = s.qty,
                                             code = p.code,
                                             barcode = p.barcode,
                                             sale_price = p.sale_price,
                                             purchase_price = p.purchase_price
                                         }).ToList();
            }
            else if(cat_id !=0 && subcat_id==0)
            {
            
                                        products =
                                       (from p in db.tblProduct
                                        join s in db.tblStoreDetails on p.id equals s.product_id
                                        join sc in db.tblSubCategory on p.sub_cat_id equals sc.id
                                        where s.store_id == store_id
                                        where sc.cat_id == cat_id
                                        select new dtoProductsDataInStore
                                        {
                                            id = p.id,
                                            store_id = s.store_id,
                                            store_name = s.TblStore.store_name,
                                            sub_cat_id = p.TblSubCategory.id,
                                            sub_cat_name = p.TblSubCategory.subcat_name,
                                            product_name = p.product_name,
                                            product_qty = s.qty,
                                            code = p.code,
                                            barcode = p.barcode,
                                            sale_price = p.sale_price,
                                            purchase_price = p.purchase_price
                                        }).ToList();
         
            }
           else if(subcat_id!=0)
            {
                 products =
                                       (from p in db.tblProduct
                                        join s in db.tblStoreDetails on p.id equals s.product_id
                                        where s.store_id == store_id 
                                        where p.sub_cat_id==subcat_id
                                        select new dtoProductsDataInStore
                                        {
                                            id = p.id,
                                            product_name = p.product_name,
                                            store_id = s.store_id,
                                            store_name = s.TblStore.store_name,
                                            sub_cat_id = p.TblSubCategory.id,
                                            sub_cat_name = p.TblSubCategory.subcat_name,
                                            product_qty = s.qty,
                                            code = p.code,
                                            barcode = p.barcode,
                                            sale_price = p.sale_price,
                                            purchase_price = p.purchase_price
                                        }).ToList();
            }
            if (products != null)
            {
                response.code = StaticApiStatus.ApiSuccess.Code;
                response.status = StaticApiStatus.ApiSuccess.Status;
                response.message = StaticApiStatus.ApiSuccess.MessageAr;
                response.payload = products;
                return response;
            }
            else
            {
                response.code = StaticApiStatus.ApiStoreEmpty.Code;
                response.status = StaticApiStatus.ApiStoreEmpty.Status;
                response.message = StaticApiStatus.ApiStoreEmpty.MessageAr;
            }
            return response;
        }

      
        public Response<List<dtoCategory>> GetCatsDependOnStore(int store_id) {
            Response<List<dtoCategory>> response = new Response<List<dtoCategory>>();
            List<dtoProductForShow> subcat_ids = new List<dtoProductForShow>();
            List<dtoCategory> cat_ids = new List<dtoCategory>();
            var product_ids = (from s in db.tblStoreDetails.Where(s => s.store_id == store_id)
                               select new productinstore
                               {
                                   product_id = s.product_id
                               }).ToList();
           foreach (var p in product_ids)
                {
                     var subCat_id=(from pro in db.tblProduct.Where(pro => pro.id == p.product_id)
                     select new dtoProductForShow
                     {
                         product_name=pro.product_name,
                         purchase_price=pro.purchase_price,
                         sale_price=pro.sale_price,
                         sub_cat_id = pro.sub_cat_id
                     }).ToList();
                subcat_ids.AddRange(subCat_id);
                }
            foreach (var c in subcat_ids)
            {
                var cat_id = (from cat in db.tblSubCategory.Where(cat => cat.id == c.sub_cat_id)
                                 select new dtoCategory
                                 {
                                    id= (int)cat.cat_id,
                                    cat_name=cat.TblCategory.cat_name
                                 }).ToList();
                cat_ids.AddRange(cat_id);
            }
            response.payload = cat_ids;
            return response;
            }
        public Response<List<dtoProductsDataInStore>> GetAllproductSpecificStoreSearchByName(int store_id, int cat_id, int subcat_id, string val)
        {
            Response<List<dtoProductsDataInStore>> response = new Response<List<dtoProductsDataInStore>>();
            List<dtoProductsDataInStore> products = new List<dtoProductsDataInStore>();
            if (subcat_id == 0 && cat_id == 0)
            {
                products =
                                       (from p in db.tblProduct
                                        join s in db.tblStoreDetails on p.id equals s.product_id
                                        where s.store_id == store_id
                                        where p.product_name.Contains(val)

                                        select new dtoProductsDataInStore
                                        {
                                            id = p.id,
                                            store_id = s.store_id,
                                            store_name = s.TblStore.store_name,
                                            sub_cat_id = p.TblSubCategory.id,
                                            sub_cat_name = p.TblSubCategory.subcat_name,
                                            product_name = p.product_name,
                                            product_qty = s.qty,
                                            code = p.code,
                                            barcode = p.barcode,
                                            sale_price = p.sale_price,
                                            purchase_price = p.purchase_price

                                        }).ToList();
            }
            else if (cat_id != 0 && subcat_id == 0)
            {

                products =
               (from p in db.tblProduct
                join s in db.tblStoreDetails on p.id equals s.product_id
                join sc in db.tblSubCategory on p.sub_cat_id equals sc.id
                where s.store_id == store_id
                where sc.cat_id == cat_id
                where p.product_name.Contains(val)
                select new dtoProductsDataInStore
                {
                    id = p.id,
                    product_name = p.product_name,
                    store_id = s.store_id,
                    store_name = s.TblStore.store_name,
                    sub_cat_id = p.TblSubCategory.id,
                    sub_cat_name = p.TblSubCategory.subcat_name,
                    product_qty = s.qty,
                    code = p.code,
                    barcode = p.barcode,
                    sale_price = p.sale_price,
                    purchase_price = p.purchase_price
                }).ToList();

            }
            else if (subcat_id != 0)
            {
                products =
                                      (from p in db.tblProduct
                                       join s in db.tblStoreDetails on p.id equals s.product_id
                                       where s.store_id == store_id
                                       where p.sub_cat_id == subcat_id
                                       where p.product_name.Contains(val)
                                       select new dtoProductsDataInStore
                                       {
                                           id = p.id,
                                           product_name = p.product_name,
                                           store_id = s.store_id,
                                           store_name = s.TblStore.store_name,
                                           sub_cat_id = p.TblSubCategory.id,
                                           sub_cat_name = p.TblSubCategory.subcat_name,
                                           product_qty = s.qty,
                                           code = p.code,
                                           barcode = p.barcode,
                                           sale_price = p.sale_price,
                                           purchase_price = p.purchase_price
                                       }).ToList();
            }
            if (products != null)
            {
                response.code = StaticApiStatus.ApiSuccess.Code;
                response.status = StaticApiStatus.ApiSuccess.Status;
                response.message = StaticApiStatus.ApiSuccess.MessageAr;
                response.payload = products;
                return response;
            }
            else
            {
                response.code = StaticApiStatus.ApiStoreEmpty.Code;
                response.status = StaticApiStatus.ApiStoreEmpty.Status;
                response.message = StaticApiStatus.ApiStoreEmpty.MessageAr;
            }
            return response;
        }

        public Response<List<dtoProductsDataInStore>> GetAllproductSpecificStoreSearchByCode(int store_id, int cat_id, int subcat_id, string val)
        {
            Response<List<dtoProductsDataInStore>> response = new Response<List<dtoProductsDataInStore>>();
            List<dtoProductsDataInStore> products = new List<dtoProductsDataInStore>();
            if (subcat_id == 0 && cat_id == 0)
            {
                products =
                                       (from p in db.tblProduct
                                        join s in db.tblStoreDetails on p.id equals s.product_id
                                        where s.store_id == store_id
                                        where p.code.Contains(val)

                                        select new dtoProductsDataInStore
                                        {
                                            id = p.id,
                                            product_name = p.product_name,
                                            store_id = s.store_id,
                                            store_name = s.TblStore.store_name,
                                            sub_cat_id = p.TblSubCategory.id,
                                            sub_cat_name = p.TblSubCategory.subcat_name,
                                            product_qty = s.qty,
                                            code = p.code,
                                            barcode = p.barcode,
                                            sale_price = p.sale_price,
                                            purchase_price = p.purchase_price

                                        }).ToList();
            }
            else if (cat_id != 0 && subcat_id == 0)
            {

                products =
               (from p in db.tblProduct
                join s in db.tblStoreDetails on p.id equals s.product_id
                join sc in db.tblSubCategory on p.sub_cat_id equals sc.id
                where s.store_id == store_id
                where sc.cat_id == cat_id
                where p.product_name.Contains(val)
                select new dtoProductsDataInStore
                {
                    id = p.id,
                    product_name = p.product_name,
                    store_id = s.store_id,
                    store_name = s.TblStore.store_name,
                    sub_cat_id = p.TblSubCategory.id,
                    sub_cat_name = p.TblSubCategory.subcat_name,
                    product_qty = s.qty,
                    code = p.code,
                    barcode = p.barcode,
                    sale_price = p.sale_price,
                    purchase_price = p.purchase_price
                }).ToList();

            }
            else if (subcat_id != 0)
            {
                products =
                                      (from p in db.tblProduct
                                       join s in db.tblStoreDetails on p.id equals s.product_id
                                       where s.store_id == store_id
                                       where p.sub_cat_id == subcat_id
                                       where p.product_name.Contains(val)
                                       select new dtoProductsDataInStore
                                       {
                                           id = p.id,
                                           product_name = p.product_name,
                                           store_id = s.store_id,
                                           store_name = s.TblStore.store_name,
                                           sub_cat_id = p.TblSubCategory.id,
                                           sub_cat_name = p.TblSubCategory.subcat_name,
                                           product_qty = s.qty,
                                           code = p.code,
                                           barcode = p.barcode,
                                           sale_price = p.sale_price,
                                           purchase_price = p.purchase_price
                                       }).ToList();
            }
            if (products != null)
            {
                response.code = StaticApiStatus.ApiSuccess.Code;
                response.status = StaticApiStatus.ApiSuccess.Status;
                response.message = StaticApiStatus.ApiSuccess.MessageAr;
                response.payload = products;
                return response;
            }
            else
            {
                response.code = StaticApiStatus.ApiStoreEmpty.Code;
                response.status = StaticApiStatus.ApiStoreEmpty.Status;
                response.message = StaticApiStatus.ApiStoreEmpty.MessageAr;
            }
            return response;
        }

        public Response<List<dtoProductsDataInStore>> GetAllproductSpecificStoreSearchByBarCode(int store_id, int cat_id, int subcat_id, string val)
        {
            Response<List<dtoProductsDataInStore>> response = new Response<List<dtoProductsDataInStore>>();
            List<dtoProductsDataInStore> products = new List<dtoProductsDataInStore>();
            if (subcat_id == 0 && cat_id == 0)
            {
                products =
                                       (from p in db.tblProduct
                                        join s in db.tblStoreDetails on p.id equals s.product_id
                                        where s.store_id == store_id
                                        where p.barcode.StartsWith(val)

                                        select new dtoProductsDataInStore
                                        {
                                            id = p.id,
                                            product_name = p.product_name,
                                            store_id = s.store_id,
                                            store_name = s.TblStore.store_name,
                                            sub_cat_id = p.TblSubCategory.id,
                                            sub_cat_name = p.TblSubCategory.subcat_name,
                                            product_qty = s.qty,
                                            code = p.code,
                                            barcode = p.barcode,
                                            sale_price = p.sale_price,
                                            purchase_price = p.purchase_price

                                        }).ToList();
            }
            else if (cat_id != 0 && subcat_id == 0)
            {

                products =
               (from p in db.tblProduct
                join s in db.tblStoreDetails on p.id equals s.product_id
                join sc in db.tblSubCategory on p.sub_cat_id equals sc.id
                where s.store_id == store_id
                where sc.cat_id == cat_id
                where p.product_name.Contains(val)
                select new dtoProductsDataInStore
                {
                    id = p.id,
                    product_name = p.product_name,
                    store_id = s.store_id,
                    store_name = s.TblStore.store_name,
                    sub_cat_id = p.TblSubCategory.id,
                    sub_cat_name = p.TblSubCategory.subcat_name,
                    product_qty = s.qty,
                    code = p.code,
                    barcode = p.barcode,
                    sale_price = p.sale_price,
                    purchase_price = p.purchase_price
                }).ToList();

            }
            else if (subcat_id != 0)
            {
                products =
                                      (from p in db.tblProduct
                                       join s in db.tblStoreDetails on p.id equals s.product_id
                                       where s.store_id == store_id
                                       where p.sub_cat_id == subcat_id
                                       where p.code.StartsWith(val)
                                       select new dtoProductsDataInStore
                                       {
                                           id = p.id,
                                           product_name = p.product_name,
                                           store_id = s.store_id,
                                           store_name = s.TblStore.store_name,
                                           sub_cat_id = p.TblSubCategory.id,
                                           sub_cat_name = p.TblSubCategory.subcat_name,
                                           product_qty = s.qty,
                                           code = p.code,
                                           barcode = p.barcode,
                                           sale_price = p.sale_price,
                                           purchase_price = p.purchase_price
                                       }).ToList();
            }
            if (products != null)
            {
                response.code = StaticApiStatus.ApiSuccess.Code;
                response.status = StaticApiStatus.ApiSuccess.Status;
                response.message = StaticApiStatus.ApiSuccess.MessageAr;
                response.payload = products;
                return response;
            }
            else
            {
                response.code = StaticApiStatus.ApiStoreEmpty.Code;
                response.status = StaticApiStatus.ApiStoreEmpty.Status;
                response.message = StaticApiStatus.ApiStoreEmpty.MessageAr;
            }
            return response;
        }
    }
}
