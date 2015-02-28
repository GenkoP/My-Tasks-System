using System.Web.Mvc;

namespace Tasks.WebClient.Areas.Administration
{
    public class AdministrationAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Administration";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {

            context.MapRoute(
                "Admin_elmah",
                "Administration/elmah/{type}",
                new { action = "Index", controller = "Elmah", type = UrlParameter.Optional }
            );

            context.MapRoute(
                "Administration_default",
                "Administration/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );

            


        }
    }
}