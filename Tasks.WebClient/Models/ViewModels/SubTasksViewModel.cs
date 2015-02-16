namespace Tasks.WebClient.Models.ViewModels.MyTasks
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
                    Description = subTask.Description,
                    DateToEnd = subTask.DateToEnd,
                    Type = subTask.Type,

                };
            }
        }

     
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? DateToEnd { get; set; }

        public PriorityType Type { get; set; }

    }
}