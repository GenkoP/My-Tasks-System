namespace Tasks.WebClient.Controllers
{

    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Collections.Generic;

    using Tasks.Data.Repositories;
    using Tasks.WebClient.Providers;
    using Tasks.WebClient.Models.ViewModels;
    using Tasks.WebClient.Models.InputModels;
    using Tasks.Models;

    public class SubTasksController : BaseController
    {

        public SubTasksController(ITaskManagerData data, ICurrentUserIdProvider userId)
            : base(data, userId)
        {

        }


        public ActionResult GetSubTasks(int taskId)
        {

            var currnetUserId = this.CurrentUser.GetUserId();

            var listOfsubtasks = this.Data.SubTasks
                            .SearchFor(x => x.MyTask.UserID == currnetUserId
                                        && x.MyTask.ID == taskId
                                        && x.IsCompleted == false)
                                      .OrderBy(x => x.Priority)
                                      .Select(SubTasksViewModel.GetSubtasts);



            return this.PartialView("_ListOfSubTasksPartial", listOfsubtasks);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return this.View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult CreateOne(SubTaskInputModel inputSubtask, int taskID)
        {

            if (!ModelState.IsValid)
            {
                throw new HttpException("The model is not valid!");
            }

            var subtask = new SubTask
            {
                Title = inputSubtask.SubtaskTitle,
                Priority = inputSubtask.SubtaskPriority,
                MyTaskID = taskID,
            };

            this.Data.SubTasks.Add(subtask);
            this.Data.SaveChanges();

            var subtasks = this.Data.SubTasks.SearchFor(x => x.MyTaskID == taskID );


            return this.Json(subtasks, JsonRequestBehavior.AllowGet );
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ICollection<SubTaskInputModel> inpSubtasks, int taskID)
        {

            if (inpSubtasks.Count > 30)
            {
                throw new HttpException("To many requests!");
            }

            if (!ModelState.IsValid)
            {
                throw new HttpException("ModelState is not valid!");
            }

            var userId = this.CurrentUser.GetUserId();

            var task = this.Data.Tasks.SearchFor(x => x.UserID == userId && x.ID == taskID).FirstOrDefault();

            var listSubtasks = new List<SubTask>();

            foreach (var inpSubtask in inpSubtasks)
            {

                listSubtasks.Add( new SubTask
                {
                    Title = inpSubtask.SubtaskTitle,
                    Priority = inpSubtask.SubtaskPriority,
                    MyTaskID = task.ID
                });
            }

            foreach (var subtask in listSubtasks)
            {
                this.Data.SubTasks.Add(subtask);
            }

            this.Data.SaveChanges();

            var returnetCollectionOfSubtasks = this.Data.SubTasks
                .SearchFor(x => x.MyTaskID == task.ID && x.MyTask.UserID == userId)
                .Select(SubTasksViewModel.GetSubtasts);

            return this.Json(returnetCollectionOfSubtasks);
        }

    }
}