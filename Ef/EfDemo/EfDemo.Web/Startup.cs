using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EfDemo.Web.Startup))]
namespace EfDemo.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
