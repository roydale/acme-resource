using Acme.Resource.Common.DataTransferObjects;

namespace Acme.Resource.Business.Interfaces
{
	public interface IEmployeeService
	{
		Task<int> CreateEmployeeAsync(CreateEmployeeDto employeeDto);

		Task<List<EmployeeDto>> RetrieveEmployeesAsync();

		Task<EmployeeDto> RetrieveEmployeeByIdAsync(int id);

		Task<EmployeeDto> UpdateEmployeeAsync(EditEmployeeDto employeeDto);

		Task<int> DeleteEmployeeAsync(int id);
	}
}
