using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Static_Data
{
    public class DeletedData
    {
        public static class SuccessDelete
        {
            public static string Code = "41";
            public static string Status = "Deleted Data";
            public static string MessageAr = "تم الحذف بنجاح";
        }
        public static class FailDelete
        {
            public static string Code = "42";
            public static string Status = "Not Deleted Data";
            public static string MessageAr = "تعذر الحذف";
        }
    }
}
