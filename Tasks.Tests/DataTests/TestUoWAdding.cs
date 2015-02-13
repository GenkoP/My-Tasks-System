namespace Tasks.Tests.DataTests
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using System;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Validation;
    using System.Linq;

    using Tasks.Data;
    using Tasks.Models;


    [TestClass]
    public class TestUoWAdding : BaseTest
    {

        [TestMethod]
        public void AddMyTask_WhenTasksIsValid_ShouldAddToDb()
        {
            var user = this.CreateUser();

            this.Data.Users.Add(user);

            string title = "Must be test all!";

            var expecterdTask = this.CreateMyTask(title, user);

            this.Data.Tasks.Add(expecterdTask);
            this.Data.SaveChanges();

            var actual = this.Context.Tasks.Where(x => x.Title == title).FirstOrDefault();

            Assert.IsNotNull(actual.ID);
            Assert.AreEqual(expecterdTask, actual);

        }

        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void AddMyTask_WhenMyTaskIsEmptyObject_ShouldSendException()
        {
            var expecterdTask = new MyTask();

            this.Data.Tasks.Add(expecterdTask);
            this.Data.SaveChanges();

        }


        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void AddMyTask_WhenMyTaskTitleIsNull_ShouldSendExecption()
        {
            var user = this.CreateUser();
            this.Data.Users.Add(user);

            var expecterdTask = this.CreateMyTask(null, user);

            this.Data.Tasks.Add(expecterdTask);
            this.Data.SaveChanges();
            
        }

        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void AddMyTask_WhenMyTaskTitleIsMissing_ShouldSendException()
        {
            var user = this.CreateUser();
            this.Data.Users.Add(user);

            var expecterdTask = new MyTask
            {
                DateOnCreate = DateTime.Now,
                UserID = user.Id,
            };

            this.Data.Tasks.Add(expecterdTask);
            this.Data.SaveChanges();

        }

        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void AddMyTask_WhenMyTaskTitleIsStringLenght31_ShouldSendException()
        {
            var user = this.CreateUser();
            this.Data.Users.Add(user);

            string bigStringLenght = this.Generator.RandomMixedString(31, 31);

            var expecterdTask = this.CreateMyTask(bigStringLenght, user);

            this.Data.Tasks.Add(expecterdTask);
            this.Data.SaveChanges();



        }

        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void AddMyTask_WhenMyTaskTitleStringLenghtIs1_ShouldSendException()
        {
            var user = this.CreateUser();
            this.Data.Users.Add(user);

            string smallStringLenght = "b";

            var expecterdTask = this.CreateMyTask(smallStringLenght, user);

            this.Data.Tasks.Add(expecterdTask);
            this.Data.SaveChanges();


        }


        [TestMethod]
        [ExpectedException(typeof(DbUpdateException))]
        public void AddMyTask_WhenMyTaskDateOnCreateIsMissing_ShouldThrowException()
        {
            var user = this.CreateUser();
            this.Data.Users.Add(user);

            string title = "Test title ";

            var expecterdTask = new MyTask
            {
                Title = title,
                UserID = user.Id,

            };

            this.Data.Tasks.Add(expecterdTask);
            this.Data.SaveChanges();
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void AddMyTask_WhenMyTaskDateOnCreateIsNoValidDateTime_ShouldThrowException()
        {
            var user = this.CreateUser();
            this.Data.Users.Add(user);

            string title = "Test title ";
            string date = "adsasdasdas";

            var expecterdTask = new MyTask
            {
                Title = title,
                UserID = user.Id,
                DateOnCreate = DateTime.Parse(date),

            };

            this.Data.Tasks.Add(expecterdTask);
            this.Data.SaveChanges();
        }

        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException))]
        public void AddMyTask_WhenMyTaskWhitEmptyUser_ShouldThrowException()
        {
            
            string title = "Test title ";

            var expecterdTask = new MyTask
            {
                Title = title,
                DateOnCreate = DateTime.Now,

            };

            this.Data.Tasks.Add(expecterdTask);
            this.Data.SaveChanges();
        }

    }
}
