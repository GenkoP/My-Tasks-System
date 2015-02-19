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
                    Subtasks = task.SubTasks.AsQueryable().Select(SubTasksViewModel.GetSubtasts),
                };

            }
        }

        public int ID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime DateToEnd { get; set; }

        public PriorityType Priority { get; set; }

        public IQueryable<SubTasksViewModel> Subtasks { get; set; } 

    }
}