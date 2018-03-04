using DazPaz.UnitOfWork.Example.Model;
using System.Data.Entity;

namespace DazPaz.UnitOfWork.Example
{
	public class ToDoDbContext : DbContext
	{
		public DbSet<ToDo> ToDos { get; set; }
		public DbSet<Category> Categories { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}
	}
}
