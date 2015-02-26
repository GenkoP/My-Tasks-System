namespace Tasks.Tests.ControllersTest
{

    using System;
    using System.Web;
    using System.Net;
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
        private MyTask myTask;
        private SubTask subtask;
        private ICollection<MyTask> listOfMyTasks;
        private List<SubTask> listOfSubtasks;

        [TestInitializeAttribute]
        public void Init()
        {
            this.myTask = this.RandomMyTask("someUserID", PriorityType.Medium);

            this.subtask = new SubTask
            {
                Title = "Test subtask title",
                Priority = PriorityType.Medium,
            };

            this.listOfMyTasks = new List<MyTask>();

            this.listOfSubtasks = new List<SubTask>();

            this.MockTaskManagerData.Setup(x => x.Tasks.Add(It.Is<MyTask>(c => true))).Callback(() =>
            {
                this.listOfMyTasks.Add(this.myTask);

            });

            this.MockTaskManagerData.Setup(data => data.SubTasks.Add(It.Is<SubTask>(x => true)))
                    .Callback(() => this.listOfSubtasks.Add(this.subtask));

        }

        [TestMethod]
        public void CreateNewMyTasks_When_InputModelIsValid_AndSubtaskCollIsNull_ShouldTaskCounteinsInList()
        {

            var data = this.MockTaskManagerData.Object;

            var taskContr = new TasksController(data, this.CurrentUserIdProvider);

            var taskInput = new MyTaskInputModel
            {
                Title = "input title",
            };

            var action = taskContr.Create(taskInput, null);

            Assert.IsTrue(listOfMyTasks.Contains(myTask));
        }


        [TestMethod]
        public void CreateMyTasksWithSubtasks_WhenTaskHave2Subtasks_ShouldReturnMyTaskAnd2SubTasks()
        {

            var datas = this.MockTaskManagerData.Object;

            var taskContr = new TasksController(datas, this.CurrentUserIdProvider);

            var taskInput = new MyTaskInputModel
            {
                Title = "input title",
            };

            var inputCollSubtasks = new List<SubTaskInputModel>
            {
                new SubTaskInputModel{ SubtaskPriority = PriorityType.Low , SubtaskTitle = "dasdasdas"},
                new SubTaskInputModel{ SubtaskPriority = PriorityType.Low , SubtaskTitle = "123Babsba"},
            };

            var action = taskContr.Create(taskInput, inputCollSubtasks);

            Assert.IsTrue(listOfMyTasks.Contains(myTask));
            Assert.IsNotNull(this.listOfSubtasks);
            Assert.AreEqual(this.listOfSubtasks.Count, inputCollSubtasks.Count);

        }


        [TestMethod]
        [ExpectedException(typeof(HttpException))]
        public void CreateMyTaskWithSubtasks_WhenSubtasksCountIsBigerFrom10_ShouldReturnException()
        {
            var datas = this.MockTaskManagerData.Object;

            var taskContr = new TasksController(datas, this.CurrentUserIdProvider);

            var taskInput = new MyTaskInputModel
             {
                 Title = "input title",
             };

            var inputCollSubtasks = new List<SubTaskInputModel>();

            for (int i = 0; i < 11; i++)
            {
                inputCollSubtasks.Add(new SubTaskInputModel
                {
                    SubtaskTitle = "Test subtask title " + i,
                    SubtaskPriority = PriorityType.Low,
                });
            }


            var action = taskContr.Create(taskInput, inputCollSubtasks);

        }


        
    }
}
