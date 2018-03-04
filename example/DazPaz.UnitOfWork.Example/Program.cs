using DazPaz.UnitOfWork.Example.Model;
using System;
using System.Linq;

namespace DazPaz.UnitOfWork.Example
{
	class Program
	{
		static void Main(string[] args)
		{
			GetListOfCategories(ToDoUnitOfWork.Create());
			GetSpecificCategory(ToDoUnitOfWork.Create());
			AddNewCategory(ToDoUnitOfWork.Create());
			UpdateCategory(ToDoUnitOfWork.Create());
			DeleteCategory(ToDoUnitOfWork.Create());
			DeleteCategoryById(ToDoUnitOfWork.Create());
			GetListOfCategories(ToDoUnitOfWork.Create());
			ResetCategories(ToDoUnitOfWork.Create());

			//GetListOfToDos(ToDoUnitOfWork.Create());
			//GetSpecificToDos(ToDoUnitOfWork.Create());
			//AddNewToDos(ToDoUnitOfWork.Create());
			//UpdateToDos(ToDoUnitOfWork.Create());
			//DeleteToDos(ToDoUnitOfWork.Create());
			//GetListToDos(ToDoUnitOfWork.Create());

			Console.WriteLine("Press any key to continue...");
			Console.ReadLine();
		}

		public static void GetListOfCategories(IToDoUnitOfWork uow)
		{
			var categories = uow.Categories.GetAll().OrderBy(c => c.Title).ToList();
			Console.WriteLine(string.Format("Categories ({0})", categories.Count));
			foreach (var category in categories)
			{
				Console.WriteLine(" - " + category.Title);
			}

			Console.WriteLine();
		}

		private static void GetSpecificCategory(IToDoUnitOfWork uow)
		{
			var category = uow.Categories.GetById(2);
			Console.WriteLine(string.Format("Category 2 : ({0})", category.Title));

			category = uow.Categories.GetById(99);
			if (category == null) Console.WriteLine("There was no Category 99");

			Console.WriteLine();
		}

		private static void AddNewCategory(IToDoUnitOfWork uow)
		{
			uow.Categories.Add(new Category { Title = "Work" });
			uow.Categories.Add(new Category { Title = "Rest" });
			uow.Categories.Add(new Category { Title = "Play" });

			uow.Commit();

			var newCount = uow.Categories.GetAll().Count();

			Console.WriteLine(string.Format("There are now {0} categories", newCount));
			Console.WriteLine();
		}

		private static void UpdateCategory(IToDoUnitOfWork uow)
		{
			var category = uow.Categories.GetAll().Where(c => c.Title == "Rest").FirstOrDefault();

			category.Title = "Taking it easy";
			uow.Commit();

			Console.WriteLine("Updated the category");
			Console.WriteLine();
		}

		private static void DeleteCategory(IToDoUnitOfWork uow)
		{
			var category = uow.Categories.GetAll().Where(c => c.Title == "Work").FirstOrDefault();

			uow.Categories.Delete(category);
			uow.Commit();

			Console.WriteLine(string.Format("Deleted a category, there are now {0}", uow.Categories.GetAll().Count()));
			Console.WriteLine();
		}

		private static void DeleteCategoryById(IToDoUnitOfWork uow)
		{
			var category = uow.Categories.GetAll().Where(c => c.Title == "Play").FirstOrDefault();

			uow.Categories.Delete(category.Id);
			uow.Commit();

			Console.WriteLine(string.Format("Deleted a category by ID, there are now {0}", uow.Categories.GetAll().Count()));
			Console.WriteLine();
		}

		private static void ResetCategories(IToDoUnitOfWork uow)
		{
			var categories = uow.Categories.GetAll().Where(c => c.Id > 4).ToList();

			foreach (var category in categories)
			{
				uow.Categories.Delete(category);
			}

			uow.Commit();
		}
	}
}
