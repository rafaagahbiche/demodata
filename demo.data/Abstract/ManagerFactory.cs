using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.data
{
	public abstract class ManagerFactory<IEntity> where IEntity : Item
	{
		public abstract IManager<IEntity> GetManager();
	}
}
