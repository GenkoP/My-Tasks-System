namespace Tasks.WebClient.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using Tasks.Data.Repositories;
    using Tasks.Data;
    using Tasks.WebClient.Providers;
    using Tasks.Models;

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


        protected ActionResult ModelStateIsValid()
        {
            if (!ModelState.IsValid)
            {

                var errors = ModelState.Values.SelectMany(x => x.Errors).ToArray();


                return View("Error", errors);
            }
            return null;
        }


        protected HttpNotFoundResult ObjectISNull(MyTask task)
        {
            if (task == null)
            {
                return HttpNotFound();
            }

            return null;
        }

        protected void RenderDeleteTask(int id)
        {
            var currnetUserId = this.CurrentUser.GetUserId();

            var task = this.data.Tasks.SearchFor(x => x.UserID == currnetUserId && x.ID == id).FirstOrDefault();

            this.ObjectISNull(task);

            this.Data.Tasks.Delete(task);
            this.Data.SaveChanges();

        }

    }
}