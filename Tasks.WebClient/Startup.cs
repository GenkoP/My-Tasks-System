using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Tasks.WebClient.Startup))]
namespace Tasks.WebClient
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
