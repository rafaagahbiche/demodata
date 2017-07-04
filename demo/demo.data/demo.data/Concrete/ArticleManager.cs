using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.data
{
	public class ArticleManager: IManager<ArticleData>
	{
		private IDataContext context;

		public ArticleManager(IDataContext _context)
		{
			this.context = _context;
		}

		public virtual IEnumerable<ArticleData> Articles
		{
			get
			{
				return from article in context.DataXml.Descendants("article")
					   select new ArticleData
					   {
						   Id = Convert.ToInt32(article.Attribute("id").Value),
						   Title = article.Element("title").Value
					   };
			}
		}

		public void Delete(int id)
		{
			throw new NotImplementedException();
		}
		public int Insert(ArticleData item)
		{
			throw new NotImplementedException();
		}

		public void Update(ArticleData item)
		{
			throw new NotImplementedException();
		}
	}
}
