
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

		// GET: Page
		public ActionResult Index()
        {
            return View();
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
			return PartialView("PageTab", 
				new PageViewModel() {
					Id = -1,
					ArticleId = articleId
				});
		}

		[HttpPost]
		public PartialViewResult SavePage(PageViewModel pageViewModel)
		{
			if (pageViewModel.Id == 0)
			{
				return PartialView("EditInfos", 
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

			return PartialView("EditInfos", pageViewModel);
		}

		[HttpGet]
		public PartialViewResult DeletePage(int pageId, int articleId)
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