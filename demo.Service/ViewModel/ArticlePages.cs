using System.Collections.Generic;

namespace demo.Service
{
	public class ArticlePages
	{
		public IEnumerable<PageViewModel> Pages { get; set; }

		public PageViewModel FirstPage { get; set; }

		public int ArticleId { get; set; }
	}
}
