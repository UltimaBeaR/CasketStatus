using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CasketStatusWebSite.Startup))]
namespace CasketStatusWebSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
