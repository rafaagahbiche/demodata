using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
