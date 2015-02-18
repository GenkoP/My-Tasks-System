namespace Tasks.WebClient.Models.ViewModels
{
    using System;
    using System.Linq.Expressions;

    using Tasks.Models;


    public class SubTasksViewModel
    {
        
        public static Expression<Func<SubTask , SubTasksViewModel>> GetSubtasts
        {
            get
            {
                return subTask => new SubTasksViewModel
                {
                    Title = subTask.Title,
                    Priority = subTask.Priority,

                };
            }
        }

     
        public string Title { get; set; }


        public PriorityType Priority { get; set; }

    }
}