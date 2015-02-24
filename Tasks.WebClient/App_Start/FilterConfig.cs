using System.Web;
using System.Web.Mvc;
using Tasks.WebClient.App_Start;

namespace Tasks.WebClient
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
           
        }
    }
}
