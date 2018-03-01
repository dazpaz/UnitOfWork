using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace DazPaz.UnitOfWork
{
	public class RepositoryProvider : IRepositoryProvider
	{
		public DbContext DbContext { get; set; }

		protected Dictionary<Type, object> Repositories { get; private set; }
		private IRepositoryFactories RepositoryFactories { get; set; }

		public RepositoryProvider(IRepositoryFactories repositoryFactories)
		{
			RepositoryFactories = repositoryFactories;
			Repositories = new Dictionary<Type, object>();
		}

		public IRepository<T> GetRepositoryForEntityType<T>() where T : class
		{
			var repoFactory = RepositoryFactories.GetRepositoryFactoryForEntityType<T>();
			return GetRepository<IRepository<T>>(repoFactory);
		}

		public virtual T GetRepository<T>(Func<DbContext, object> factory = null) where T : class
		{
			// Look for T dictionary cache under typeof(T).
			Repositories.TryGetValue(typeof(T), out object repoObj);
			if (repoObj != null)
			{
				return (T)repoObj;
			}

			// Not found or null; make one, add to dictionary cache, and return it.
			return MakeRepository<T>(factory, DbContext);
		}

		protected virtual T MakeRepository<T>(Func<DbContext, object> factory, DbContext dbContext)
		{
			var f = factory ?? RepositoryFactories.GetRepositoryFactory<T>();
			if (f == null)
			{
				throw new NotImplementedException("No factory for repository type, " + typeof(T).FullName);
			}
			var repo = (T)f(dbContext);
			Repositories[typeof(T)] = repo;
			return repo;
		}

		public void SetRepository<T>(T repository)
		{
			Repositories[typeof(T)] = repository;
		}
	}
}
