
namespace demo.UI.Controllers
{
	using System.Web.Mvc;
	using demo.Service;

	public class PageController : Controller
    {
		private IService<PageViewModel> service;

		public PageController(IService<PageViewModel> service)
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
			var pageViewModel = this.service.Get(pageId);
			if (pageViewModel != null)
			{
				return PartialView("CreateArticlePage", pageViewModel);
			}

			return PartialView("CreateArticlePage", 
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
			if (pageViewModel == null || pageViewModel.Id == 0)
			{
				return PartialView("EditPageInfos", 
					new PageViewModel() {
						Id = -1,
						ArticleId = -1
					});
			}

			// Old article + New Page
			if (pageViewModel.Id == -1)
			{
				pageViewModel.Id = this.service.Insert(pageViewModel);
				if (pageViewModel.Id == -1)
				{
					return PartialView("EditPageInfos", 
						new PageViewModel() {
							Id = -1,
							ArticleId = -1
						});
				}
				else
				{
					return PartialView("EditPageInfos", pageViewModel);
				}
			}
			else
			{
				var updateSucceeded = this.service.Update(pageViewModel);
				return PartialView("EditPageInfos", pageViewModel);
			}
		}
	}
}