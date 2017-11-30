namespace demo.Infra
{
	using Kaliko;
	using System;
	using System.Web.Mvc;

	public class DemoHandleErrorAttribute: HandleErrorAttribute
	{
		public override void OnException(ExceptionContext filterContext)
		{
			Logger.Write(filterContext.Exception, Logger.Severity.Critical);
		}

		public void cc()
		{
			try
			{

			}
			catch (Exception ex)
			{
				Logger.Write(ex, Logger.Severity.Critical);
			}
		}
	}
}
