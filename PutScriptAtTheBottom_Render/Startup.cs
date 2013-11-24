using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PutScriptAtTheBottom_Render.Startup))]
namespace PutScriptAtTheBottom_Render
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
