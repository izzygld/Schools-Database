using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HoneyMustard.Web.Startup))]
namespace HoneyMustard.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
