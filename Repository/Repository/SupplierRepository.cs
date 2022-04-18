using DataLayer.DBContext;
using DataLayer.Tables;
using Dto.Dto;
using IRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
   public class SupplierRepository: ISupplierRepository
    {
        private readonly AccountDbContext db;
        public SupplierRepository(AccountDbContext _db)
        {
            db = _db;
        }

        public Response<dtoSupplier> Add(dtoSupplier sup)
        {
            Response<dtoSupplier> res = new Response<dtoSupplier>();
            if (sup != null)
            {
                if (sup.supplier_name != "")
                {
                    if (db.tblSupplier.Any(c => c.supplier_name == sup.supplier_name))
                    {
                        res.code = Static_Data.StaticApiStatus.ApiDuplicate.Code;
                        res.status = Static_Data.StaticApiStatus.ApiDuplicate.Status;
                        res.message = Static_Data.StaticApiStatus.ApiDuplicate.MessageAr;
                    }
                    else
                    {
                        var newsupplier = new TblSupplier()
                        {
                            id = sup.id,
                            supplier_name = sup.supplier_name,
                            supplier_address = sup.supplier_address,
                            supplier_code = sup.supplier_code,
                            supplier_phone = sup.supplier_phone,
                        };
                        db.tblSupplier.Add(newsupplier);
                        db.SaveChanges();
                        sup.id = newsupplier.id;
                        sup.supplier_name = newsupplier.supplier_name;
                        sup.supplier_phone = newsupplier.supplier_phone;
                        sup.supplier_code = newsupplier.supplier_code;
                        sup.supplier_address = newsupplier.supplier_address;
                        res.code = Static_Data.StaticApiStatus.ApiSaveSuccess.Code;
                        res.status = Static_Data.StaticApiStatus.ApiSaveSuccess.Status;
                        res.message = Static_Data.StaticApiStatus.ApiSaveSuccess.MessageAr;
                        res.payload = sup;
                    }
                }
                else
                {
                    res.code = Static_Data.StaticApiStatus.ApiRequired.Code;
                    res.status = Static_Data.StaticApiStatus.ApiRequired.Status;
                    res.message = Static_Data.StaticApiStatus.ApiRequired.MessageAr;

                }
            }


            return res;
        }

        public bool Delete(int id)
        {
            bool deleted = false;
            if (id != 0)
            {
                var supplier = db.tblSupplier.Where(c => c.id == id).FirstOrDefault();
                db.tblSupplier.Remove(supplier);
                db.SaveChanges();
                deleted = true;
            }
            return deleted;
        }

        public dtoSupplier Edit(dtoSupplier sup)
        {
            if (sup != null)
            {
                var isExist = db.tblSupplier.Where(c => c.id == sup.id).FirstOrDefault();
                if (isExist != null)
                {
                    if(sup.supplier_name!=null)
                        isExist.supplier_name = sup.supplier_name;
                    if (sup.supplier_code != 0)
                        isExist.supplier_code = sup.supplier_code;
                    if (sup.supplier_address != null)
                        isExist.supplier_address = sup.supplier_address;
                    if (sup.supplier_phone != null)
                        isExist.supplier_phone = sup.supplier_phone;
                }
                db.SaveChanges();
                isExist.id = sup.id;
                isExist.supplier_name = sup.supplier_name;
            }
            return sup;
        }

        public List<dtoSupplier> Read()
        {
            var allsup = (from s in db.tblSupplier
                               select new dtoSupplier()
                               {
                                   id = s.id,
                                   supplier_name = s.supplier_name,
                                   supplier_code = s.supplier_code,
                                   supplier_phone = s.supplier_phone,
                                   supplier_address = s.supplier_address,

                               }).ToList();
            return allsup;
        }
    }
}
