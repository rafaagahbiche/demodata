using System.Collections.Generic;

namespace demo.Service
{
	public interface IArticleService
	{
		IEnumerable<ArticleViewModel> GetAll();
		ArticleViewModel Get(int id);
		ArticleViewModel Get(string title);
		ArticleViewModel Insert(ArticleViewModel item);
		void Delete(int Id);
		ArticleViewModel Update(ArticleViewModel item);
		ArticlesGlobe GetGlobe();
		IEnumerable<PageViewModel> GetPages(int articleId);
		PageViewModel GetFirstPage(int articleId);
		ArticleViewModel GetFirstArticle();
	}
}
