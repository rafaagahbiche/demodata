using System;
using System.Linq;
using System.Web.Mvc;
using demo.Service;

namespace demo.UI.Controllers
{
	public class ArticleController : Controller
    {
		private readonly IArticleService service;

		public ArticleController(IArticleService service)
		{
			this.service = service;
		}

		public ActionResult Details()
		{
			return View("Error");
		}

		//[Route("pages/{title}-{id}")]
		public ActionResult Details(int id)
		{
			try
			{
				var article = this.service.Get(id);
				if (article != null)
				{
					return View(article);
				}

				return View();
			}
			catch (Exception ex)
			{
				//Kaliko.Logger.Write(ex, Logger.Severity.Critical);
			}

			return View();
		}

		[HttpPost, ValidateInput(false)]
		public ActionResult Create(ArticleViewModel articleViewModel)
		{
			try
			{
				if (articleViewModel != null)
				{
					articleViewModel.Id = this.service.Insert(articleViewModel);
					return RedirectToAction("Edit", new { id = articleViewModel.Id });
				}

				return View("Create");
			}
			catch
			{
				return View();
			}
		}

		[HttpPost]
		public PartialViewResult SaveArticle(ArticleViewModel articleViewModel)
		{
			if (articleViewModel.Id == -1)
			{
				articleViewModel.Id = service.Insert(articleViewModel);
			}
			else
			{
				service.Update(articleViewModel);
			}

			return PartialView("Edit", articleViewModel);
		}

		//[Route("pages/edit/{id}")]
		[HttpGet]
		//public PartialViewResult EditAction(int id)
		public PartialViewResult EditAction(ArticleViewModel article)
		{
			//var article = service.Get(id);
			if (article != null)
			{
				article.PagesGlobe = new ArticlePages()
				{
					ArticleId = article.Id,
					FirstPage = service.GetFirstPage(article.Id),
					Pages = service.GetPages(article.Id)
				};

				return PartialView("Edit", article);
			}

			return PartialView("Edit", new ArticleViewModel() { Id = -1, PagesGlobe = new ArticlePages() });
		}

		[HttpPost, ValidateInput(false)]
		[Route("pages/edit/{id}")]
		public ActionResult Edit(ArticleViewModel articleViewModel)
		{
			try
			{
				bool succeeded = service.Update(articleViewModel);
				if (succeeded)
				{
					return View(articleViewModel);
				}
				else
				{
					return View("Error");
				}
			}
			catch (Exception ex)
			{
				return View();
			}
		}
	}
}