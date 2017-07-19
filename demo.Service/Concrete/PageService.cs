
namespace demo.Service
{
	using demo.data;
	using demo.Domain;
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class PageService: IPageService
	{
		private readonly IRepo<PageData> repo;
		public PageService(IRepo<PageData> repo)
		{
			this.repo = repo;
		}

		public void Delete(int Id)
		{
			throw new NotImplementedException();
		}

		public PageViewModel Get(int id)
		{
			var articleData = repo.GetAll().FirstOrDefault(x => x.Id.Equals(id));
			return articleData.GetViewModel();
		}

		public IEnumerable<PageViewModel> GetAll()
		{
			throw new NotImplementedException();
		}

		public int Insert(PageViewModel page)
		{
			throw new NotImplementedException();
		}

		public bool Update(PageViewModel page)
		{
			throw new NotImplementedException();
		}
	}
}
