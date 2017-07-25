namespace demo.Service
{
	using demo.data;
	using demo.Domain;
	using System.Collections.Generic;
	using System.Linq;

	public class ArticleService: IArticleService
	{
		private readonly IRepo<ArticleData> articleRepo;
		private readonly IRepo<PageData> pageRepo;

		public ArticleService(IRepo<ArticleData> articleRepo, IRepo<PageData> pageRepo)
		{
			this.articleRepo = articleRepo;
			this.pageRepo = pageRepo;
		}

		public void Delete(int Id)
		{
			articleRepo.Delete(Id);
		}

		public ArticleViewModel Get(int id)
		{
			var articleData = articleRepo.GetAll().FirstOrDefault(x => x.Id.Equals(id));
			return GetArticle(articleData);
		}

		public IEnumerable<ArticleViewModel> GetAll()
		{
			foreach(var item in articleRepo.GetAll())
			{
				yield return item.GetViewModel();
			}
		}

		public ArticlesGlobe GetGlobe()
		{
			return new ArticlesGlobe()
			{
				Articles = GetAll(),
				First = GetArticle(articleRepo.GetAll().FirstOrDefault())
			};
		}

		public int Insert(ArticleViewModel item)
		{
			var article = item.GetDataModel();
			return articleRepo.Insert(article).Id;
		}

		public bool Update(ArticleViewModel item)
		{
			var article = item.GetDataModel();
			return articleRepo.Update(article);
		}

		public IEnumerable<PageViewModel> GetPages(int articleId)
		{
			var pages = pageRepo.GetAll().Where(x => x.ArticleId.Equals(articleId));
			foreach(var page in pages)
			{
				yield return page.GetViewModel();
			}
		}

		public PageViewModel GetFirstPage(int articleId)
		{
			PageData page = null;
			try
			{
				page = pageRepo.GetAll().First(x => x.ArticleId.Equals(articleId));
				if (page != null)
				{
					return page.GetViewModel();
				}
			}
			catch  { }

			return null;
		}

		private ArticleViewModel GetArticle(ArticleData articleData)
		{
			var article = articleData.GetViewModel();
			article.PagesGlobe = new ArticlePages();
			article.PagesGlobe.FirstPage = GetFirstPage(articleData.Id);
			article.PagesGlobe.Pages = GetPages(articleData.Id);
			article.PagesGlobe.ArticleId = articleData.Id;
			return article;
		}
	}
}
