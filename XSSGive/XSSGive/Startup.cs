using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(XSSGive.Startup))]
namespace XSSGive
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
