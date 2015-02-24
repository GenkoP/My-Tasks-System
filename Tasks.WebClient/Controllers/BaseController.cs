namespace Tasks.WebClient.Controllers
{
    using System.Linq;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Mvc;

    using Tasks.Data.Repositories;
    using Tasks.Data;
    using Tasks.WebClient.Infrastructure.Providers;
    using Tasks.Models;
    using Tasks.WebClient.Models.InputModels;

    [Authorize]
    public abstract class BaseController : Controller
    {

        private ITaskManagerData data;
        private ICurrentUserIdProvider currentUser;

        public BaseController(ITaskManagerData data, ICurrentUserIdProvider userId)
        {
            this.data = data;
            this.currentUser = userId;
        }


        protected ITaskManagerData Data
        {
            get { return this.data; }
        }

        protected ICurrentUserIdProvider CurrentUser
        {
            get { return this.currentUser; }
        }

        protected void SubtasksIsValid(ICollection<SubTaskInputModel> subtasks)
        {
            if (subtasks.Count > 10)
            {
                throw new HttpException(400, "The subtask count must be to less from 10!");
            }
 
        }

        protected void ObjectIsNull(object task)
        {
            if (task == null)
            {
                throw new HttpException(404, "Object not found");
            }

        }

        protected void RenderDeleteTask(int id)
        {
            var currnetUserId = this.CurrentUser.GetUserId();

            var task = this.data.Tasks.SearchFor(x => x.UserID == currnetUserId && x.ID == id).FirstOrDefault();

            this.ObjectIsNull(task);

            this.Data.Tasks.Delete(task);
            this.Data.SaveChanges();

        }

    }
}