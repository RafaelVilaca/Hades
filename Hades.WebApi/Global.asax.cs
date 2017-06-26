using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using SimpleInjector.Integration.WebApi;

namespace Hades.WebApi
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            
            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(SimpleInjectorApiContainer.RegisterContainer());
        }
    }
}
