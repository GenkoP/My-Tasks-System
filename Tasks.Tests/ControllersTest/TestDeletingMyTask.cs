namespace Tasks.Tests.ControllersTest
{
    using Moq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Tasks.Models;
    using Tasks.WebClient.Controllers;
    using Tasks.Data.Repositories;

    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Web;
    

    [TestClass]
    public class TestDeletingMyTask : BaseTestController
    {

        private IList<MyTask> myTaskColl;
        private MyTask deletableMyTask;

        [TestInitialize]
        public void PrepareTest()
        {
            this.myTaskColl = this.GenerateMytaskColl(BaseTestController.USER_ID, 5);


            this.deletableMyTask = new MyTask
           {
               ID = 5,
               UserID = BaseTestController.USER_ID,
           };


           this.myTaskColl.Add(deletableMyTask);

           var moqRepos = new Mock<IGenericRepository<MyTask>>();

           moqRepos.Setup(rep => rep.All()).Returns(this.myTaskColl.AsQueryable());

           moqRepos.Setup(rep => rep.Delete(It.Is<MyTask>(s => true))).Callback(() => this.myTaskColl.Remove(deletableMyTask));

           var repObj = moqRepos.Object;

           this.MoqTaskManagerData.Setup(data => data.Tasks).Returns(repObj);

        }
        
        [TestMethod]
        public void DeleteMyTask_WhenMyTaskExistInDatabase_ShoudeRemoveMyTaskFromList()
        {

            var taskData = this.MoqTaskManagerData.Object;

            var tasksController = new TasksController(taskData , this.CurrentUserIdProvider);

            var result = tasksController.Delete(5);

            Assert.IsFalse(this.myTaskColl.Contains(this.deletableMyTask));

        }

        [TestMethod]
        [ExpectedException(typeof(HttpException))]
        public void DeletingMyTask_WhenMyTasksNotExistInDatabase_ShouldThrowHttpException()
        {
           

            var taskData = this.MoqTaskManagerData.Object;

            var tasksController = new TasksController(taskData, this.CurrentUserIdProvider);

            var result = tasksController.Delete(999);
        }

        [TestMethod]
        [ExpectedException(typeof(HttpException))]
        public void DeletingMyTask_WhenMyTaksIsInDifrentUser_ShouldThrowHttpException()
        {
            var taskOnOtherUser = new MyTask
            {
                ID = 100,
                UserID = "difrent current user",

            };

            this.myTaskColl.Add(taskOnOtherUser);


            var taskData = this.MoqTaskManagerData.Object;

            var tasksController = new TasksController(taskData, this.CurrentUserIdProvider);

            var result = tasksController.Delete(taskOnOtherUser.ID);


        }



    }
}
