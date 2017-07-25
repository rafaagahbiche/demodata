using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using demo.Service;


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