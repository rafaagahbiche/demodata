﻿using System.Web.Mvc;
using System.Web.Routing;

namespace data.UI
{
	public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
		}

		protected void Session_Start()
		{
			System.Random rnd = new System.Random();
			Session["new_session"] = rnd.Next();
		}
	}
}
