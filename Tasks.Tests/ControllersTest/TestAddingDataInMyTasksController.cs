namespace Tasks.Tests.ControllersTest
{

    using System;
    using System.Web.Mvc;
    using System.Collections.Generic;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    
    using Moq;

    using Tasks.Data.Repositories;
    using Tasks.Models;
    using Tasks.WebClient.Models.InputModels;
    using Tasks.WebClient.Controllers;



    [TestClass]
    public class TestAddingDataInMyTasksController : BaseTestController
    {
        [TestMethod]
        public void CreateNewMyTasks_When_InputModelIsValid_AndSubtaskCollIsNull_ShouldRedirectToIndexPage()
        {

            var listOfMyTasks = new List<MyTask>();

            var myTask = new MyTask
            {
                  Title = "mytask title",
                 
            };

            this.MoqTaskManagerData.Setup(x => x.Tasks.Add(It.Is<MyTask>(c => true))).Callback(() =>
            {
                listOfMyTasks.Add(myTask);

            });

            var data = this.MoqTaskManagerData.Object;


            var taskContr = new TasksController(data, this.CurrentUserIdProvider);

            var taskInput = new MyTaskInputModel
            {
                 Title = "input title",
            };

            var action = taskContr.Create(taskInput, null);

            Assert.IsTrue(listOfMyTasks.Contains(myTask));
        }
            
    }
}
