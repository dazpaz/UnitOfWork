namespace DazPaz.UnitOfWork.Tests
{
	public enum Gender
	{
		Male,
		Female
	}

	public class Person
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public Gender Gender { get; set; }
		public int Age { get; set; }
	}
}
