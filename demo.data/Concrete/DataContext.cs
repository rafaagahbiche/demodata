namespace demo.data
{
	using System;
	using System.Xml.Linq;

	public class DataContext: IDataContext
	{
		//private static readonly string _path = "@/Data/data.xml";
		private XDocument _dataXml;
		
		private string path;
		public DataContext(string path)
		{
			//HttpContext.Current.Server.MapPath
			//this.path = System.IO.Directory.GetCurrentDirectory HttpContext.Current.Server.MapPath(path);
			this.path = path;
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
