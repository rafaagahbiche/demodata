namespace demo.data
{
	using System;
	using System.Linq;
	using System.Xml.Linq;
	using System.Xml.XPath;

	public class PageManager : Manager<PageData>, IManager<PageData>
	{
		public PageManager(IDataContext _context) 
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

		public IQueryable<PageData> GetAll()
		{
			return (from page in context.DataXml
					.Element("data")
					.Element("pages")
					.Descendants("page")
					select new PageData
					{
						Id = Convert.ToInt32(page.Attribute("id").Value),
						Content = page.Element("content").Value,
						ArticleId = Convert.ToInt32(page.Element("articleId").Value)
					}).AsQueryable();
		}

		public int Insert(PageData item)
		{
			var maxId = GetMaxId();
			var newItem = new XElement("page",
				new XElement("content", item.Content),
				new XElement("articleId", item.ArticleId));
			var newId = new XAttribute("id", maxId + 1);
			newItem.Add(newId);
			context.DataXml
				.Element("data")
				.Element("pages").Add(newItem);

			context.SaveFile();
			return maxId;
		}

		public void Delete(int id)
		{
			context.SaveFile();
			throw new NotImplementedException();
		}

		public bool Update(PageData item)
		{
			bool updateSucceeded = false;
			context.SaveFile();
			return updateSucceeded;
		}
	}
}
