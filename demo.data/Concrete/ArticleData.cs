
namespace demo.data
{
	using System.Collections.Generic;
	public class ArticleData: Item
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public virtual IEnumerable<PageData> Pages { get; set; }
	}
}
