namespace demo.data
{
	using System.Web;
	using System.Xml.Linq;

	public class DataContext: IDataContext
	{
		private XDocument _dataXml;

		private string originalPath;
		private string userPath;

		public DataContext(string originalDataPath, string userDataPath)
		{
			this.originalPath = HttpContext.Current.Request.MapPath(originalDataPath);
			this.userPath = HttpContext.Current.Request.MapPath(userDataPath);
		}

		public DataContext(string originalDataPath)
		{
			this.userPath = HttpContext.Current.Request.MapPath(originalDataPath);
		}

		public void SaveFile()
		{
			_dataXml.Save(userPath);
		}

		private void CopyData()
		{
			_dataXml = XDocument.Load(originalPath);
			var newCopy = new XDocument(_dataXml);
			newCopy.Save(userPath);
		}

		public XDocument DataXml
		{
			get
			{
				if (_dataXml == null)
				{
                    var newSession = HttpContext.Current.Session.IsNewSession;
					if (newSession && !string.IsNullOrEmpty(originalPath))
					{
						CopyData();
					}

					_dataXml = XDocument.Load(userPath);
				}

				return _dataXml;
			}
		}
	}
}
