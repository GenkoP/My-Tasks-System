namespace Tasks.WebClient.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Tasks.Data.Repositories;
    using Tasks.WebClient.Models.ViewModels;
    using Tasks.WebClient.Providers;

    [Authorize]
    public class SearchingController : BaseController
    {

        public SearchingController(ITaskManagerData data, ICurrentUserIdProvider userId)
            : base(data, userId)
        {
            
        }

        public ActionResult SearchByDate(DateTime date)
        {
            var currentUserId = this.CurrentUser.GetUserId();

            var currentTasks = this.Data.Tasks
                                    .SearchFor(x => x.DateToEnd == date 
                                                && x.UserID == currentUserId 
                                                && x.IsCompleted == false
                                              )
                                    .OrderByDescending(x => x.Priority)
                                    .Select(MyTaskViewModel.GetTasks);


            return this.PartialView("_ListOfMyTaskPartial", currentTasks);
        }
    }
}