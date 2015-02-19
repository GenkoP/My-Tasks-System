namespace Tasks.WebClient.Models.InputModels
{

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    using Tasks.Models;

    public class SubTaskInputModel
    {

        [Required]
        [StringLength(30, MinimumLength = 2)]
        [DisplayName(" Subtask title:")]
        public string SubtaskTitle { get; set; }


        [DisplayName("Subtask priority:")]
        public PriorityType SubtaskPriority { get; set; }


    }
}