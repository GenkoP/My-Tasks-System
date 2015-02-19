namespace Tasks.Data.Migrations
{
    using System;
    using System.Linq;
    using System.Data.Entity;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using Tasks.Models;
    using Tasks.Data.Repositories;

    public sealed class Configuration : DbMigrationsConfiguration<DataContext>
    {
        //private UserManager<User> userManager;
        //private const string USER_EMAIL = "some@mail.com";

        //public Configuration()
        //{
        //    this.AutomaticMigrationsEnabled = true;
        //    this.AutomaticMigrationDataLossAllowed = true;
        //}

        //protected override void Seed(DataContext context)
        //{

        //    this.userManager = new UserManager<User>(new UserStore<User>(context));

        //    this.GenerateUser(context);
        //    this.GenerateTasks(context);

        //}


        //private void GenerateUser(DataContext context)
        //{

        //    if (context.Users.Any())
        //    {
        //        return;
        //    }

        //    var user = new User
        //    {
        //        Email = USER_EMAIL,
        //        UserName = USER_EMAIL,
        //    };


        //    this.userManager.Create(user, "123456");

        //    context.SaveChanges();


        //}

        //private void GenerateTasks(DataContext context)
        //{
        //    if (context.Tasks.Any())
        //    {
        //        return;
        //    }
        //    else
        //    {

        //        var userId = context.Users.Where(x => x.Email == USER_EMAIL)
        //                                  .Select(x => x.Id)
        //                                  .FirstOrDefault();

        //        context.Tasks.Add(new MyTask
        //        {
        //            UserID = userId,
        //            Title = "Must get up early!",
        //            DateOnCreate = DateTime.Now,
        //            DateToEnd = DateTime.Parse("16/02/2015"),
        //            Priority = PriorityType.Important,
        //            Description = "Must get up early, becouse I have to study ASP MVC5!"
        //        });

        //        context.Tasks.Add(new MyTask
        //        {
        //            UserID = userId,
        //            Title = "Need to brush my teeth!",
        //            DateOnCreate = DateTime.Now,
        //            DateToEnd = DateTime.Parse("17/02/2015"),
        //            Priority = PriorityType.Important,
        //            Description = "need to brush my teeth, because it must be healthy!!!"
        //        });

        //        context.Tasks.Add(new MyTask
        //        {
        //            UserID = userId,
        //            Title = "My mom have birthday!",
        //            DateOnCreate = DateTime.Now,
        //            DateToEnd = DateTime.Parse("18/02/2015"),
        //            Priority = PriorityType.Important,
        //            Description = "My mom have birthday, buy gift!"
        //        });

        //        var subTask = new HashSet<SubTask>
        //            {
        //                new SubTask
        //                {
        //                    Title = "Write person information!",
        //                },
        //                new SubTask
        //                {
        //                    Title = "Write projects!",
        //                },
        //                new SubTask
        //                {
        //                    Title = "Write education!",
        //                },
        //            };

        //        var task = new MyTask
        //    {
        //        UserID = userId,
        //        Title = "I must go to interview!",
        //        DateOnCreate = DateTime.Now,
        //        DateToEnd = DateTime.Parse("17/02/2015"),
        //        Priority = PriorityType.Important,
        //        Description = "Prepare your CV!",
        //        SubTasks = subTask
        //    };


        //        context.Tasks.Add(task);

        //        context.Tasks.Add(new MyTask
        //        {
        //            UserID = userId,
        //            Title = "Must go to work",
        //            DateOnCreate = DateTime.Now,
        //            DateToEnd = DateTime.Parse("03/02/2015"),
        //            Priority = PriorityType.Important,
        //            Description = "Get ready to work!",
        //            SubTasks = new List<SubTask>
        //            {
        //                new SubTask
        //                {
        //                    Title = "Put your clothes",
        //                },
        //                new SubTask
        //                {
        //                    Title = "Drink coffe!",
        //                },
        //                new SubTask
        //                {
        //                    Title = "Breakfast!",
        //                },
        //            }
        //        });

        //        context.Tasks.Add(new MyTask
        //        {
        //            UserID = userId,
        //            Title = "Buy food!",
        //            DateOnCreate = DateTime.Now,
        //            DateToEnd = DateTime.Parse("20/02/2015"),
        //            Priority = PriorityType.Important,
        //            Description = "Buy food to cook!",
        //            SubTasks = new List<SubTask>
        //            {
        //                new SubTask
        //                {
        //                    Title = "Potatoes!",
        //                },
        //                new SubTask
        //                {
        //                    Title = "Tomatoes!",
        //                },
        //                new SubTask
        //                {
        //                    Title = "Cucumbers!",
        //                },
        //            }
        //        });

        //        context.SaveChanges();

        //   }

 //       }

    }
}
