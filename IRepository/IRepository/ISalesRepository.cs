using Dto.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepository.IRepository
{
   public interface ISalesRepository
    {
        public Response<dtoInvoiceNumbers> GetTotalAfterDiscounts(decimal total, decimal discountNum, decimal discountPer, decimal tax, decimal DiscountTax);
        public Response<dtoSalesInvDetails> Add(dtoSalesInvDetails dto);
        public Tuple<int, int> getMinMaxCode();
        public Response<int> GetMaxCode();
        public Response<bool> Delete(int id);
        public Response<List<dtoSalesInvForShow>> Read();
        public Response<List<dtoSalesInvForShow>> ReadSalesInvDetails(int sale_inv_id);
        public Response<List<dtoSalesInvForShow>> FilterSalesInvoices(int customer_id, int store_id, DateTime from_date, DateTime to_date, int from_code, int to_code, int is_deleted);
        public Response<List<dtoSalesInvForShow>> GetDeletedSalesInvoices();
        public Response<List<dtoSalesInvForShow>> GetNotDeletedSalesInvoices();
    }
}
