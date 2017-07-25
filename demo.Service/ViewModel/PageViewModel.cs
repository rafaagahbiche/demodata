	
namespace demo.Service
{
	using System.ComponentModel.DataAnnotations;
	using System.Web;
	using System.Web.Mvc;

	public class PageViewModel: ItemViewModel
	{
		[UIHint("tinymce_jquery_full"), AllowHtml]
		public string Content { get; set; }

		[UIHint("tinymce_jquery_full"), AllowHtml]
		public string DecodedContent
		{
			get { return HttpUtility.HtmlDecode(Content); }
			set { Content = HttpUtility.HtmlEncode(value); }
		}

		public int ArticleId { get; set; }
	}
}
