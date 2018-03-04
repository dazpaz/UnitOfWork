using System;
using System.Linq;

namespace DazPaz.UnitOfWork.Example
{
	class Program
	{
		static void Main(string[] args)
		{
			GetListOfCategories(ToDoUnitOfWork.Create());
			//GetSpecificCategory(ToDoUnitOfWork.Create());
			//AddNewCategory(ToDoUnitOfWork.Create());
			//UpdateCategory(ToDoUnitOfWork.Create());
			//DeleteCategory(ToDoUnitOfWork.Create());
			GetListOfCategories(ToDoUnitOfWork.Create());

			//GetListOfToDos(ToDoUnitOfWork.Create());
			//GetSpecificToDos(ToDoUnitOfWork.Create());
			//AddNewToDos(ToDoUnitOfWork.Create());
			//UpdateToDos(ToDoUnitOfWork.Create());
			//DeleteToDos(ToDoUnitOfWork.Create());
			//GetListToDos(ToDoUnitOfWork.Create());
		}

		public static void GetListOfCategories(IToDoUnitOfWork uow)
		{
			var categories = uow.Categories.GetAll().OrderBy(c => c.Title).ToList();
			Console.WriteLine(string.Format("Categories ({0})", categories.Count));
			foreach (var category in categories)
			{
				Console.WriteLine(" - " + category);
			}

			Console.WriteLine();
		}
	}
}
