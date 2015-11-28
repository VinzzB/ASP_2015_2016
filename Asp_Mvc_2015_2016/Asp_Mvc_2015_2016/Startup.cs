using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Asp_Mvc_2015_2016.Startup))]
namespace Asp_Mvc_2015_2016
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
