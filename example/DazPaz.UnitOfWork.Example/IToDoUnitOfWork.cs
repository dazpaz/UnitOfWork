using DazPaz.UnitOfWork.Example.Model;

namespace DazPaz.UnitOfWork.Example
{
	public interface IToDoUnitOfWork : IUnitOfWork
	{
		IToDoRepository ToDos { get ; }
		IRepository<Category> Categories { get ; }
	}
}
