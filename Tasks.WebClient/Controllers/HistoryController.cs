namespace Tasks.WebClient.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Tasks.Data.Repositories;
    using Tasks.Models;
    using Tasks.WebClient.Models.ViewModels;
    using Tasks.WebClient.Infrastructure.Providers;


    public class HistoryController : BaseController
    {

        public HistoryController(ITaskManagerData data, ICurrentUserIdProvider userId)
            : base(data, userId)
        {

        }
        public ActionResult Index()
        {

            var compleatedTasks = FindCompleatedTasks()
                                    .Select(MyTaskViewModel.GetTasks); 

            return View(compleatedTasks);
        }

       

        [ValidateAntiForgeryToken]
        public ActionResult ClearAll()
        {
            var compleatedTasks = FindCompleatedTasks();

            foreach (var task in compleatedTasks)
            {
                this.Data.Tasks.Delete(task);
            }

            this.Data.SaveChanges();


            return RedirectToAction("Index");

        }
        
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            this.RenderDeleteTask(id);

            return this.RedirectToAction("Index");

        }


        private IQueryable<MyTask> FindCompleatedTasks()
        {

            var currentUserId = this.CurrentUser.GetUserId();

            var currentDate = DateTime.Now.Date;

            var compleatedTasks = this.Data.Tasks.SearchFor
                                (x => x.UserID == currentUserId
                                    && (x.IsCompleted == true
                                    || DbFunctions.TruncateTime(x.DateToEnd) < DbFunctions.TruncateTime(currentDate)))
                                .OrderByDescending(x => x.Priority)
                                .AsQueryable();
                                
            return compleatedTasks;
        }

    }
}