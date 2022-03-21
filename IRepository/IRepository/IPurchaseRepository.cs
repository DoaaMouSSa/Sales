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
        public string Add(dtoPurchaseStoreDetails dto);
        //public dtoPurchaseForShow Edit(dtoPurchaseForAdd dto);
        public bool Delete(int id);
        //public List<dtoPurchaseForShow> Read();
    }
}
