namespace Tasks.Tests.DataTests
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Tasks.Models;


    [TestClass]
    public class TestGetAllMyTaskFromRepository : BaseTest
    {
        [TestMethod] 
        public void GetAllMyTasks_WhenMyTasksFromUser_ShouldReturnCollectionOnUserTasks()
        {
            var user = this.CreateUser();

            this.Data.Users.Add(user);
            this.Data.SaveChanges();

            var listOfTasks = new List<MyTask>();

            for (int i = 0; i < 10; i++)
            {
                listOfTasks.Add(new MyTask
                {
                    Title = this.Generator.RandomMixedString(5 , 20),
                    DateOnCreate = DateTime.Now,
                    UserID = user.Id,
                    
                });
            }

            foreach (var task in listOfTasks)
            {
                this.Context.Tasks.Add(task);
            }

            this.Context.SaveChanges();


            var actualListOftasks = this.Data.Tasks.All().Where(x => x.User.Email == user.Email).ToList<MyTask>();

            Assert.IsNotNull(listOfTasks, "Expected list is empty!");
            Assert.IsNotNull(actualListOftasks, "Actual list is empty!");
            Assert.AreEqual(listOfTasks.Count, actualListOftasks.Count);
            CollectionAssert.AreEqual(listOfTasks, actualListOftasks);

        }
    }
}
