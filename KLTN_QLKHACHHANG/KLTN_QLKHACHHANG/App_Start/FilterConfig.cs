using System.Web;
using System.Web.Mvc;

namespace KLTN_QLKHACHHANG
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
