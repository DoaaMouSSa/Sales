using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Dto
{
    public class dtoInvoiceNumbers
    {
        public decimal total { get; set; }
        public decimal discount { get; set; }
        public decimal tax { get; set; }
        public decimal tax_discount { get; set; }
        public decimal final_total { get; set; }
    }
}
