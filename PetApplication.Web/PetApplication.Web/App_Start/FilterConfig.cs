using PetApplication.Utility;
using System.Web.Mvc;

namespace PetApplication.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            //Exception Filter
            filters.Add(new AppErrorHandler());
        }
    }
}
