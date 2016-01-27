using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ElasticSearch.Startup))]
namespace ElasticSearch
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
