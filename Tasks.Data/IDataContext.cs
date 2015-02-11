namespace Tasks.Data
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    using Tasks.Models;

    public interface IDataContext
    {

        IDbSet<MyTask> Tasks { get; set; }

        IDbSet<SubTask> SubTasks { get; set; }

        IDbSet<T> Set<T>() where T : class;

        DbEntityEntry<T> Entry<T>(T entity) where T : class;

        void Dispose();

        void SaveChanges();

    }
}
