namespace demo.data
{
	using System.Web;
	using System.Xml.Linq;

	public class DataContext: IDataContext
	{
		private XDocument _dataXml;
		
		private string path;

		public DataContext(string path)
		{
			this.path = path;
		}

		public void SaveFile()
		{
			_dataXml.Save(path);
		}


		public XDocument DataXml
		{
			get
			{
				if (_dataXml == null)
				{
					var newSession = HttpContext.Current.Session.IsNewSession;
					var path1 = HttpContext.Current.Request.MapPath(path);
					_dataXml = XDocument.Load(path1);
				}

				return _dataXml;
			}
		}
	}
}
