using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Projekt_Dejtingsida.Startup))]
namespace Projekt_Dejtingsida
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
