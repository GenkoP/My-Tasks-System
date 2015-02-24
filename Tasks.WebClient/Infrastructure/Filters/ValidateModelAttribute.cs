namespace Tasks.WebClient.Infrastructure.Filters
{
    using System.Web;
    using System.Web.Mvc;

    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            if (!filterContext.Controller.ViewData.ModelState.IsValid )
            {
                filterContext.Result = new ViewResult
                {
                    ViewName = filterContext.ActionDescriptor.ActionName,
                    ViewData = filterContext.Controller.ViewData,
                    TempData = filterContext.Controller.TempData
                };
            }

            
        }



    }
}