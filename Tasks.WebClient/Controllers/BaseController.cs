namespace Tasks.WebClient.Controllers
{
    using System.Web.Mvc;

    using Tasks.Data.Repositories;
    using Tasks.Data;

    public abstract class BaseController : Controller
    {

        protected ITaskManagerData data;

        public BaseController(ITaskManagerData data)
        {
            this.data = data;
        }
    }
}