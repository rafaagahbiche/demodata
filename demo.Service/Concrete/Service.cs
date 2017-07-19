//using demo.data;
//using System.Collections.Generic;

//namespace demo.Service
//{
//	public class Service<T,V> : IService<T> 
//		where T : ItemViewModel
//		where V : ItemData
//	{
//		protected readonly IRepo<V> repo;

//		protected Service(IRepo<V> repo)
//		{
//			this.repo = repo;
//		}

//		public void Delete(int Id)
//		{
//			repo.Delete(Id);
//		}

//		public IEnumerable<T> GetAll()
//		{
//			foreach (var item in repo.GetAll())
//			{
//				yield return item.GetViewModel();
//			}
//		}

//		public int Insert(T item)
//		{
//			var article = item.GetDataModel();
//			return repo.Insert(article).Id;
//		}

//		public bool Update(T item)
//		{
//			var article = item.GetDataModel();
//			return repo.Update(article);
//		}
//	}
//}
