namespace Tasks.WebClient.Areas.Administration.Controllers
{

    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Tasks.Data.Repositories;
    using Tasks.WebClient.Areas.Administration.Views.Admins;
    using Tasks.WebClient.Controllers;
    using Tasks.WebClient.Infrastructure.Providers;


    public class AdminsController : BaseController
    {
        public AdminsController(ITaskManagerData data , ICurrentUserIdProvider userId)
            : base(data , userId)
        {

        }

        public ActionResult Index()
        {

            var users = this.Data.Users.All().Select(UserViewModel.GetUsers).ToList();

            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DelteUser(string id)
        {

            var userToDelete = this.Data.Users.SearchFor(x => x.Id == id).FirstOrDefault();

            this.ObjectIsNull(userToDelete);

            this.Data.Users.Delete(userToDelete);

            return RedirectToAction("Index");
        }

    }
}