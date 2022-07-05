using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto.Dto;
using DtoReport.Dto;
namespace IRepository.IRepository
{
   public interface IProuductRepository
    {
        public Response<dtoProductForAdd> Add(dtoProductForAdd dto);
        public dtoProductForAdd Edit(dtoProductForAdd dto);
        public Response<bool> Delete(int id);
        public List<dtoProductForShow> Read();
        public List<dtoProductReport> ReadForReport();
        public Response<List<dtoProductForShowBeforeAddToInvoice>> ReadForInvoiceAdd(string character, int store_id);
    }
}
