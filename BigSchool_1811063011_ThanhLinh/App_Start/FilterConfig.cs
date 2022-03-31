using System.Web;
using System.Web.Mvc;

namespace BigSchool_1811063011_ThanhLinh
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
