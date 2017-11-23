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
			ArticleViewModel article = null;
			var articleData = articleRepo.GetAll().FirstOrDefault(x => x.Id.Equals(id));
			if(articleData != null)
			{
				article = GetArticle(articleData);
			}

			return article;
		}

		public ArticleViewModel Get(string title)
		{
			ArticleViewModel article = null;
			var articleData = articleRepo.GetAll().FirstOrDefault(x => x.Title.Equals(title, System.StringComparison.InvariantCultureIgnoreCase));
			if (articleData != null)
			{
				article = GetArticle(articleData);
			}

			return article;
		}

		public IEnumerable<ArticleViewModel> GetAll()
		{
			foreach(var item in articleRepo.GetAll())
			{
				yield return item.GetViewModel();
			}
		}

		public ArticleViewModel GetFirstArticle()
		{
			var article = articleRepo.GetAll().FirstOrDefault();
			if (article != null)
			{
				return GetArticle(article);
			}

			return null;
		}

		public ArticlesGlobe GetGlobe()
		{
			return new ArticlesGlobe()
			{
				Articles = GetAll(),
                First = GetArticle(articleRepo.GetAll().FirstOrDefault()) ?? new ArticleViewModel() { Id = -1}
			};
		}

		public ArticleViewModel Insert(ArticleViewModel item)
		{
			var article = item.GetDataModel();
			article = articleRepo.Insert(article);
			return Get(article.Id);
		}

		public ArticleViewModel Update(ArticleViewModel item)
		{
			var article = item.GetDataModel();
			articleRepo.Update(article);
			return Get(article.Id);
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
            ArticleViewModel article = null;
            if (articleData != null)
            {
                article = articleData.GetViewModel();
                article.PagesGlobe = new ArticlePages();
                var firstPage = GetFirstPage(article.Id);
                article.PagesGlobe.FirstPage = firstPage ?? new PageViewModel() { Id = -1 };
                article.PagesGlobe.Pages = GetPages(articleData.Id);
				article.PagesGlobe.Count = article.PagesGlobe.Pages.Count();
				article.PagesGlobe.ArticleId = articleData.Id;
            }
        
            return article;
        }
	}
}
