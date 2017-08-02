using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace demo.Service
{
	public class ArticleViewModel: ItemViewModel
	{
		[Required]
		public string Title { get; set; }
		public string Description { get; set; }
		public ArticlePages PagesGlobe { get; set; }
	}
}
