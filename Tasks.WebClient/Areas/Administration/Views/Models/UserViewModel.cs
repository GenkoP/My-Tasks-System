namespace Tasks.WebClient.Areas.Administration.Views.Admins
{
    using System;
    using System.Linq.Expressions;

    using Tasks.Models;

    public class UserViewModel
    {
        public static Expression<Func<User, UserViewModel>> GetUsers
        {
            get
            {
                return user => new UserViewModel
                {
                    Id = user.Id,
                    Email = user.Email,

                };
            }
        }


        public string Id { get; set; }

        public string Email { get; set; }

    }
}