using System.Data.Entity;

namespace DazPaz.UnitOfWork
{
	public interface IDbContextFactory
	{
		DbContext CreateDbContext();
	}
}
