namespace demo.data
{
	using System;
	using System.Linq;
	using System.Xml.Linq;

	public class PageManager : Manager<PageData>, IManager<PageData>
	{
		public PageManager(IDataContext _context) 
			: base(_context)
		{
		}

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
			var newId = GetMaxId("//data/pages/page") + 1;
			var newItem = new XElement("page",
				new XElement("content", item.Content),
				new XElement("articleId", item.ArticleId));
			var newIdAttr = new XAttribute("id", newId);
			newItem.Add(newIdAttr);
			context.DataXml
				.Element("data")
				.Element("pages").Add(newItem);

			context.SaveFile();
			return newId;
		}

		public void Delete(int id)
		{
			var pageToDelete = from page in context.DataXml
					.Element("data")
					.Element("pages")
					.Descendants("page")
					let attr = page.Attribute("id")
					where attr != null && attr.Value == id.ToString()
					select page;

			if(pageToDelete != null)
			{
				pageToDelete.First().Remove();
				context.SaveFile();
			}
		}

		public bool Update(PageData item)
		{
			bool updateSucceeded = false;
			try
			{
				var pageToUpdate = from page in context.DataXml
						.Element("data")
						.Element("pages")
						.Descendants("page")
							let attr = page.Attribute("id")
							where attr != null && attr.Value == item.Id.ToString()
							select page;
				pageToUpdate.First().Element("content").Value = item.Content;
				context.SaveFile();
				updateSucceeded = true;
			}
			catch { }

			return updateSucceeded;
		}
	}
}
