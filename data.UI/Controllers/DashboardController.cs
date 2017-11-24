using demo.Service;
using System.Web.Mvc;


namespace demo.UI.Controllers
{
	public class DashboardController : Controller
    {
		private readonly IArticleService service;

		public DashboardController(IArticleService service)
		{
			this.service = service;
		}

		[Route("")]
		[Route("article-editor")]
		public ActionResult Index()
        {
            return View(service.GetGlobe());
        }
    }
}