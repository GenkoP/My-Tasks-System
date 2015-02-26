namespace Tasks.WebClient.Models.InputModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using Tasks.Models;
    using Tasks.WebClient.Helpers;


    public class MyTaskInputModel
    {

        public int ID { get; set; }

        [Required]
        [DisplayName("Task title:")]
        [StringLength(30, MinimumLength = 2)]
        public string Title { get; set; }

        [DisplayName("Description:")]
        [StringLength(600)]
        public string Description { get; set; }

        [Required]
        [DisplayName("Date to end:")]
        [MinAndMaxDateTime()]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateToEnd { get; set; }

        [Required]
        [DisplayName("Task priority:")]
        public PriorityType Priority { get; set; }



    }
}