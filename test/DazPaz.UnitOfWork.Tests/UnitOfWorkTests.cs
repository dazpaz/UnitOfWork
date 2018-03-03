using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data.Entity;

namespace DazPaz.UnitOfWork.Tests
{
	[TestClass]
	public class UnitOfWorkTests
	{
		private Mock<IRepositoryProvider> MockRepositoryProvider { get; set; }
		private Mock<IDbContextFactory> MockDbContextFactory { get; set; }
		private Mock<DbContext> MockDbContext { get; set; }

		[TestInitialize]
		public void TestInitialize()
		{
			MockDbContext = new Mock<DbContext>(MockBehavior.Strict);
			MockRepositoryProvider = new Mock<IRepositoryProvider>(MockBehavior.Strict);
			MockDbContextFactory = new Mock<IDbContextFactory>(MockBehavior.Strict);

			MockDbContextFactory.Setup(cf => cf.CreateDbContext()).Returns(MockDbContext.Object);
			MockRepositoryProvider.SetupSet(rp => rp.DbContext = MockDbContext.Object);
		}

		[TestCleanup]
		public void TestCleanup()
		{
			MockRepositoryProvider.VerifyAll();
			MockDbContextFactory.VerifyAll();
			MockDbContext.VerifyAll();
		}

		[TestMethod]
		public void CanUseTheUnitOfWorkToGetGenericRepository()
		{
			Mock<IRepository<Person>> mockGenericRepository = new Mock<IRepository<Person>>();
			MockRepositoryProvider.Setup(rp => rp.GetRepositoryForEntityType<Person>()).Returns(mockGenericRepository.Object);

			var uow = new TestUoW(MockRepositoryProvider.Object, MockDbContextFactory.Object);

			var genericRepository = uow.GenericRepository;

			Assert.IsNotNull(genericRepository);
		}

		[TestMethod]
		public void CanUseTheUnitOfWorkToGetCustomRepositoryFrom()
		{
			Mock<IPersonRepository> mockCustomRepository = new Mock<IPersonRepository>();
			MockRepositoryProvider.Setup(rp => rp.GetRepository<IPersonRepository>(null)).Returns(mockCustomRepository.Object);

			var uow = new TestUoW(MockRepositoryProvider.Object, MockDbContextFactory.Object);

			var customRepository = uow.CustomRepository;

			Assert.IsNotNull(customRepository);
		}

		[TestMethod]
		public void CanCallCommitOnTheUnitOfWorkAndItCallsSaveChangesOnTheDbContext()
		{
			MockDbContext.Setup(c => c.SaveChanges()).Returns(0);

			var uow = new TestUoW(MockRepositoryProvider.Object, MockDbContextFactory.Object);

			uow.Commit();
		}
	}
}
