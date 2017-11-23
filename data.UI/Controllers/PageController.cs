
namespace demo.UI.Controllers
{
	using System.Web.Mvc;
	using demo.Service;

	public class PageController : Controller
    {
		private IPageService service;

		public PageController(IPageService service)
		{
			this.service = service;
		}

		[HttpGet]
		public PartialViewResult ShowPageContent(int pageId, int articleId)
		{
			var pageViewModel = service.Get(pageId);
			if (pageViewModel != null)
			{
				return PartialView("Edit", pageViewModel);
			}

			return PartialView("Edit", 
				new PageViewModel() {
					Id = -1,
					ArticleId = articleId
				});
		}

		[HttpGet]
		public PartialViewResult AddNewTab(int articleId)
		{
			return PartialView("Tab", 
				new PageViewModel() {
					Id = -1,
					ArticleId = articleId
				});
		}

		[HttpPost]
		public PartialViewResult Save(PageViewModel pageViewModel)
		{
			if (pageViewModel.Id == 0)
			{
                return PartialView("Content", 
					new PageViewModel() {
						Id = -1,
						ArticleId = -1
					});
			}

			// Old article + New Page
			if (pageViewModel.Id == -1)
			{
				pageViewModel.Id = service.Insert(pageViewModel);
			}
			else
			{
				service.Update(pageViewModel);
			}

            return PartialView("Content", pageViewModel);
		}

		[HttpGet]
		public PartialViewResult Delete(int pageId, int articleId)
		{
			if (pageId > 0)
			{
				service.Delete(pageId);
			}

			if (service.PagesCountByArticleId(articleId) > 0)
			{
				var firstPageViewModel = service.GetFirstPageByArticleId(articleId);
				if (firstPageViewModel != null)
				{
					return PartialView("Edit", firstPageViewModel);
				}
			}

			return PartialView("Edit", new PageViewModel() { Id = -1, ArticleId = articleId });
		}
	}
}