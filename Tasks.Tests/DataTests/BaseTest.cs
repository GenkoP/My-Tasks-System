namespace Tasks.Tests.DataTests
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using System;
    using System.Transactions;
    using System.Collections.Generic;

    using Tasks.Models;
    using Tasks.Data;
    using Tasks.Data.Repositories;
    using Tasks.Common;


    [TestClass]
    public class BaseTest
    {

        protected const string USER_EMAIL = "test@test.com";

        private TransactionScope tran;
        private DataContext context;
        private ITaskManagerData data;
        private IRandomGenerator generator;


        [TestInitialize]
        public void Init()
        {
            this.tran = new TransactionScope();
            this.context = new DataContext();
            this.data = new TaskManagerData(this.context);

            this.generator = new RandomGenerator();
        }

        protected ITaskManagerData Data
        {
            get { return this.data; }
        }

        protected IDataContext Context
        {
            get { return this.context; }
        }

        protected IRandomGenerator Generator
        {
            get { return this.generator; }
        }

        [TestCleanup]
        public void CleanUp()
        {
            this.tran.Dispose();
        }

        protected MyTask CreateMyTask(string title , User user )
        {
            var task = new MyTask
            {
                Title = title,
                DateOnCreate = DateTime.Now,
                DateToEnd = DateTime.Now,
                Priority = PriorityType.Low,
                UserID = user.Id,
            };

            return task;
        }

        protected MyTask CreateMyTask(string title, User user,string dicription, DateTime dateEnd)
        {
            var task = new MyTask
            {
                Title = title,
                Description = dicription,
                DateOnCreate = DateTime.Now,
                DateToEnd = dateEnd,
                UserID = user.Id,
            };

            return task;
        }

        protected User CreateUser(string email = USER_EMAIL)
        {
            var user = new User
            {
                Email = email,
                UserName = email,
            };

            return user;
        }

    }
}
