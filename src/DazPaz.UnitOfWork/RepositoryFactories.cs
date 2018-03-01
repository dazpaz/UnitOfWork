using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace DazPaz.UnitOfWork
{
	public class RepositoryFactories : IRepositoryFactories
	{
		protected IDictionary<Type, Func<DbContext, object>> RepositoryFactoryDictionary { get; set; }

		public RepositoryFactories()
		{
			RepositoryFactoryDictionary = new Dictionary<Type, Func<DbContext, object>>();
		}

		public RepositoryFactories(IDictionary<Type, Func<DbContext, object>> factories)
		{
			RepositoryFactoryDictionary = factories;
		}

		public Func<DbContext, object> GetRepositoryFactoryForEntityType<T>() where T : class
		{
			var repoFactory = GetRepositoryFactory<T>();
			return repoFactory ?? DefaultEntityRepositoryFactory<T>();
		}

		public Func<DbContext, object> GetRepositoryFactory<T>()
		{
			RepositoryFactoryDictionary.TryGetValue(typeof(T), out Func<DbContext, object> factory);
			return factory;
		}

		protected virtual Func<DbContext, object> DefaultEntityRepositoryFactory<T>() where T : class
		{
			return dbContext => new EFRepository<T>(dbContext);
		}
	}
}
