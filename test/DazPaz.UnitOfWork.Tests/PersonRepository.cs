using System.Data.Entity;

namespace DazPaz.UnitOfWork.Tests
{
	public interface IPersonRepository
	{
		int GetFavouriteNumber();
	}

	class PersonRepository : EFRepository<Person>, IPersonRepository
	{
		public PersonRepository(DbContext dbContext) : base(dbContext)
		{
		}

		public int GetFavouriteNumber()
		{
			return 42;
		}
	}
}
