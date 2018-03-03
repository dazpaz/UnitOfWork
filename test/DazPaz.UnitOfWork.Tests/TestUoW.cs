namespace DazPaz.UnitOfWork.Tests
{
	public class TestUoW : UoW
	{
		public IPersonRepository CustomRepository { get { return GetCustomRepo<IPersonRepository>(); } }
		public IRepository<Person> GenericRepository { get { return GetGenericRepo<Person>(); } }

		public TestUoW(IRepositoryProvider repositoryProvider, IDbContextFactory contextFactory) : base(repositoryProvider, contextFactory)
		{
		}
	}
}

