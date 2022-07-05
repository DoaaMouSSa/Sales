using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Static_Data
{
   public class StaticApiStatus
    {
        public static class ApiSuccess
        {
            public static string Code = "Ok";
            public static string Status = "success";
            public static string MessageEn = "Success Get Data";
            public static string MessageAr = "تم تحميل البيانات بنجاح";
        }
        public static class ApiSaveSuccess
        {
            public static string Code = "Ok";
            public static string Status = "success";
            public static string MessageEn = "Infromation is Saved";
            public static string MessageAr = "تم الحفظ بنجاح";

        }

        public static class ApiFaild
        {
            public static string Code = "Faild";
            public static string Status = "fail";
            public static string MessageEn = "Somthing Wrong";
            public static string MessageAr = "حدث خطأ اثناء تحميل البيانات";
        }
        public static class ApiDuplicate
        {
            public static string Code = "Faild";
            public static string Status = "duplicate Data";
            public static string MessageEn = "Somthing Wrong";
            public static string MessageAr = "بيان مكرر";
        }
        public static class ApiEmpty
        {
            public static string Code = "List_Empty";
            public static string Status = "empty";
            public static string MessageEn = "List is Empty";
            public static string MessageAr = "القائمة فارغة";
        }
        public static class ApiRequired
        {
            public static string Code = "required";
            public static string Status = "required";
            public static string MessageEn = "field is Empty";
            public static string MessageAr = "لا يمكن ادخال بيان فارغ";
        }
        public static class ApiStoreNoProduct
        {
            public static string Code = "empty-product";
            public static string Status = "empty-product";
            public static string MessageEn = "no product";
            public static string MessageAr = "هذا الصنف غير متوفر بالمخزن";
        }
        public static class ApiStoreEmpty
        {
            public static string Code = "505";
            public static string Status = "empty";
            public static string MessageEn = "empty";
            public static string MessageAr = "غير متوفر أصناف بالمخزن";
        }
        public static class ApiNoCount
        {
            public static string Code = "Empty";
            public static string Status = "Empty";
            public static string MessageEn = "Empty";
            public static string MessageAr = "فارغ";
        }
    }
}
