namespace demo.data
{
	public interface IManagerFactory<IEntity> where IEntity : ItemData
	{
		IManager<IEntity> GetManager();
	}
}
