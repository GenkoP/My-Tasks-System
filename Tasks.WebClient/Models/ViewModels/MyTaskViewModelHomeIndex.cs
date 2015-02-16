namespace Tasks.WebClient.Models.ViewModels
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Tasks.Models;

    public class MyTaskViewModel
    {
        
        public static Expression<Func<MyTask, MyTaskViewModel>> GetTasks
        {
            get
            {
                return task => new MyTaskViewModel
                {
                    ID = task.ID,
                    Title = task.Title,
                    Description = task.Description,
                    DateToEnd = task.DateToEnd,
                    Priority = task.Priority,
                };

            }
        }

        public int ID { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Title { get; set; }

        [StringLength(600)]
        public string Description { get; set; }

        public DateTime DateToEnd { get; set; }

        public PriorityType Priority { get; set; }

       

    }
}