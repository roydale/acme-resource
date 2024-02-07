using Acme.Resource.Common.DataTransferObjects;
using Acme.Resource.Common.Enums;

namespace Acme.Resource.Business.Interfaces
{
	public interface ISalaryService
	{
		EmployeeType EmployeeType { get; }

		CalculateSalaryDto SalaryDependency { get; set; }

		decimal Compute();
	}
}
