using Acme.Resource.Common.DataTransferObjects;
using Acme.Resource.Common.Utilities.DataGenerator.Fakers;
using Bogus;

namespace Acme.Resource.Common.Utilities.DataGenerator
{
	public class FakeDataGenerator
	{
		private readonly int Seed = 6955128;

		public static EmployeeDto GenerateRandomEmployee()
		{
			return EmployeeFaker.GetEmployeeFaker().Generate();
		}

		public EmployeeDto GenerateEmployee()
		{
			Randomizer.Seed = new Random(Seed);
			return EmployeeFaker.GetEmployeeFaker().Generate();
		}

		public static List<EmployeeDto> GenerateRandomEmployeeList(int count)
		{
			return EmployeeFaker.GetEmployeeFaker().Generate(count);
		}

		public List<EmployeeDto> GenerateEmployeeList(int count)
		{
			Randomizer.Seed = new Random(Seed);
			return EmployeeFaker.GetEmployeeFaker().Generate(count);
		}

		public static string GenerateRandomProfileImage()
		{
			return new Faker("en").Person.Avatar;
		}
	}
}
