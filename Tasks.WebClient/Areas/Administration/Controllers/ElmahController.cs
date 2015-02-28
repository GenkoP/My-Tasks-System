namespace Tasks.WebClient.Areas.Administration.Controllers
{

    using System.Web.Mvc;
    using Tasks.WebClient.Infrastructure.Helpers;

    [Authorize(Roles = "Admin")]
    public class ElmahController : Controller
    {
        // GET: Administration/Elmah
        public ActionResult Index(string type)
        {
            return new ElmahResult(type);
        }
    }
}