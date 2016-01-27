using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(XSSGet.Startup))]
namespace XSSGet
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
