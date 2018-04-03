using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProyectoED1.Startup))]
namespace ProyectoED1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
