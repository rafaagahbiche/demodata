using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.data
{
	public interface IManager<IEntity> where IEntity : Item
	{
		int Insert(IEntity item);
		void Update(IEntity item);
		void Delete(int id); 

	}
}
