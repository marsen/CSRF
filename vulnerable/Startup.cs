using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(vulnerable.Startup))]
namespace vulnerable
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
