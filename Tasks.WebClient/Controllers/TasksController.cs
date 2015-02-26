namespace Tasks.WebClient.Controllers
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Net;
    using System.Web.Mvc;
    using System.Collections.Generic;
    using System.Data.Entity.Core.Objects;
    using System.Data.Entity;

    using Tasks.Models;
    using Tasks.Data.Repositories;
    using Tasks.WebClient.Infrastructure.Providers;
    using Tasks.WebClient.Models.InputModels;
    using Tasks.WebClient.Models.ViewModels;
    using Tasks.WebClient.Infrastructure.Filters;

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

            var currentDate = DateTime.Now.Date;

            var currentTasks = this.Data.Tasks
                .SearchFor(x => DbFunctions.TruncateTime(x.DateToEnd) >= DbFunctions.TruncateTime(currentDate)
                                    && x.UserID == currentUserId
                                    && x.IsCompleted == false
                               )
                                .GroupBy(x => x.DateToEnd)
                                .Where(g => g.Count() >= 1)
                                .Select(g => g.Key);


            return View(currentTasks);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateModel]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MyTaskInputModel task, ICollection<SubTaskInputModel> subtasks)
        {

            var currentUserId = this.CurrentUser.GetUserId();

            var newTask = new MyTask
            {
                UserID = currentUserId,
                Title = task.Title,
                DateOnCreate = DateTime.Now,
                DateToEnd = task.DateToEnd,
                Description = task.Description,
                Priority = task.Priority,

            };

            this.Data.Tasks.Add(newTask);
           
            this.CreateSubtasks(subtasks, newTask.ID);

            this.Data.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {

            this.RenderDeleteTask(id);

            return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            var currnetUserId = this.CurrentUser.GetUserId();

            var task = this.Data.Tasks.All().Where(x => x.UserID == currnetUserId && x.ID == id)
                .Select(MyTaskViewModel.GetTasks)
                .FirstOrDefault();

            this.ObjectIsNull(task);

            return View(task);
        }


        [HttpPost]
        [ValidateModel]
        [ValidateAntiForgeryToken]
        public ActionResult Update(MyTaskInputModel task, ICollection<SubTaskInputModel> inpSubtasks)
        {

            var currnetUserId = this.CurrentUser.GetUserId();

            var updatedTask = this.Data.Tasks.All().Where(x => x.UserID == currnetUserId && x.ID == task.ID)
                .FirstOrDefault();

            this.ObjectIsNull(updatedTask);

            updatedTask.Title = task.Title;
            updatedTask.IsCompleted = false;
            updatedTask.Description = task.Description;
            updatedTask.Priority = task.Priority;
            updatedTask.DateToEnd = task.DateToEnd;

            this.Data.Tasks.Update(updatedTask);
            this.Data.SaveChanges();


            this.CreateSubtasks(inpSubtasks, updatedTask.ID);


            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckTaskIsComplete(int id)
        {
            var currnetUserId = this.CurrentUser.GetUserId();

            var task = this.Data.Tasks.SearchFor(x => x.UserID == currnetUserId && x.ID == id).FirstOrDefault();

            this.ObjectIsNull(task);

            task.IsCompleted = true;

            this.Data.Tasks.Update(task);
            this.Data.SaveChanges();

            return RedirectToAction("Index");
        }

        private void CreateSubtasks(ICollection<SubTaskInputModel> inpSubtasks, int taskID)
        {
            if (inpSubtasks != null)
            {
                this.SubtasksIsValid(inpSubtasks);

                var listSubtasks = new List<SubTask>();

                foreach (var inpSubtask in inpSubtasks)
                {

                    listSubtasks.Add(new SubTask
                    {
                        Title = inpSubtask.SubtaskTitle,
                        Priority = inpSubtask.SubtaskPriority,
                        MyTaskID = taskID
                    });
                }

                foreach (var subtask in listSubtasks)
                {
                    this.Data.SubTasks.Add(subtask);
                }

            }
        }

    }
}