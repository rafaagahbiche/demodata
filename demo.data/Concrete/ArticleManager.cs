namespace demo.data
{
	using System;
	using System.Linq;
	using System.Xml.Linq;

	public class ArticleManager: Manager<ArticleData>, IManager<ArticleData>
	{
		public ArticleManager(IDataContext _context) 
			: base(_context)
		{
		}

		public IQueryable<ArticleData> GetAll()
		{
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
			var articleToDelete = from article in context.DataXml
					.Element("data")
					.Element("articles")
					.Descendants("article")
							   let attr = article.Attribute("id")
							   where attr != null && attr.Value == id.ToString()
							   select article;

			if(articleToDelete != null)
			{
				articleToDelete.First().Remove();
				context.SaveFile();
			}
		}

		public int Insert(ArticleData item)
		{
			var newId = GetMaxId("//data/articles/article") + 1;
			var newItem = new XElement("article", 
				new XElement("title", item.Title),
				new XElement("description", item.Description));
			var newIdAttr = new XAttribute("id", newId);
			newItem.Add(newIdAttr);
			context.DataXml
				.Element("data")
				.Element("articles").Add(newItem);
			context.SaveFile();
			return newId;
		}

		public bool Update(ArticleData item)
		{
			bool updateSucceeded = false;
			try
			{
				var articleToUpdate = from page in context.DataXml
						.Element("data")
						.Element("articles")
						.Descendants("article")
								   let attr = page.Attribute("id")
								   where attr != null && attr.Value == item.Id.ToString()
								   select page;
				if(articleToUpdate != null && articleToUpdate.First() != null)
				{
					var articleElement = articleToUpdate.First();
					if (!string.IsNullOrEmpty(item.Title))
					{
						articleElement.Element("title").Value = item.Title;
					}

					if (!string.IsNullOrEmpty(item.Description))
					{
						if(articleElement.Element("description") != null)
						{
							articleElement.Element("description").Value = item.Description;
						}
						else
						{
							var descElt = new XElement("description");
							descElt.Value = item.Description;
							articleElement.Add(descElt);
						}
					}
				}

				context.SaveFile();
				updateSucceeded = true;
			}
			catch { }

			return updateSucceeded;
		}
	}
}
