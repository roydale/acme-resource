using Acme.Resource.Common.DataTransferObjects;

namespace Acme.Resource.Business.Interfaces
{
	public interface IFakerService
	{
		EmployeeDto GenerateRandomEmployee();

		EmployeeDto GenerateEmployee();

		List<EmployeeDto> GenerateRandomEmployeeList(int count);

		List<EmployeeDto> GenerateEmployeeList(int count);

		string GenerateProfileImage();
	}
}
