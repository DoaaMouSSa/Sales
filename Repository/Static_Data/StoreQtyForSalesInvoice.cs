using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Static_Data
{
    public class SalesIvoices
    {
        public static class SuccessSalesInvoice
        {
            public static string Status = "success";
            public static string Code = "51";
            public static string MessageEn = "";
            public static string MessageAr = "تمت عمليه البيع بنجاح";
        }
        public static class FailSalesInvoice
        {
            public static string Status = "Unavailable product";
            public static string Code = "52";
            public static string MessageEn = "";
            public static string MessageAr = "لا يمكن اتمام العمليه";
        }
        public static class EmptySalesInvoice
        {
            public static string Status = "empty";
            public static string Code = "53";
            public static string MessageEn = "";
            public static string MessageAr = "فاتوره البيع فارغه";
        }
    }
   
}
