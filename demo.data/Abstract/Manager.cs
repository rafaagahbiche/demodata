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

		protected int GetMaxId(string xpath)
		{
			int maxId = -1;
			try
			{
				maxId = context
					.DataXml
					.XPathSelectElements(xpath)
					.Max(c => (int)c.Attribute("id"));
			}
			catch { }

			return maxId;
		}
	}
}
