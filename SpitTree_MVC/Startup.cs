using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SpitTree_MVC.Startup))]
namespace SpitTree_MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
