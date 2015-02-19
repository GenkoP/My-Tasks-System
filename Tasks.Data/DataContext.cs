namespace Tasks.Data
{
    using System.Data.Entity;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using Tasks.Models;
    using Tasks.Data.Migrations;

    public class DataContext : IdentityDbContext<User> , IDataContext
    {
        public DataContext()
            : base("TaskManagerDb", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataContext, Configuration>());
        }

        public IDbSet<MyTask> Tasks { get; set; }

        public IDbSet<SubTask> SubTasks { get; set; }


        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public new void SaveChanges()
        {
            base.SaveChanges();
        }

        public static DataContext Create()
        {
            return new DataContext();
        }
    }
}
