﻿namespace Tasks.WebClient.Infrastructure.Providers
{
    using System.Threading;

    using Microsoft.AspNet.Identity;

    public class CurrentUserIdProvider : ICurrentUserIdProvider
    {
        public string GetUserId()
        {
            return Thread.CurrentPrincipal.Identity.GetUserId();
        }
    }
}