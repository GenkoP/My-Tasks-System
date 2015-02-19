namespace Tasks.Models
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class SubTask
    {

        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(30 , MinimumLength= 2)]
        public string Title { get; set; }

        [Required]
        [DefaultValue(1)]
        public PriorityType Priority { get; set; }

        [DefaultValue(false)]
        public bool IsCompleted { get; set; }

        [Required]
        public int MyTaskID { get; set; }

        public virtual MyTask MyTask { get; set; }

    }
}
