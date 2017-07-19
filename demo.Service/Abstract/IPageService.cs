using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.Service
{
	public interface IPageService
	{
		IEnumerable<PageViewModel> GetAll();
		int Insert(PageViewModel page);
		void Delete(int Id);
		bool Update(PageViewModel page);

		PageViewModel Get(int id);
	}
}
