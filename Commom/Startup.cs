using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Commom.Startup))]
namespace Commom
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
