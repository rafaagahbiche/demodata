using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace demo.data
{
	public interface IDataContext
	{
		XDocument DataXml { get; }
		void SaveFile();
	}
}
