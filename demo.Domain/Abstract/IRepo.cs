using System.Linq;
using demo.data;
namespace demo.Domain
{
	public interface IRepo<T> where T : ItemData
	{
		IQueryable<T> GetAll();
		T Insert(T o);
		void Delete(int id);
		bool Update(T item);
	}
}
