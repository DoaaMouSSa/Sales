using Dto.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepository.IRepository
{
   public interface ISalesReturnRepository
    {
        public Response<dtoInvoiceNumbers> GetTotalAfterDiscounts(decimal total, decimal discountNum, decimal discountPer, decimal tax, decimal DiscountTax);
        public Response<dtoSalesReturnInvDetails> AddSalesReturnInvoice(dtoSalesReturnInvDetails dto);
        public Response<int> GetMaxReturnSalesCode();
        public Response<bool> Delete(int id);
        public Response<List<dtoSalesReturnInvForShow>> ReadSummeryReturnSalesInv();
        public Response<List<dtoSalesReturnInvForShow>> ReadSalesReturnInvWithDetails(int sale_return_inv_id);
        public List<dtoSalesReturnInvForShow> ReadCustomSalesReturnInvoiceForReport(int sale_return_inv_id);
        public Response<List<dtoSalesReturnInvForShow>> FilterSalesReturnInvoices(int customer_id, int store_id, DateTime from_date, DateTime to_date);
        public Response<List<dtoSalesReturnInvForShow>> GetDeletedSalesReturnInvoices();
        public Response<List<dtoSalesReturnInvForShow>> GetNotDeletedReturnSalesInvoices();
    }
}
