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
    using System.Data.Entity;

    public class HomeController : BaseController
    {

        public HomeController(ITaskManagerData data, ICurrentUserIdProvider userId)
            : base(data, userId)
        {

        }

        public ActionResult Index()
        {
            string currentUserId = this.CurrentUser.GetUserId();

            var currnetDate = DateTime.Now.Date;

            var allTasksForToday = this.Data.Tasks
                        .SearchFor(x => x.UserID == currentUserId
                                   && DbFunctions.TruncateTime(x.DateToEnd) == DbFunctions.TruncateTime(currnetDate)
                                   && x.IsCompleted == false
                                   )
                        .OrderByDescending(x => x.Priority)
                        .Select(MyTaskViewModel.GetTasks);

            return View(allTasksForToday);
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