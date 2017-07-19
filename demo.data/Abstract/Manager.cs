using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;

namespace demo.data
{
	public class Manager<IEntity> : IManager<IEntity> 
		where IEntity : ItemData
	{
		protected IDataContext context;

		public Manager(IDataContext _context)
		{
			this.context = _context;
		}


		protected int GetMaxId()
		{
			int maxId = -1;
			try
			{
				maxId = context
					.DataXml
					.XPathSelectElements("//data/pages/page")
					.Max(c => (int)c.Attribute("id"));
			}
			catch { }

			return maxId;
		}

		public virtual void Delete(int id) { }

		public virtual IQueryable<IEntity> GetAll()
		{
			throw new NotImplementedException();
		}

		public virtual int Insert(IEntity item)
		{
			throw new NotImplementedException();
		}

		public virtual bool Update(IEntity item)
		{
			throw new NotImplementedException();
		}

	}
}
