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
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AccountDbContext db;
        public CustomerRepository(AccountDbContext _db)
        {
            db = _db;
        }
        public Response<dtoCustomer> Add(dtoCustomer customer)
        {
            Response<dtoCustomer> res = new Response<dtoCustomer>();
            if (customer != null)
            {
                if (customer.customer_name != "")
                {
                    if (db.TblCustomer.Any(c => c.customer_name == customer.customer_name))
                    {
                        res.code = Static_Data.DuplicateData.DuplicateName.Code;
                        res.status = Static_Data.DuplicateData.DuplicateName.Status;
                        res.message = Static_Data.DuplicateData.DuplicateName.MessageAr;
                    }

                    else if (db.TblCustomer.Where(c => c.customer_code != 0).Any(c => c.customer_code == customer.customer_code))
                    {
                        res.code = Static_Data.DuplicateData.DuplicateCode.Code;
                        res.status = Static_Data.DuplicateData.DuplicateCode.Status;
                        res.message = Static_Data.DuplicateData.DuplicateCode.MessageAr;
                    }

                    else if (db.TblCustomer.Where(c => c.customer_commercial_record != 0).Any(c => c.customer_commercial_record == customer.customer_commercial_record))
                    {
                        res.code = Static_Data.DuplicateData.DuplicateCommercail.Code;
                        res.status = Static_Data.DuplicateData.DuplicateCommercail.Status;
                        res.message = Static_Data.DuplicateData.DuplicateCommercail.MessageAr;
                    }

                    else if (db.TblCustomer.Where(c => c.customer_card != 0).Any(c => c.customer_card == customer.customer_card))
                    {
                        res.code = Static_Data.DuplicateData.DuplicateCard.Code;
                        res.status = Static_Data.DuplicateData.DuplicateCard.Status;
                        res.message = Static_Data.DuplicateData.DuplicateCard.MessageAr;
                    }
                    else
                    {
                        if (customer.customer_code == 0)
                        {
                            customer.customer_code = (db.TblCustomer.Max(s => s.customer_code)) + 1;
                        }
                        var newcustomer = new TblCustomer()
                        {
                            id = customer.id,
                            customer_name = customer.customer_name,
                            customer_address = customer.customer_address,
                            customer_code = customer.customer_code,
                            customer_phone = customer.customer_phone,
                            customer_email = customer.customer_email,
                            customer_type = customer.customer_type,

                            customer_card = customer.customer_card,
                            customer_commercial_record = customer.customer_commercial_record,
                        };
                        db.TblCustomer.Add(newcustomer);
                        db.SaveChanges();
                        customer.id = newcustomer.id;
                        customer.customer_name = newcustomer.customer_name;
                        customer.customer_address = newcustomer.customer_address;
                        customer.customer_code = newcustomer.customer_code;
                        customer.customer_phone = newcustomer.customer_phone;
                        customer.customer_type = newcustomer.customer_type;
                        customer.customer_email = newcustomer.customer_email;
                        customer.customer_card = newcustomer.customer_card;
                        customer.customer_commercial_record = newcustomer.customer_commercial_record;
                        res.code = Static_Data.StaticApiStatus.ApiSaveSuccess.Code;
                        res.status = Static_Data.StaticApiStatus.ApiSaveSuccess.Status;
                        res.message = Static_Data.StaticApiStatus.ApiSaveSuccess.MessageAr;
                        res.payload = customer;
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
                var customer = db.TblCustomer.Where(c => c.id == id).FirstOrDefault();
                db.TblCustomer.Remove(customer);
                db.SaveChanges();
                deleted = true;
            }
            return response;
        }

        public dtoCustomer Edit(dtoCustomer customer)
        {
            if (customer != null)
            {
                var isExist = db.TblCustomer.Where(c => c.id == customer.id).FirstOrDefault();
                if (isExist != null)
                {
                    if (customer.customer_name != null)
                        isExist.customer_name = customer.customer_name;
                    if (customer.customer_code != 0)
                        isExist.customer_code = customer.customer_code;
                    if (customer.customer_phone != null)
                        isExist.customer_phone = customer.customer_phone;
                    if (customer.customer_type != null)
                        isExist.customer_type = customer.customer_type;
                    if (customer.customer_address != null)
                        isExist.customer_address = customer.customer_address;
                    if (customer.customer_email != null)
                        isExist.customer_email = customer.customer_email;
                    if (customer.customer_card != 0)
                        isExist.customer_card = customer.customer_card;
                    if (customer.customer_commercial_record != 0)
                        isExist.customer_commercial_record = customer.customer_commercial_record;
                }
                db.SaveChanges();
                isExist.id = customer.id;
                isExist.customer_name = customer.customer_name;
            }
            return customer;
        }

        public List<dtoCustomer> Read()
        {
            var allcustomer = (from c in db.TblCustomer
                             select new dtoCustomer()
                          {
                              id = c.id,
                              customer_name = c.customer_name,
                              customer_code = c.customer_code,
                              customer_phone = c.customer_phone,
                              customer_address = c.customer_address,
                              customer_type = c.customer_type,
                              customer_email = c.customer_email,
                              customer_commercial_record = c.customer_commercial_record,
                              customer_card = c.customer_card,
                             }).ToList();
            return allcustomer;
        }

        public Response<List<dtoCustomerDD>> ReadForDD()
        {
            Response<List<dtoCustomerDD>> response = new Response<List<dtoCustomerDD>>();
            var allcustomer = (from c in db.TblCustomer
                       
                               select new dtoCustomerDD()
                               {
                                   id = c.id,
                                   customer_name = c.customer_name,
                               }).ToList();
            response.code = Static_Data.StaticApiStatus.ApiSuccess.Code;
            response.status = Static_Data.StaticApiStatus.ApiSuccess.Status;
            response.payload = allcustomer;

            return response;
        }

        public Response<List<dtoCustomer>> SearchCustomer(string val)
        {
            Response<List<dtoCustomer>> response = new Response<List<dtoCustomer>>();
            var allcustomer = (from c in db.TblCustomer
                            .Where(s => s.customer_name.Contains(val))
                               select new dtoCustomer()
                               {
                                   id = c.id,
                                   customer_name = c.customer_name,
                                   customer_code = c.customer_code,
                                   customer_phone = c.customer_phone,
                                   customer_address = c.customer_address,
                                   customer_type = c.customer_type,
                                   customer_email = c.customer_email,
                                   customer_commercial_record = c.customer_commercial_record,
                                   customer_card = c.customer_card,
                               }).ToList();
            response.code = Static_Data.StaticApiStatus.ApiSuccess.Code;
            response.status = Static_Data.StaticApiStatus.ApiSuccess.Status;
            response.payload = allcustomer;
            return response;
        }
    }
}
