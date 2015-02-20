namespace Tasks.Tests.DataTests
{

    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Tasks.Models;

    [TestClass]
    public class TestDeletingMyTasksFromData : BaseTest
    {
        [TestMethod]
        public void DeleteMyTask_WhenMyTaskIsAddedInDb_ShouldReturnEmpty()
        {

            var user = this.CreateUser();

            this.Data.Users.Add(user);

            string title = "Must be test all!";

            var task = this.CreateMyTask(title, user);

            this.Data.Tasks.Add(task);
            this.Data.SaveChanges();


            var addedTask = this.Context.Tasks.Where(x => x.Title == title && x.ID == task.ID).FirstOrDefault();


            this.Data.Tasks.Delete(task);
            this.Data.SaveChanges();


            bool isDeleted = this.Context.Tasks.Any(x => x.ID == addedTask.ID && x.ID == addedTask.ID);

            var actualTask = this.Context.Tasks.Where(
                                                       x => x.ID == addedTask.ID
                                                       && x.ID == addedTask.ID
                                                     ).FirstOrDefault();

            Assert.IsFalse(isDeleted);
            Assert.IsNull(actualTask);
        }

        
    }
}
