using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MCVShop.Startup))]
namespace MCVShop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
