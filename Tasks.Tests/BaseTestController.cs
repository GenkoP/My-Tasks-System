﻿namespace Tasks.Tests.ControllersTest
{

    using System;
    using System.Linq;
    using System.Collections.Generic;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using Tasks.Models;
    using Tasks.Common;
    using Tasks.WebClient.Infrastructure.Providers;
    using Tasks.Data.Repositories;

    [TestClass]
    public class BaseTestController
    {

        protected const string USER_ID = "testUserID";

        private IList<MyTask> mytasksColl;
        private IRandomGenerator generator;
        private ICurrentUserIdProvider currentUserId;
        private Mock<ITaskManagerData> mockTaskManagerData;
        private Mock<IGenericRepository<MyTask>> mockTaskRepos;

        [TestInitialize]
        public void MyTestMethod()
        {

            this.generator = new RandomGenerator();

            this.mytasksColl = this.GenerateMytaskColl(USER_ID );

            this.mockTaskRepos = new Mock<IGenericRepository<MyTask>>();

            this.mockTaskManagerData = new Mock<ITaskManagerData>();

            this.MoqingCurrentUserId();
        }

        protected IList<MyTask> MyTasksList 
        {
            get { return this.mytasksColl; }
            set { this.mytasksColl = value; }
        }

        protected ICurrentUserIdProvider CurrentUserIdProvider { get { return this.currentUserId; } }

        protected Mock<ITaskManagerData> MockTaskManagerData { get { return this.mockTaskManagerData; } }

        protected Mock<IGenericRepository<MyTask>> MockMyTaskRep { get { return this.mockTaskRepos; } }
            
        protected IList<MyTask> GenerateMytaskColl(string userId, int tasksTodayCount = 5)
        {

            this.mytasksColl = new List<MyTask>();

            var count = this.generator.RandomNumber(0, 10);
            for (int i = 0; i < count; i++)
            {
                this.mytasksColl.Add(this.RandomMyTask(userId, PriorityType.Low));
            }

            for (int i = 0; i < tasksTodayCount; i++)
            {
                this.mytasksColl.Add(this.RandomMyTask(userId, PriorityType.Low , 0,0));
            }

            count = this.generator.RandomNumber(0, 10);
            for (int i = 0; i < count; i++)
            {
                this.mytasksColl.Add(this.RandomMyTask(userId, PriorityType.Low, -10, -2));
            }


            count = this.generator.RandomNumber(0, 10);
            for (int i = 0; i < count; i++)
            {
                this.mytasksColl.Add(this.RandomMyTask(userId, PriorityType.Medium));
            }

            count = this.generator.RandomNumber(0, 10);
            for (int i = 0; i < count; i++)
            {
                this.mytasksColl.Add(this.RandomMyTask(userId, PriorityType.Important));
            }


            return this.mytasksColl;

        }

        protected MyTask RandomMyTask(string userId , PriorityType priority , int dateMin = 2 , int dateMax = 10)
        {
            var task = new MyTask
            {
                 ID = this.generator.RandomNumber(1,1000),
                Title = this.generator.RandomMixedString(7, 20),
                DateOnCreate = DateTime.Now,
                DateToEnd = DateTime.Now.AddDays(this.generator.RandomNumber(dateMin, dateMax)),
                Priority = priority,
                UserID = userId,
            };

            return task;
        }

        private void MoqingCurrentUserId()
        {
            var moqUser = new Mock<ICurrentUserIdProvider>();
            moqUser.Setup(x => x.GetUserId()).Returns(USER_ID);
            this.currentUserId = moqUser.Object;
        }

    }
}
