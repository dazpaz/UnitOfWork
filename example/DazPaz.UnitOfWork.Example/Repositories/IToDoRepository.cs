using DazPaz.UnitOfWork.Example.Model;
using System.Linq;

namespace DazPaz.UnitOfWork.Example
{
	public interface IToDoRepository : IRepository<ToDo>
	{
		IQueryable<ToDo> GetImportantToDos();
	}
}
