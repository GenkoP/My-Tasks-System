namespace Tasks.WebClient.Helpers
{

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;


    public class MinAndMaxDateTimeAttribute : RangeAttribute
    {
        
        private const string MAX_DATE = "01/01/2099";

        public MinAndMaxDateTimeAttribute()
            : base(typeof(DateTime), DateTime.Now.ToShortDateString(), DateTime.Parse(MAX_DATE).ToShortDateString())
        {

        }


    }
}