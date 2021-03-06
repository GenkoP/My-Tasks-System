﻿namespace Tasks.Models
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

        public class User : IdentityUser
        {

            private ICollection<MyTask> tasks;

            public User()
            {
                this.tasks = new HashSet<MyTask>();
            }

            public virtual ICollection<MyTask> Tasks
            {
                get { return this.tasks;}
                set { this.tasks = value;}
            }

            public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
            {
                // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
                var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
                // Add custom user claims here
                return userIdentity;
            }
        }
    
}
