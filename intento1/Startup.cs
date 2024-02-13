using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(intento1.Startup))]
namespace intento1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
