using DazPaz.UnitOfWork.Example.Helper;
using DazPaz.UnitOfWork.Example.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace DazPaz.UnitOfWork.Example
{
	class ToDoUnitOfWork : UoW, IToDoUnitOfWork
	{
		protected ToDoUnitOfWork(IRepositoryProvider repositoryProvider, IDbContextFactory contextFactory) : base(repositoryProvider, contextFactory)
		{
		}

		public IToDoRepository ToDos { get { return GetCustomRepo<IToDoRepository>();  } }
		public IRepository<Category> Categories { get { return GetGenericRepo<Category>(); } }

		public static IDictionary<Type, Func<DbContext, object>> CustomFactories = new Dictionary<Type, Func<DbContext, object>>
		{
			{ typeof(ToDo), dbContext => new ToDoRepository(dbContext) }
		};

		public static IToDoUnitOfWork Create()
		{
			var repositoryFactories = new RepositoryFactories(CustomFactories);
			var repositoryProvider = new RepositoryProvider(repositoryFactories);
			var contextFactory = new ToDoDbContextFactory();

			return new ToDoUnitOfWork(repositoryProvider, contextFactory);
		}
	}
}
