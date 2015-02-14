﻿namespace Tasks.WebClient.Controllers
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
                .Select(MyTaskViewModel.GetTasks);


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
        
      
        public ActionResult Delete(int id)
        {

            var currnetUserId = this.CurrentUser.GetUserId();

            var task = this.Data.Tasks.SearchFor(x => x.UserID == currnetUserId && x.ID == id).FirstOrDefault();

            if (task == null)
            {
                return View("Error");
            }
            else
            {
                this.Data.Tasks.Delete(task);
                this.Data.SaveChanges();
            }

            return RedirectToAction("Index");

        }

        [HttpGet]
        public  ActionResult Update(int id)
        {
            var currnetUserId = this.CurrentUser.GetUserId();

            var task = this.Data.Tasks.SearchFor(x => x.UserID == currnetUserId && x.ID == id)
                .Select(MyTaskViewModel.GetTasks)
                .FirstOrDefault();
            if (task == null)
            {
                return View("Error");
            }

            return View(task);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(MyTaskViewModel task)
        {
            if (!ModelState.IsValid)
            {

                return View("Error");
            }

            var currnetUserId = this.CurrentUser.GetUserId();

            var updatedTask = this.Data.Tasks.SearchFor(x => x.UserID == currnetUserId && x.ID == task.ID)
                .FirstOrDefault();

            if (updatedTask == null)
            {
                return View("Error");
            }

            updatedTask.Title = task.Title;
            updatedTask.Description = task.Description;
            updatedTask.Preority = task.Preority;
            updatedTask.DateToEnd = task.DateToEnd;

            this.Data.Tasks.Update(updatedTask);
            this.Data.SaveChanges();

            return RedirectToAction("Index");
        }


    }
}