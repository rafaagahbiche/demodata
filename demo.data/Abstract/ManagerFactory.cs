namespace demo.data
{
	public class ManagerFactory<IEntity> : IManagerFactory<IEntity> 
		where IEntity : ItemData
	{
		private IManager<IEntity> manager;

		public ManagerFactory(IManager<IEntity> manager)
		{
			this.manager = manager;
		}

		public IManager<IEntity> GetManager()
		{
			return manager;
		}
	}
}
