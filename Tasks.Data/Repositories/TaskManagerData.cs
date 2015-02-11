namespace Tasks.Data.Repositories
{
    using System;
    using System.Collections.Generic;

    using Tasks.Models;

    public class TaskManagerData : ITaskManagerData
    {

        private readonly IDataContext context;
        private readonly IDictionary<Type, object> repositories;

        public TaskManagerData(IDataContext context)
        {

            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public TaskManagerData()
            : this(new DataContext())
        {

        }

        public IGenericRepository<MyTask> Tasks
        {
            get { return this.GetRepository<MyTask>(); }
        }

        public IGenericRepository<SubTask> SubTasks
        {
            get { return this.GetRepository<SubTask>(); }
        }

        public IGenericRepository<User> Users
        {
            get { return this.GetRepository<User>(); }
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        public void SaveChanges()
        {
            this.context.SaveChanges();

        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.context != null)
                {
                    this.context.Dispose();
                }
            }
        }

        

        private IGenericRepository<T> GetRepository<T>() where T : class
        {
            var typeOfModel = typeof(T);
            if (!this.repositories.ContainsKey(typeOfModel))
            {
                var type = typeof(GenericRepository<T>);

                this.repositories.Add(typeOfModel, Activator.CreateInstance(type, this.context));
            }

            return (IGenericRepository<T>)this.repositories[typeOfModel];
        }


    }
}
