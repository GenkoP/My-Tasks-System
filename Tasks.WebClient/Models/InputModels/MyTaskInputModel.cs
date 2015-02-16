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

        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "dd/MM/yyyy")]
        public DateTime DateToEnd { get; set; }

       public PriorityType Priority { get; set; }

    }
}