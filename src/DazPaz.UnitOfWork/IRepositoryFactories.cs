using System;
using System.Data.Entity;

namespace DazPaz.UnitOfWork
{
	public interface IRepositoryFactories
	{
		Func<DbContext, object> GetRepositoryFactoryForEntityType<T>() where T : class;
		Func<DbContext, object> GetRepositoryFactory<T>();
	}

}
