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
        public dtoStore Add(dtoStore dto)
        {
            if (dto != null)
            {
                var newStore = new TblStore()
                {
                    id = dto.id,
                    store_name = dto.store_name
                };
                db.tblStore.Add(newStore);
                db.SaveChanges();
                dto.id = dto.id;
                dto.store_name = dto.store_name;
            }
            return dto;
        }

        public bool Delete(int id)
        {
            bool deleted = false;
            if (id != 0)
            {
                var store = db.tblStore.Where(s => s.id == id).FirstOrDefault();
                db.tblStore.Remove(store);
                db.SaveChanges();
                deleted = true;
            }
            return deleted;
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

        public List<dtoStore> Read()
        {
            var allstore = (from s in db.tblStore
                          select new dtoStore(){
                        id = s.id,
                        store_name = s.store_name
                          }).ToList();
            return allstore;
        }
    }
}
