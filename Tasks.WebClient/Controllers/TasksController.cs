namespace Tasks.WebClient.Controllers
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Tasks.Models;
    using Tasks.Data.Repositories;
    using Tasks.WebClient.Providers;
    using Tasks.WebClient.Models.InputModels;
    using Tasks.WebClient.Models.ViewModels;
   


    [Authorize]
    public class TasksController : BaseController
    {

        public TasksController(ITaskManagerData data, ICurrentUserIdProvider userId)
            : base(data, userId)
        {

        }

        [HttpGet]
        public ActionResult Index()
        {
            var currentUserId = this.CurrentUser.GetUserId();

            var currentTasks = this.Data.Tasks.All()
                .Where(x => x.DateToEnd > DateTime.Now && x.UserID == currentUserId)
                .OrderBy(x => x.Preority)
                .Select(MyTaskViewModelHomeIndex.GetTasks);


            return View(currentTasks);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MyTaskInputModel task)
        {


            if (!ModelState.IsValid)
            {
                return View("Error");
            }

            var currentUserId = this.CurrentUser.GetUserId();

            var newTask = new MyTask
            {
                UserID = currentUserId,
                Title = task.Title,
                DateOnCreate = DateTime.Now,
                DateToEnd = task.DateToEnd,
                Description = task.Description,
                Preority = task.Preority,

            };

            this.Data.Tasks.Add(newTask);
            this.Data.SaveChanges();


            return RedirectToAction("Index");
        }

    }
}