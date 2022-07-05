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
    public class SupplierRepository : ISupplierRepository
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
                        res.code = Static_Data.DuplicateData.DuplicateName.Code;
                        res.status = Static_Data.DuplicateData.DuplicateName.Status;
                        res.message = Static_Data.DuplicateData.DuplicateName.MessageAr;
                    }

                    else if (db.tblSupplier.Where(c => c.supplier_code != 0).Any(c => c.supplier_code == sup.supplier_code))
                    {
                        res.code = Static_Data.DuplicateData.DuplicateCode.Code;
                        res.status = Static_Data.DuplicateData.DuplicateCode.Status;
                        res.message = Static_Data.DuplicateData.DuplicateCode.MessageAr;
                    }

                    else if (db.tblSupplier.Where(c => c.supplier_commercial_record != 0).Any(c => c.supplier_commercial_record == sup.supplier_commercial_record))
                    {
                        res.code = Static_Data.DuplicateData.DuplicateCommercail.Code;
                        res.status = Static_Data.DuplicateData.DuplicateCommercail.Status;
                        res.message = Static_Data.DuplicateData.DuplicateCommercail.MessageAr;
                    }

                    else if (db.tblSupplier.Where(c => c.supplier_card != 0).Any(c => c.supplier_card == sup.supplier_card))
                    {
                        res.code = Static_Data.DuplicateData.DuplicateCard.Code;
                        res.status = Static_Data.DuplicateData.DuplicateCard.Status;
                        res.message = Static_Data.DuplicateData.DuplicateCard.MessageAr;
                    }
                    else
                    {
                        if (sup.supplier_code == 0)
                        {
                            sup.supplier_code = (db.tblSupplier.Max(s => s.supplier_code)) + 1;
                        }
                        var newsupplier = new TblSupplier()
                        {
                            id = sup.id,
                            supplier_name = sup.supplier_name,
                            supplier_address = sup.supplier_address,
                            supplier_code = sup.supplier_code,
                            supplier_phone = sup.supplier_phone,
                            supplier_type = sup.supplier_type,
                            supplier_card = sup.supplier_card,
                            supplier_commercial_record = sup.supplier_commercial_record,
                            supplier_email = sup.supplier_email
                        };
                        db.tblSupplier.Add(newsupplier);
                        db.SaveChanges();
                        sup.id = newsupplier.id;
                        sup.supplier_name = newsupplier.supplier_name;
                        sup.supplier_phone = newsupplier.supplier_phone;
                        sup.supplier_code = newsupplier.supplier_code;
                        sup.supplier_address = newsupplier.supplier_address;
                        sup.supplier_type = newsupplier.supplier_type;
                        sup.supplier_email = newsupplier.supplier_email;
                        sup.supplier_commercial_record = newsupplier.supplier_commercial_record;
                        sup.supplier_card = sup.supplier_card;
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

        public Response<bool> Delete(int id)
        {
            Response<bool> response = new Response<bool>();
            bool deleted = false;
            if (id != 0)
            {
                var supplier = db.tblSupplier.Where(c => c.id == id).FirstOrDefault();
                db.tblSupplier.Remove(supplier);
                db.SaveChanges();
                deleted = true;
            }
            return response;
        }

        public dtoSupplier Edit(dtoSupplier sup)
        {
            if (sup != null)
            {
                var isExist = db.tblSupplier.Where(c => c.id == sup.id).FirstOrDefault();
                if (isExist != null)
                {
                    if (sup.supplier_name != null)
                        isExist.supplier_name = sup.supplier_name;
                    if (sup.supplier_code != 0)
                        isExist.supplier_code = sup.supplier_code;
                    if (sup.supplier_address != null)
                        isExist.supplier_address = sup.supplier_address;
                    if (sup.supplier_phone != null)
                        isExist.supplier_phone = sup.supplier_phone;
                    if (sup.supplier_card != 0)
                        isExist.supplier_card = sup.supplier_card;
                    if (sup.supplier_commercial_record != 0)
                        isExist.supplier_commercial_record = sup.supplier_commercial_record;
                    if (sup.supplier_email != null)
                        isExist.supplier_email = sup.supplier_email;
                    if (sup.supplier_type != null)
                        isExist.supplier_type = sup.supplier_type;
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
                              supplier_commercial_record = s.supplier_commercial_record,
                              supplier_card = s.supplier_card,
                              supplier_email = s.supplier_email,
                              supplier_type = s.supplier_type,
                          }).ToList();
            return allsup;
        }

        public Response<List<dtoSupplierDD>> ReadForDD()
        {
            Response<List<dtoSupplierDD>> response = new Response<List<dtoSupplierDD>>();
            var allsup = (from s in db.tblSupplier
                          select new dtoSupplierDD()
                          {
                              id = s.id,
                              supplier_name = s.supplier_name,
                          }).ToList();
            response.code = Static_Data.StaticApiStatus.ApiSuccess.Code;
            response.status = Static_Data.StaticApiStatus.ApiSuccess.Status;
            response.payload = allsup;

            return response;
        }

        public Response<List<dtoSupplier>> SearchSupplier(string val)
        {
            Response<List<dtoSupplier>> response = new Response<List<dtoSupplier>>();
            var allsup = (from s in db.tblSupplier
                          .Where(s => s.supplier_name.Contains(val))
                          select new dtoSupplier()
                          {
                              id = s.id,
                              supplier_name = s.supplier_name,
                              supplier_code = s.supplier_code,
                              supplier_phone = s.supplier_phone,
                              supplier_address = s.supplier_address,
                              supplier_commercial_record = s.supplier_commercial_record,
                              supplier_card = s.supplier_card,
                              supplier_email = s.supplier_email,
                              supplier_type = s.supplier_type,
                          }).ToList();
            response.code = Static_Data.StaticApiStatus.ApiSuccess.Code;
            response.status = Static_Data.StaticApiStatus.ApiSuccess.Status;
            response.payload = allsup;

            return response;
        }
    }
}
