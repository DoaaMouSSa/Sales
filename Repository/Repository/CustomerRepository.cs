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
                    if (db.tblCustomer.Any(c => c.customer_name == customer.customer_name))
                    {
                        res.code = Static_Data.StaticApiStatus.ApiDuplicate.Code;
                        res.status = Static_Data.StaticApiStatus.ApiDuplicate.Status;
                        res.message = Static_Data.StaticApiStatus.ApiDuplicate.MessageAr;
                    }
                    else
                    {
                        var newcustomer = new TblCustomer()
                        {
                            id = customer.id,
                            customer_name = customer.customer_name,
                            customer_address = customer.customer_address,
                            customer_code = customer.customer_code,
                            customer_phone = customer.customer_phone,
                        };
                        db.tblCustomer.Add(newcustomer);
                        db.SaveChanges();
                        customer.id = newcustomer.id;
                        customer.customer_name = newcustomer.customer_name;
                        customer.customer_address = newcustomer.customer_address;
                        customer.customer_code = newcustomer.customer_code;
                        customer.customer_phone = newcustomer.customer_phone;
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

        public bool Delete(int id)
        {
            bool deleted = false;
            if (id != 0)
            {
                var customer = db.tblCustomer.Where(c => c.id == id).FirstOrDefault();
                db.tblCustomer.Remove(customer);
                db.SaveChanges();
                deleted = true;
            }
            return deleted;
        }

        public dtoCustomer Edit(dtoCustomer customer)
        {
            if (customer != null)
            {
                var isExist = db.tblCustomer.Where(c => c.id == customer.id).FirstOrDefault();
                if (isExist != null)
                {
                    if (customer.customer_name != null)
                        isExist.customer_name = customer.customer_name;
                    if (customer.customer_code != 0)
                        isExist.customer_code = customer.customer_code;
                    if (customer.customer_phone != null)
                        isExist.customer_phone = customer.customer_phone;
                    if (customer.customer_address != null)
                        isExist.customer_address = customer.customer_address;
                }
                db.SaveChanges();
                isExist.id = customer.id;
                isExist.customer_name = customer.customer_name;
            }
            return customer;
        }

        public List<dtoCustomer> Read()
        {
            var allcustomer = (from c in db.tblCustomer
                          select new dtoCustomer()
                          {
                              id = c.id,
                              customer_name = c.customer_name,
                              customer_code = c.customer_code,
                              customer_phone = c.customer_phone,
                              customer_address = c.customer_address,

                          }).ToList();
            return allcustomer;
        }
    }
}
