using Dto.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepository.IRepository
{
   public interface IPurchaseReturnRepository
    {
        public Response<dtoPurchaseReturnStoreDetails> AddPurchaseReturnInvoice(dtoPurchaseReturnStoreDetails dto);
        public Response<int> GetMaxCode();
        public Response<dtoInvoiceNumbers> GetTotalAfterDiscounts(decimal  total, decimal  discountNum, decimal  discountPer, decimal  tax, decimal  taxM);
        //public dtoPurchaseForShow Edit(dtoPurchaseForAdd dto);
        public Response<bool> Delete(int id);
        public Response<List<dtoPurchaseInvReturnForShow>> ReadAllPurchaseReturnInvoices();
        public Response<List<dtoPurchaseInvReturnForShow>> ReadCustomPurchaseReturnInvoice(int pur_return_inv_id);
        public List<dtoPurchaseInvReturnForShow> ReadCustomPurchaseReturnInvoiceForReport(int pur_return_inv_id);
        public Response<List<dtoPurchaseInvReturnForShow>> FilterPurchaseReturnInvoices(int supplier_id,int store_id,DateTime from_date,DateTime to_date);
        public Response<List<dtoPurchaseInvReturnForShow>> GetDeletedPurchaseReturnInvoices();
        public Response<List<dtoPurchaseInvReturnForShow>> GetNotDeletedPurchaseReturnInvoices();

    }
}
