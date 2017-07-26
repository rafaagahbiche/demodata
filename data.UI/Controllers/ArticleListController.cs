using demo.Service;
using System.Web.Mvc;


namespace demo.UI.Controllers
{
	public class ArticleListController : Controller
    {
		private readonly IArticleService service;

		public ArticleListController(IArticleService service)
		{
			this.service = service;
		}

		// GET: ArticleList
		public ActionResult Index()
        {
            return View(service.GetGlobe());
        }
    }
}