using demo.data;

namespace demo.Service
{
	public static class ExtensionHelper
	{
		public static ArticleData GetDataModel(this ArticleViewModel articleViewModel)
		{
			var articleData = new ArticleData()
			{
				Description = articleViewModel.Description,
				Id = articleViewModel.Id,
				Title = articleViewModel.Title
			};

			return articleData;
		}

		public static ArticleViewModel GetViewModel(this ArticleData article)
		{
			var articleViewModel = new ArticleViewModel();
			if (article != null)
			{
				articleViewModel.Id = article.Id;
				articleViewModel.Title = article.Title;
				articleViewModel.Description = article.Description;
			}

			return articleViewModel;
		}

		public static PageData GetDataModel(this PageViewModel pageViewModel)
		{
			var pageData = new PageData()
			{
				Id = pageViewModel.Id,
				Content = pageViewModel.Content,
				ArticleId = pageViewModel.ArticleId
			};

			return pageData;
		}

		public static PageViewModel GetViewModel(this PageData articlePage)
		{
			PageViewModel articlePageViewModel = null;
			if (articlePage != null)
			{
				articlePageViewModel = new PageViewModel()
				{
					Id = articlePage.Id,
					Content = articlePage.Content,
					ArticleId = articlePage.ArticleId
				};
			}

			return articlePageViewModel;
		}
	}
}
