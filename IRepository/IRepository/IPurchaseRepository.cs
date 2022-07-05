using Dto.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepository.IRepository
{
   public interface IPurchaseRepository
    {
        public Response<dtoPurchaseInvForShow> Add(dtoPurchaseStoreDetails dto);
        public Response<int> GetMaxCode();
        public Tuple<int, int> getMinMaxCode();
        public Response<dtoInvoiceNumbers> GetTotalAfterDiscounts(decimal  total, decimal  discountNum, decimal  discountPer, decimal  tax, decimal  taxM);
        public Response<bool> Delete(int id);
        public Response<List<dtoPurchaseInvForShow>> Read();
        public Response<List<dtoPurchaseInvForShow>> ReadCustomPurchaseInvoice(int pur_inv_id);
        public Response<List<dtoPurchaseInvForShow>> FilterPurchaseInvoices(int supplier_id,int store_id,DateTime from_date,DateTime to_date, int from_code, int to_code, int is_deleted);
        public Response<List<dtoPurchaseInvForShow>> GetDeletedPurchaseInvoices();
        public Response<List<dtoPurchaseInvForShow>> GetNotDeletedPurchaseInvoices();

    }
}
