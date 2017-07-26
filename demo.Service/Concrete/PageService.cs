
namespace demo.Service
{
	using demo.data;
	using demo.Domain;
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class PageService: IPageService
	{
		private readonly IRepo<PageData> repo;

		public PageService(IRepo<PageData> repo)
		{
			this.repo = repo;
		}

		public void Delete(int Id)
		{
			repo.Delete(Id);
		}

		public PageViewModel Get(int id)
		{
			PageViewModel page = null;
			var pageData = repo.GetAll().FirstOrDefault(x => x.Id.Equals(id));
			if(pageData != null)
			{
				page = pageData.GetViewModel();
			}

			return page;
		}

		public IEnumerable<PageViewModel> GetAll()
		{
			throw new NotImplementedException();
		}

		public int Insert(PageViewModel page)
		{
			var pageData = repo.Insert(page.GetDataModel());
			return pageData.Id;
		}

		public bool Update(PageViewModel page)
		{
			return repo.Update(page.GetDataModel());
			throw new NotImplementedException();
		}

		public IEnumerable<PageViewModel> GetPagesByArticleId(int articleId)
		{
			foreach(var item in repo.GetAll().Where(page => page.ArticleId == articleId))
			{
				yield return item.GetViewModel();
			}
		}

		public int PagesCountByArticleId(int articleId)
		{
			var pages = repo.GetAll().Where(page => page.ArticleId == articleId);
			if(pages != null)
			{
				return pages.Count();
			}

			return 0;
		}

		public PageViewModel GetFirstPageByArticleId(int articleId)
		{
			var pageData = repo.GetAll().FirstOrDefault(x => x.ArticleId.Equals(articleId));
			if(pageData != null)
			{
				return pageData.GetViewModel();
			}

			return null;
		}
	}
}
