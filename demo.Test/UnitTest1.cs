using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using demo.data;
using demo.Domain;
using demo.Service;

namespace demo.Test
{
	[TestClass]
	public class UnitTest1
	{
		private Mock<IRepo<ArticleData>> articleRepository;
		private Mock<IArticleService> articleService;
		private Mock<IManagerFactory<ArticleData>> mockManagerFactory;
		private Mock<IManager<ArticleData>> mockManager;
		private Mock<IDataContext> dbContextMock;

		[TestInitialize()]
		public void MyTestInitialize()
		{
			string path = @"ff";
			var dbContextConcreteMock = new Mock<DataContext>(path);
			dbContextMock = dbContextConcreteMock.As<IDataContext>();

			var repo = new Mock<Repo<ArticleData>>(dbContextMock.Object);
			articleRepository = repo.As<IRepo<ArticleData>>();

			var service = new Mock<ArticleService>(articleRepository.Object);
			articleService = service.As<IArticleService>();

			//mockEntities.Setup(m => m.Articles).Returns(mockSet.Object);
			//dbContextMock.Setup<GrafaaEntities>(x => x.GetContext()).Returns(mockEntities.Object);
			//this.articleRepository = new Repo<Article>(dbContextMock.Object);
		}

		[TestMethod]
		public void TestMethod1()
		{
		}
	}
}
