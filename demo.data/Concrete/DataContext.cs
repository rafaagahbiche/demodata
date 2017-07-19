namespace demo.data
{
	using System.Xml.Linq;

	public class DataContext: IDataContext
	{
		//private static readonly string _path = "@/Data/data.xml";
		private XDocument _dataXml;
		//private string path;
		private static readonly string path = @"/App_Data/data.xml";
		public DataContext()
		{
			_dataXml = XDocument.Load(path);
		}

		public void SaveFile()
		{
			_dataXml.Save(path);
		}


		public XDocument DataXml
		{
			get
			{
				return _dataXml;
			}
		}
	}
}
