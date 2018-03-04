using System;

namespace DazPaz.UnitOfWork.Example.Model
{
	public class ToDo
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public Status Status { get; set; }
		public bool IsImportant { get; set; }
		public DateTime DueDate { get; set; }

		public int CategoryId { get; set; }
		public Category Category { get; set; }
	}
}
