using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VyatkinSchool.Startup))]
namespace VyatkinSchool
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}