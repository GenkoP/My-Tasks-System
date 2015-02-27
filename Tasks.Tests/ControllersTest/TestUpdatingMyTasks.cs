namespace Tasks.Tests.ControllersTest
{

    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Linq.Expressions;
    using System.Collections.Generic;

    using Moq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Tasks.Models;
    using Tasks.Data.Repositories;
    using Tasks.WebClient.Controllers;
    using Tasks.WebClient.Models.InputModels;
    using Tasks.WebClient.Models.ViewModels;

    [TestClass]
    public class TestUpdatingMyTasks : BaseTestController
    {

        private MyTask  updatedTask;

        [TestInitialize]
        public void PrepareTest()
        {
             this.updatedTask = this.RandomMyTask(BaseTestController.USER_ID, PriorityType.Low);

            this.MyTasksList.Add(updatedTask);


            this.MockMyTaskRep.Setup(rep => rep.All()).Returns(this.MyTasksList.AsQueryable());

            this.MockTaskManagerData.Setup(data => data.Tasks).Returns(this.MockMyTaskRep.Object);

        }

        [TestMethod]
        public void GetMyTaskForUpdate_WhenMyTaskExistInDb_ShouldReturnMyTask()
        {


            var taskData = this.MockTaskManagerData.Object;

            var controller = new TasksController(taskData, this.CurrentUserIdProvider);

            var result = controller.Update(this.updatedTask.ID) as ViewResult;

            

            var nameView = result.ViewName;

            var model = result.Model as MyTaskViewModel;

            
            Assert.AreEqual(model.Title, this.updatedTask.Title);
            Assert.AreEqual(model.ID, this.updatedTask.ID);

        }

        [TestMethod]
        [ExpectedException(typeof(HttpException))]
        public void GetMyTaskForUpdate_WhenMyTaskNoExistInDb_ShouldThrowHttpException()
        {

            var taskData = this.MockTaskManagerData.Object;

            var controller = new TasksController(taskData, this.CurrentUserIdProvider);

            var result = controller.Update(99999);

        }


        [TestMethod]
        [ExpectedException(typeof(HttpException))]
        public void GetMyTaskForUpdate_WhenMyTaskUserIDIsDiffrentFromCurrentUser_ShouldThrowHttpException()
        {
            var diffrentUserTask = new MyTask
            {
                ID = 9999,
                Title = "Title on diffrent user task",
                UserID = "User id on diffrent user from current user",
            };

            this.MyTasksList.Add(diffrentUserTask);

            var taskData = this.MockTaskManagerData.Object;

            var controller = new TasksController(taskData, this.CurrentUserIdProvider);

            var result = controller.Update(diffrentUserTask.ID);

        }

        [TestMethod]
        public void UpdatedMyTask_WhenUpdatePriorityTitleAndSubtasksCollIsNull_ShouldReturnUpdatedMyTaskTitle()
        {

            var taskForUpdate = this.RandomMyTask(BaseTestController.USER_ID, PriorityType.Low);

            this.MyTasksList.Add(taskForUpdate);

            var inputMyTaskModel = new MyTaskInputModel
            {
                ID = taskForUpdate.ID,
                Title = "Updated tittle",
                Priority = PriorityType.Important,

            };


            this.MockMyTaskRep.Setup(rep => rep.Update(It.Is<MyTask>(x => true)))
                .Callback(() => taskForUpdate.Title = inputMyTaskModel.Title);

            this.MockMyTaskRep.Setup(rep => rep.Update(It.Is<MyTask>(x => true)))
                .Callback(() => taskForUpdate.Priority = inputMyTaskModel.Priority);


            this.MockTaskManagerData.Setup(data => data.Tasks).Returns(this.MockMyTaskRep.Object);


            var controller = new TasksController(this.MockTaskManagerData.Object, this.CurrentUserIdProvider);

       

            var result = controller.Update(inputMyTaskModel, null);

            

            Assert.AreEqual(inputMyTaskModel.Title, taskForUpdate.Title);
            Assert.AreEqual(inputMyTaskModel.Priority, taskForUpdate.Priority);

        }

    }
}
