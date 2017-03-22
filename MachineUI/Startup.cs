using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MachineUI.Startup))]
namespace MachineUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
