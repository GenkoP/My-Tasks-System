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

           // var date = DateTime.Parse("03/03/2015");

            var currentTasks = this.Data.Tasks.All()
                .Where(x => x.DateToEnd == date && x.UserID == currentUserId && x.IsCompleted == false)
                .Select(MyTaskViewModel.GetTasks);



            return this.PartialView("_ListOfMyTaskPartial", currentTasks);
        }

       
        public ActionResult SearchByPriority()
        {
            var currentTasks = new List<MyTaskViewModel> 
            {
           
                new MyTaskViewModel
                {
                     DateToEnd = DateTime.Now,
                     Title = "baba mesa",
                     Preority = Tasks.Models.PreorityType.Important
                },

                new MyTaskViewModel
                {
                     DateToEnd = DateTime.Now,
                     Title = "kuma lisa",
                     Preority = Tasks.Models.PreorityType.Important
                },

            };

            return this.PartialView("_ListOfMyTaskPartial", currentTasks.AsQueryable());
        }
    }
}