namespace DazPaz.UnitOfWork.Example.Migrations
{
	using DazPaz.UnitOfWork.Example.Model;
	using System;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;

	internal sealed class Configuration : DbMigrationsConfiguration<ToDoDbContext>
	{
		public Configuration()
		{
			AutomaticMigrationsEnabled = false;
		}

		protected override void Seed(ToDoDbContext context)
		{
			context.Categories.AddOrUpdate(c => c.Id,
				new Category[] {
					new Category { Title = "Household" },
					new Category { Title = "Shopping" },
					new Category { Title = "Family" },
					new Category { Title = "Work" }
				});
		}
	}
}
