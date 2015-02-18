namespace Tasks.WebClient.Models.InputModels
{

    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web;
    using Tasks.Models;

    public class SubTaskInputModel
    {

        [Required]
        [StringLength(30 , MinimumLength = 2)]
        [DisplayName("Title:")]
        public string Title { get; set; }
        
        [DisplayName("Description:")]
        [StringLength(600)]
        public string Description { get; set; }
        
        [DisplayName("Task priority:")]
        public PriorityType Priority { get; set; }


    }
}