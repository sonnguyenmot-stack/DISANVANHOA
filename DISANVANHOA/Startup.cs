using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DISANVANHOA.Startup))]
namespace DISANVANHOA
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
        }
        
    }
}
