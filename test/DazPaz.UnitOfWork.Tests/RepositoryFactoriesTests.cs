using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace DazPaz.UnitOfWork.Tests
{
	[TestClass]
	public class RepositoryFactoriesTests
	{
		[TestMethod]
		public void CanGetAFactoryThatWillCreateARepositoryForAnEntityClass()
		{
			IRepositoryFactories repoFactories = new RepositoryFactories();

			var factory = repoFactories.GetRepositoryFactoryForEntityType<Person>();

			Assert.IsNotNull(factory);
		}

		[TestMethod]
		public void CanGetAFactoryThatCanThenBeUsedToCreateARepositoryForAnEntityClass()
		{
			IRepositoryFactories repoFactories = new RepositoryFactories();

			var repoFactory = repoFactories.GetRepositoryFactoryForEntityType<Person>();
			var repositoryObject = repoFactory(new DbContext("Test"));

			var repository = repositoryObject as IRepository<Person>;
			Assert.IsNotNull(repository);
		}

		[TestMethod]
		public void CanProvideADictionaryOfBespokeFactoriesAndThenUseABespokeFactoryToGetARepository()
		{
			var factories = new Dictionary<Type, Func<DbContext, object>>
			{
				{typeof(Person), dbContext => new PersonRepository(dbContext)}
			};

			IRepositoryFactories repoFactories = new RepositoryFactories(factories);
			var repoFactory = repoFactories.GetRepositoryFactoryForEntityType<Person>();

			var repository = repoFactory(new DbContext("Test")) as PersonRepository;
			Assert.IsNotNull(repository);
		}
	}
}
