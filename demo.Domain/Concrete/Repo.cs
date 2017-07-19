using System;
using System.Linq;
using demo.data;

namespace demo.Domain
{
	public class Repo<T> : IRepo<T> where T : ItemData
	{
		protected readonly IManager<T> manager;

		public Repo(IManagerFactory<T> managerFactory)
		{
			this.manager = managerFactory.GetManager();
		}

		public void Delete(int id)
		{
			manager.Delete(id);
		}

		public IQueryable<T> GetAll()
		{
			return manager.GetAll();
		}

		public T Insert(T o)
		{
			o.Id = manager.Insert(o);
			return o;
		}

		public bool Update(T item)
		{
			return manager.Update(item);
		}
	}
}
