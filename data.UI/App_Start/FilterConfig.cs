namespace demo.UI
{
	using Infra;
	using System.Web.Mvc;

	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new DemoHandleErrorAttribute());
		}
	}
}