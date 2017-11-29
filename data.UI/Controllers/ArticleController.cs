

namespace demo.UI.Controllers
{
	using System.Web.Mvc;
	using Service;

	public class ArticleController : Controller
    {
		private readonly IArticleService service;

		public ArticleController(IArticleService service)
		{
			this.service = service;
		}

		[Route("article-viewer/{id}/{title}")]
		public ViewResult Display(int id)
		{
			return View(service.Get(id));
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
				return PartialView("Editor", firstArticle);
			}

			var articleViewModel = new ArticleViewModel()
			{
				Id = -1,
				PagesGlobe = new ArticlePages()
			};

			return PartialView("Editor", articleViewModel);
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

			return PartialView("Editor", articleViewModel);
		}

		[HttpGet]
		public PartialViewResult ShowArticleContent(int articleId)
		{
			var article = service.Get(articleId);
			return PartialView("Editor", article ?? new ArticleViewModel() { Id = -1 });
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