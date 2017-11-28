[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(demo.Infra.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(demo.Infra.App_Start.NinjectWebCommon), "Stop")]

namespace demo.Infra.App_Start
{
	using System;
	using System.Web;

	using Microsoft.Web.Infrastructure.DynamicModuleHelper;

	using Ninject;
	using Ninject.Web.Common;
	using demo.Domain;
	using demo.data;
	using demo.Service;

	public static class NinjectWebCommon
	{
		private static readonly Bootstrapper bootstrapper = new Bootstrapper();

		/// <summary>
		/// Starts the application
		/// </summary>
		public static void Start()
		{
			DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
			DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
			bootstrapper.Initialize(CreateKernel);
		}

		/// <summary>
		/// Stops the application.
		/// </summary>
		public static void Stop()
		{
			bootstrapper.ShutDown();
		}

		/// <summary>
		/// Creates the kernel that will manage your application.
		/// </summary>
		/// <returns>The created kernel.</returns>
		private static IKernel CreateKernel()
		{
			var kernel = new StandardKernel();
			try
			{
				kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
				kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

				RegisterServices(kernel);
				return kernel;
			}
			catch
			{
				kernel.Dispose();
				throw;
			}
		}

		/// <summary>
		/// Load your modules or register your services here!
		/// </summary>
		/// <param name="kernel">The kernel.</param>
		private static void RegisterServices(IKernel kernel)
		{
			var originalFilepath = System.Configuration.ConfigurationManager.AppSettings["originaldata"];
			var userFilepath = System.Configuration.ConfigurationManager.AppSettings["userdata"];

			//kernel.Bind<IDataContext>().To<DataContext>().InRequestScope()
			//	.WithConstructorArgument("originalDataPath", string.Format(@"{0}", originalFilepath))
			//	.WithConstructorArgument("userDataPath", string.Format(@"{0}", userFilepath));

			kernel.Bind<IDataContext>().To<DataContext>().InRequestScope()
				.WithConstructorArgument("originalDataPath", string.Format(@"{0}", userFilepath));

			kernel.Bind<IArticleService>().To<ArticleService>();
			kernel.Bind<IPageService>().To<PageService>();

			kernel.Bind<IRepo<ArticleData>>().To<Repo<ArticleData>>();
			kernel.Bind<IRepo<PageData>>().To<Repo<PageData>>();

			kernel.Bind<IManagerFactory<ArticleData>>().To<ManagerFactory<ArticleData>>();
			kernel.Bind<IManagerFactory<PageData>>().To<ManagerFactory<PageData>>();

			kernel.Bind<IManager<ArticleData>>().To<ArticleManager>();
			kernel.Bind<IManager<PageData>>().To<PageManager>();
		}
	}
}
