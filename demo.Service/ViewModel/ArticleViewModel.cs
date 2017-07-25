using System.Collections;
using System.Collections.Generic;

namespace demo.Service
{
	public class ArticleViewModel: ItemViewModel
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public virtual IEnumerable<PageViewModel> Pages { get; set; }
		public PageViewModel FirstPage { get; set; }
		public ArticlePages PagesGlobe { get; set; }

	}
}
