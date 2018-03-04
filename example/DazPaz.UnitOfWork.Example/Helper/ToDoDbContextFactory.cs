using System.Data.Entity;

namespace DazPaz.UnitOfWork.Example.Helper
{
	public class ToDoDbContextFactory : IDbContextFactory
	{
		public DbContext CreateDbContext()
		{
			var dbContext = new ToDoDbContext();
			return dbContext;
		}
	}
}
