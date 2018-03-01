using System;
using System.Data.Entity;

namespace DazPaz.UnitOfWork
{
	public abstract class UoW : IUnitOfWork, IDisposable
	{
		protected DbContext DbContext { get; set; }
		protected IRepositoryProvider RepositoryProvider { get; set; }

		protected UoW(IRepositoryProvider repositoryProvider, IDbContextFactory contextFactory)
		{
			DbContext = contextFactory.CreateDbContext();

			repositoryProvider.DbContext = DbContext;
			RepositoryProvider = repositoryProvider;
		}

		public void Commit()
		{
			DbContext.SaveChanges();
		}

		protected IRepository<T> GetGenericRepo<T>() where T : class
		{
			return RepositoryProvider.GetRepositoryForEntityType<T>();
		}

		protected T GetCustomRepo<T>() where T : class
		{
			return RepositoryProvider.GetRepository<T>();
		}

		#region IDisposable

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (DbContext != null)
				{
					DbContext.Dispose();
				}
			}
		}

		#endregion
	}
}
