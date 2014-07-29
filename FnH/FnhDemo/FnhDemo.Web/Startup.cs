using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FnhDemo.Web.Startup))]
namespace FnhDemo.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
