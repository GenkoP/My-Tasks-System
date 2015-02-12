namespace Tasks.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class SubTask
    {

        [Key]
        public int ID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? DateOnCreate { get; set; }

        public DateTime? DateToEnd { get; set; }

        public PreorityType Type { get; set; }

        public int MyTaskID { get; set; }

        public virtual MyTask MyTask { get; set; }

    }
}
