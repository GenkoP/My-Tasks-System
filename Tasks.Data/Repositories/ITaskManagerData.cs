namespace Tasks.Data.Repositories
{
    using Tasks.Models;

    public interface ITaskManagerData
    {

        IGenericRepository<MyTask> Tasks { get;  }

        IGenericRepository<SubTask> SubTasks { get;  }

        IGenericRepository<User> Users { get;  }

        void Dispose();

        void SaveChanges();

    }
}
