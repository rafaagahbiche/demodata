using System.Linq;

namespace demo.data
{
	public interface IManager<IEntity> where IEntity : ItemData
	{
		int Insert(IEntity item);
		bool Update(IEntity item);
		void Delete(int id);
		IQueryable<IEntity> GetAll();
	}
}
