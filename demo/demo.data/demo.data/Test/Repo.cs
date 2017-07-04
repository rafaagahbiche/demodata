using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.data.Test
{
	public class Repo<T> where T : Item, new()
	{
		protected readonly IManager<T> dbContext;

		public Repo(ManagerFactory<T> dbContextFactory)
		{
			dbContext = dbContextFactory.GetManager();
		}

		public T Insert(T o)
		{
			o.Id = dbContext.Insert(o);
			return o;
		}
	}
}
