using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ShoppingGo.Startup))]
namespace ShoppingGo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
