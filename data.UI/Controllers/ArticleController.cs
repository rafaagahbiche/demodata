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

		//[Route("pages/edit/{id}")]
		[HttpGet]
		public PartialViewResult EditAction(int id)
		{
			if (!id.Equals(0) && !id.Equals(-1))
			{
				var article = service.Get(id); 
				if (article != null)
				{
					//article.Pages = service.GetPages(id);
					//article.FirstPage = service.GetFirstPage(id);
					article.PagesGlobe = new ArticlePages()
					{
						ArticleId = id,
						FirstPage = service.GetFirstPage(id),
						Pages = service.GetPages(id)
					};

					return PartialView("Edit", article);
				}
				else
				{
					//Logger.Write("Article is null");
					return PartialView("Error");
				}
			}

			return PartialView();
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
				//Logger.Write(ex, Logger.Severity.Major);
				return View();
			}
		}
	}
}