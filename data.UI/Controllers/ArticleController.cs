

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

		[HttpPost]
		public PartialViewResult Delete(int id)
		{
			var articleViewModel = new ArticleViewModel()
			{
				Id = -1,
				PagesGlobe = new ArticlePages()
			};

			if (id != -1)
			{
				service.Delete(id);
			}

			var firstArticle = service.GetFirstArticle();
			if (firstArticle != null)
			{
				return PartialView("EditInfos", firstArticle);
			}

			return PartialView("EditInfos", articleViewModel);
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

			return PartialView("EditInfos", articleViewModel);
		}

		[HttpGet]
		public PartialViewResult ShowArticleContent(int articleId)
		{
			var article = service.Get(articleId);
			return PartialView("EditInfos", article ?? new ArticleViewModel() { Id = -1 });
		}

		[HttpGet]
		public PartialViewResult AddNewTab()
		{
			return PartialView("ArticleTab",
				new ArticleViewModel()
				{
					Id = -1
				});
		}
	}
}