
namespace Tasks.Tests.ControllersTest
{

    using System;
    using System.Linq;
    using System.Data.Entity;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Web.Mvc;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using Tasks.Models;
    using Tasks.Data.Repositories;
    using Tasks.WebClient.Controllers;
  

    [TestClass]
    public class TestMyTasksController : BaseTestController
    {
        [TestMethod]
        public void GetingIndex_WhenDbHeveManyTasks_ShouldReturnIQueryableOfDate()
        {
            var listOfMyTasks = PrepareList();

            var data = MockinDataLayer(listOfMyTasks);

            var tasksControll = new TasksController(data, this.CurrentUserIdProvider);

            var result = tasksControll.Index() as ViewResult;
            var model = result.Model as IQueryable<DateTime>;

            Assert.IsNotNull(model, "The model is null!");
        }
      

        [TestMethod]
        public void Index_WhenGetingIndex_ShouldReturnSortedIQueryableOfDateTyme()
        {
             var prepareListOfMyTasks = this.PrepareList();

             var model = CreateControllerAndModel(prepareListOfMyTasks);

             var currentDate = DateTime.Now.Date;

             var sortedList = this.MyTasksList
                    .Where(x => x.DateToEnd >= currentDate
                            && x.UserID == BaseTestController.USER_ID
                            && x.IsCompleted == false)
                    .GroupBy(x => x.DateToEnd)
                    .Where(g => g.Count() >= 1)
                    .Select(g => g.Key);



             Assert.IsFalse(sortedList.Count() == 0);
             Assert.AreEqual(sortedList.ToList().Count, model.ToList().Count);
             CollectionAssert.AreEqual(sortedList.ToList(), model.ToList());
             
        }

        private IEnumerable<MyTask> PrepareList()
        {
            var currentDate = DateTime.Now.Date;
            var listOfMyTasks = this.MyTasksList.Where(x => x.DateToEnd >= currentDate
                            && x.UserID == BaseTestController.USER_ID
                            && x.IsCompleted == false);
            return listOfMyTasks;
        }

        private IQueryable<DateTime> CreateControllerAndModel(IEnumerable<MyTask> listOfMyTasks)
        {
            var data = MockinDataLayer(listOfMyTasks);

            var tasksControll = new TasksController(data, this.CurrentUserIdProvider);

            var result = tasksControll.Index() as ViewResult;
            var model = result.Model as IQueryable<DateTime>;

            return model;

        }

        private ITaskManagerData MockinDataLayer(IEnumerable<MyTask> listOfMyTasks)
        {
            var moqRepository = new Mock<IGenericRepository<MyTask>>();
            moqRepository.Setup(r => r.SearchFor(It.IsAny<Expression<Func<MyTask, bool>>>())).Returns(listOfMyTasks.AsQueryable());
            var repMyTasks = moqRepository.Object;

            this.MoqTaskManagerData.Setup(u => u.Tasks).Returns(repMyTasks);
            var data = this.MoqTaskManagerData.Object;
            return data;
        }

    }
}
