using Acme.Resource.Business.Interfaces;
using Acme.Resource.Common.DataTransferObjects;
using Acme.Resource.Common.Utilities.DataGenerator;

namespace Acme.Resource.Business.Services
{
	public class FakerService(FakeDataGenerator _generator) : IFakerService
	{
		public EmployeeDto GenerateEmployee()
		{
			return _generator.GenerateEmployee();
		}

		public List<EmployeeDto> GenerateEmployeeList(int count)
		{
			return _generator.GenerateEmployeeList(count);
		}

		public string GenerateProfileImage()
		{
			return FakeDataGenerator.GenerateRandomProfileImage();
		}

		public EmployeeDto GenerateRandomEmployee()
		{
			return FakeDataGenerator.GenerateRandomEmployee();
		}

		public List<EmployeeDto> GenerateRandomEmployeeList(int count)
		{
			return FakeDataGenerator.GenerateRandomEmployeeList(count);
		}
	}
}
