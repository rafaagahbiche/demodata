namespace demo.data
{
	using System;
	using System.Linq;
	using System.Xml.Linq;

	public class ArticleManager: Manager<ArticleData>, IManager<ArticleData>
	//Manager<ArticleData>, IArticleManager
	{
		//private IDataContext context;

		public ArticleManager(IDataContext _context) 
			: base(_context)
		{
			//this.context = _context;
		}

		//private int GetMaxId()
		//{
		//	int maxId = -1;
		//	try
		//	{
		//		maxId = context
		//			.DataXml
		//			.XPathSelectElements("//data/pages/page")
		//			.Max(c => (int)c.Attribute("id"));
		//	}
		//	catch { }

		//	return maxId;
		//}


		public IQueryable<ArticleData> GetAll()
		{
			var dd = context.DataXml.Element("data");

			return (from article in context.DataXml
					.Element("data")
					.Element("articles")
					.Descendants("article")
					select new ArticleData
					{
						Id = article.Attribute("id") != null 
							? Convert.ToInt32(article.Attribute("id").Value) : -1,
						Title = article.Element("title") != null 
							? article.Element("title").Value : string.Empty,
						Description = article.Element("description") != null 
							? article.Element("description").Value : string.Empty
					}).AsQueryable();
		}

		public void Delete(int id)
		{
			context.SaveFile();
		}

		public int Insert(ArticleData item)
		{
			var maxId = GetMaxId();
			var newItem = new XElement("article", 
				new XElement("title", item.Title),
				new XElement("description", item.Description));
			var newId = new XAttribute("id", maxId + 1);
			newItem.Add(newId);
			context.DataXml
				.Element("data")
				.Element("articles").Add(newItem);
			context.SaveFile();
			return maxId;
		}

		public bool Update(ArticleData item)
		{
			bool updateSucceeded = false;
			context.SaveFile();
			return updateSucceeded;
		}
	}
}
