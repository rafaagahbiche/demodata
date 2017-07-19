using demo.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.Service
{
	public interface IService<T> 
		where T : ItemViewModel
	{
		IEnumerable<T> GetAll();
		T Get(int id);
		int Insert(T item);
		void Delete(int Id);
		bool Update(T item);
	}
}
