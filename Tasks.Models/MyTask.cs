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

        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Title { get; set; }

        [StringLength(600)]
        public string Description { get; set; }

        public DateTime DateOnCreate { get; set; }

        public DateTime? DateToEnd { get; set; }

        [DefaultValue(2)]
        public PreorityType Preority { get; set; }

        [Required]
        public string UserID { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<SubTask> SubTasks
        {
            get { return this.subTasks; }
            set { this.subTasks = value; }
        }

    }
}
