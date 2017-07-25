﻿using System.Collections.Generic;

namespace demo.Service
{
	public interface IArticleService
	{
		IEnumerable<ArticleViewModel> GetAll();
		ArticleViewModel Get(int id);
		int Insert(ArticleViewModel item);
		void Delete(int Id);
		bool Update(ArticleViewModel item);
		ArticlesGlobe GetGlobe();
		IEnumerable<PageViewModel> GetPages(int articleId);
		PageViewModel GetFirstPage(int articleId);
	}
}
