using System.Collections.Generic;

namespace demo.Service
{
	public class ArticlesGlobe
	{
		public IEnumerable<ArticleViewModel> Articles { get; set; }
		public ArticleViewModel First { get; set; }
	}
}
