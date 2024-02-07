using Acme.Resource.Common.Enums;

namespace Acme.Resource.Common.DataTransferObjects
{
	public class CalculateSalaryDto
	{
		public int Id { get; set; }

		public EmployeeType EmployeeType { get; set; }

		public decimal AbsentDays { get; set; }

		public decimal WorkedDays { get; set; }
	}
}
