using System.Web.Mvc;
using System.Web.Routing;

namespace demo.UI
{
	public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
		}

		protected void Session_Start()
		{
			System.Random rnd = new System.Random();
			Session["new_session"] = rnd.Next();
		}
	}
}
