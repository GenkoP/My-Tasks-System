namespace Tasks.Data.Migrations
{
    using System;
    using System.Linq;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;

    using Tasks.Models;
    using Tasks.Data.Repositories;

    public sealed class Configuration : DbMigrationsConfiguration<Tasks.Data.DataContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            //this.ContextKey = "";
        }

        protected override void Seed(Tasks.Data.DataContext context)
        {
            //TaskManagerData data = new TaskManagerData();

            //data.Tasks.Add(new MyTask { Preority = PreorityType.Important,
            //                             Title = "Must get up early!"});





        }
    }
}
