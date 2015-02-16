namespace Tasks.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class SubTask
    {

        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(30 , MinimumLength= 2)]
        public string Title { get; set; }

        [StringLength(600)]
        public string Description { get; set; }

        public DateTime? DateOnCreate { get; set; }

        public DateTime? DateToEnd { get; set; }

        public PriorityType Type { get; set; }

        [Required]
        public int MyTaskID { get; set; }

        public virtual MyTask MyTask { get; set; }

    }
}
