using System.Collections;
using System.Collections.Generic;

namespace demo.Service
{
	public class ArticleViewModel: ItemViewModel
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public virtual List<PageViewModel> Pages { get; set; }
	}
}
