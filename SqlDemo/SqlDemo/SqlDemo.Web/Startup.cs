using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SqlDemo.Web.Startup))]
namespace SqlDemo.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
