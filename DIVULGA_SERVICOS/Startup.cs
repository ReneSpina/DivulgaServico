using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DIVULGA_SERVICOS.Startup))]
namespace DIVULGA_SERVICOS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
