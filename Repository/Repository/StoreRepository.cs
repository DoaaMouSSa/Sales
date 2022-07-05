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
    public class StoreRepository : IStoreRepository
    {
        private readonly AccountDbContext db;
        public StoreRepository(AccountDbContext _db)
        {
            db = _db;
        }
        public Response<dtoStore> Add(dtoStore dto)
        {
            Response<dtoStore> res = new Response<dtoStore>();
            if (dto != null)
            {
                if (dto.store_name != "")
                {
                    if (db.tblStore.Any(c => c.store_name == dto.store_name))
                    {
                        res.code = Static_Data.DuplicateData.DuplicateName.Code;
                        res.status = Static_Data.DuplicateData.DuplicateName.Status;
                        res.message = Static_Data.DuplicateData.DuplicateName.MessageAr;
                    }
                    else
                    {
                        var newStore = new TblStore()
                        {
                            id = dto.id,
                            store_name = dto.store_name
                        };
                        db.tblStore.Add(newStore);
                        db.SaveChanges();
                        dto.id = newStore.id;
                        dto.store_name = newStore.store_name;
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

        public Response<bool> Delete(int id)
        {
            Response<bool> response = new Response<bool>();

            bool deleted = false;
            if (id != 0)
            {
                var store = db.tblStore.Where(s => s.id == id).FirstOrDefault();
                db.tblStore.Remove(store);
                db.SaveChanges();
                deleted = true;
            }
            return response;
        }

        public dtoStore Edit(dtoStore dto)
        {
            if (dto != null)
            {
                var isExist = db.tblStore.Where(s => s.id == dto.id).FirstOrDefault();
                if (isExist != null)
                {
                    isExist.store_name = dto.store_name;
                }
                db.SaveChanges();
                isExist.id = dto.id;
                isExist.store_name = dto.store_name;
            }
            return dto;
        }

        public Response<List<dtoStore>> Read()
        {
            Response<List<dtoStore>> res = new Response<List<dtoStore>>();
            var allstore = (from s in db.tblStore
                          select new dtoStore(){
                        id = s.id,
                        store_name = s.store_name
                          }).ToList();
            res.code = Static_Data.StaticApiStatus.ApiSuccess.Code;
            res.status = Static_Data.StaticApiStatus.ApiSuccess.Status;
            res.payload = allstore;
            return res;
        }
    }
}
