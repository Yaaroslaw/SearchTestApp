using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SearchTestApp.Startup))]
namespace SearchTestApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
