using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.data
{
	public class ArticleManagerFactory : ManagerFactory<ArticleData>
	{
		private IManager<ArticleData> manager;

		public ArticleManagerFactory(IManager<ArticleData> manager)
		{
			this.manager = manager;
		}

		public override IManager<ArticleData> GetManager()
		{
			return manager;
		}
	}
}
