using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Matthewpbaileydesigns.WebUI.Startup))]
namespace Matthewpbaileydesigns.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
