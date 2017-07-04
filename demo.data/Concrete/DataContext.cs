namespace demo.data
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using System.Xml.Linq;

	public class DataContext: IDataContext
	{
		//private static readonly string _path = "@/Data/data.xml";
		private XDocument _dataXml;
		public DataContext(string _path)
		{
			_dataXml = XDocument.Load(_path);
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
