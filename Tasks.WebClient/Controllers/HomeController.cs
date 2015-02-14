namespace Tasks.WebClient.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Tasks.Data.Repositories;
    using Tasks.WebClient.Providers;
    using Tasks.WebClient.Models.ViewModels;

    public class HomeController : BaseController
    {

        public HomeController(ITaskManagerData data, ICurrentUserIdProvider userId)
            : base(data, userId)
        {

        }

        public ActionResult Index()
        {
            string currentUserId = this.CurrentUser.GetUserId();

            var allTasks = this.Data.Tasks.All()
                .Where(x => x.UserID == currentUserId && x.DateToEnd >= DateTime.Now)
                .Select(MyTaskViewModel.GetTasks);

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