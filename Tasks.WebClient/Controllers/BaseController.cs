namespace Tasks.WebClient.Controllers
{
    using System.Web.Mvc;

    using Tasks.Data.Repositories;
    using Tasks.Data;
    using Tasks.WebClient.Providers;

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



    }
}