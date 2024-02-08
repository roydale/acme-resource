namespace Acme.Resource.Data.Models
{
	public class Employee
	{
		public int Id { get; set; }

		public string FullName { get; set; }

		public DateTime Birthdate { get; set; }

		public string TIN { get; set; }

		public int EmployeeTypeId { get; set; }

		public string ProfileImage { get; set; }

		public bool IsDeleted { get; set; }
	}
}
