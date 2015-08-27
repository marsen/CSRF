using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(attack.Startup))]
namespace attack
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
