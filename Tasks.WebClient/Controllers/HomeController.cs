namespace Tasks.WebClient.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Tasks.Data.Repositories;

    public class HomeController : BaseController
    {

        public HomeController(ITaskManagerData data)
            : base(data)
        {

        }

        public ActionResult Index()
        {
            var allTasks = this.data.Tasks.All();

            return View(allTasks);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}