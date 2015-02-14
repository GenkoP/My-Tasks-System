namespace Tasks.WebClient.Models.InputModels
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using Tasks.Models;


    public class MyTaskInputModel
    {
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Title { get; set; }

        [StringLength(600)]
        public string Description { get; set; }

        public DateTime DateOnCreate { get; set; }

        public DateTime? DateToEnd { get; set; }

       public PreorityType Preority { get; set; }

        public string UserID { get; set; }
    }
}