namespace demo.data
{
	public class main
	{
		private static readonly string _path = "@/Data/data.xml";
		private IDataContext context;
		private IManager<ArticleData> manager;
		ManagerFactory<ArticleData> factory;
		ArticleData article = new ArticleData();
		public void executeMethod()
		{
			//kernel.Bind<IDataContext>().To<DataContext>().InRequestScope();
			//kernel.Bind<IArticleService>().To<ArticleService>();
			//kernel.Bind<IManager<ArticleData>>().To<ArticleManager>();
			//kernel.Bind<IRepo<ArticlePage>>().To<Repo<ArticlePage>>();
			//kernel.Bind<IAuthProvider>().To<FormsAuthProvider>();

			context = new DataContext(_path);
			manager = new ArticleManager(context);
			factory = new ArticleManagerFactory(manager);
			factory.GetManager().Insert(article);
		}
	}
}
