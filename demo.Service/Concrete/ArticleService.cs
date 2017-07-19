namespace demo.Service
{
	using demo.data;
	using demo.Domain;
	using System.Collections.Generic;
	using System.Linq;
	using System;

	public class ArticleService: IArticleService
	//IService<ArticleViewModel>
	{
		private readonly IRepo<ArticleData> repo;

		//public ArticleService()
		//{
		//	repo = new Repo<ArticleData>(
		//		new ArticleManagerFactory(
		//			new ArticleManager(
		//				new DataContext(
		//					System.Web.HttpContext.Current.
		//					Server.MapPath(@"/App_Data/data.xml")))));
		//}

		public ArticleService(IRepo<ArticleData> repo)
		{
			this.repo = repo;
		}

		public void Delete(int Id)
		{
			repo.Delete(Id);
		}

		public ArticleViewModel Get(int id)
		{
			var articleData = repo.GetAll().FirstOrDefault(x => x.Id.Equals(id));
			return articleData.GetViewModel();
		}

		public IEnumerable<ArticleViewModel> GetAll()
		{
			foreach(var item in repo.GetAll())
			{
				yield return item.GetViewModel();
			}
		}

		public int Insert(ArticleViewModel item)
		{
			var article = item.GetDataModel();
			return repo.Insert(article).Id;
		}

		public bool Update(ArticleViewModel item)
		{
			var article = item.GetDataModel();
			return repo.Update(article);
		}

		//public IEnumerable<PageViewModel> GetPages(int articleId)
		//{
		//	var article = repo.GetAll().First<ArticleData>(x => x.Id.Equals(articleId));
		//}
	}
}
