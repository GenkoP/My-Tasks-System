namespace Tasks.WebClient.Models.ViewModels
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Tasks.Models;

    public class MyTaskViewModelHomeIndex
    {
        
        public static Expression<Func<MyTask, MyTaskViewModelHomeIndex>> GetTasks
        {
            get
            {
                return task => new MyTaskViewModelHomeIndex
                {
                    ID = task.ID,
                    Title = task.Title,
                    Description = task.Description,
                    DateToEnd = task.DateToEnd,
                    Preority = task.Preority,
                };

            }
        }

        public int ID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? DateToEnd { get; set; }

        public PreorityType Preority { get; set; }

       

    }
}