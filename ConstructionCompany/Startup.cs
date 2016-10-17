using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ConstructionCompany.Startup))]
namespace ConstructionCompany
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
