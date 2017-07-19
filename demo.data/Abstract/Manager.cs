namespace demo.data
{
	using System.Linq;
	using System.Xml.XPath;

	public class Manager<IEntity>
		where IEntity : ItemData
	{
		protected IDataContext context;

		public Manager(IDataContext context)
		{
			this.context = context;
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

		//public virtual void Delete(int id) { }

		//public virtual IQueryable<IEntity> GetAll()
		//{
		//	throw new NotImplementedException();
		//}

		//public virtual int Insert(IEntity item)
		//{
		//	throw new NotImplementedException();
		//}

		//public virtual bool Update(IEntity item)
		//{
		//	throw new NotImplementedException();
		//}

	}
}
