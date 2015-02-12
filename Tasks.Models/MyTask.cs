namespace Tasks.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class MyTask
    {
        private ICollection<SubTask> subTasks;

        public MyTask()
        {
            this.subTasks = new HashSet<SubTask>();
        }


        [Key]
        public int ID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? DateOnCreate { get; set; }

        public DateTime? DateToEnd { get; set; }

        [DefaultValue(PreorityType.Medium)]
        public PreorityType Preority { get; set; }

        public string UserID { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<SubTask> SubTasks
        {
            get { return this.subTasks; }
            set { this.subTasks = value; }
        }

    }
}
