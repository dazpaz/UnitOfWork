using DazPaz.UnitOfWork.Example.Model;
using System.Linq;
using System.Data.Entity;

namespace DazPaz.UnitOfWork.Example
{
	public class ToDoRepository : EFRepository<ToDo>, IToDoRepository
	{
		public ToDoRepository(DbContext dbContext) : base(dbContext)
		{
		}

		public IQueryable<ToDo> GetImportantToDos()
		{
			return DbSet
				.Where(t => t.IsImportant)
				.OrderByDescending(t => t.DueDate);
		}
	}
}
