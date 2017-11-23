

namespace demo.UI.Controllers
{
	using System.Web.Mvc;
	using demo.Service;

	public class ArticleController : Controller
    {
		private readonly IArticleService service;

		public ArticleController(IArticleService service)
		{
			this.service = service;
		}

		[Route("article-viewer/{title}")]
		public ViewResult Display(string title)
		{
			return View(service.Get(title));
		}

		[HttpPost]
		public PartialViewResult Delete(int id)
		{
			if (id != -1)
			{
				service.Delete(id);
			}

			var firstArticle = service.GetFirstArticle();
			if (firstArticle != null)
			{
				return PartialView("Edit", firstArticle);
			}

			var articleViewModel = new ArticleViewModel()
			{
				Id = -1,
				PagesGlobe = new ArticlePages()
			};

			return PartialView("Edit", articleViewModel);
		}

		[HttpPost]
		public PartialViewResult Save(ArticleViewModel articleViewModel)
		{
			if (articleViewModel.Id == -1)
			{
				articleViewModel = service.Insert(articleViewModel);
			}
			else
			{
				articleViewModel = service.Update(articleViewModel);
			}

			// TODO handle case when article is null after update/insert

			return PartialView("Edit", articleViewModel);
		}

		[HttpGet]
		public PartialViewResult ShowArticleContent(int articleId)
		{
			var article = service.Get(articleId);
			return PartialView("Edit", article ?? new ArticleViewModel() { Id = -1 });
		}

		[HttpGet]
		public PartialViewResult AddNewTab()
		{
			return PartialView("Tab",
				new ArticleViewModel()
				{
					Id = -1
				});
		}
	}
}