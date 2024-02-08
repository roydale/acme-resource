using Acme.Resource.Common.DataTransferObjects;
using Acme.Resource.Common.Enums;
using Bogus;
using Bogus.Extensions.UnitedStates;

namespace Acme.Resource.Common.Utilities.DataGenerator.Fakers
{
	internal class EmployeeFaker
	{
		public static Faker<EmployeeDto> GetEmployeeFaker()
		{
			return new Faker<EmployeeDto>()
				.RuleFor(e => e.FullName, f => f.Name.FullName())
				.RuleFor(e => e.Birthdate, f => f.Person.DateOfBirth.ToString("yyyy-MM-dd"))
				.RuleFor(e => e.Tin, f => f.Person.Ssn())
				.RuleFor(e => e.TypeId, f => (int)f.PickRandom<EmployeeType>())
				.RuleFor(e => e.ProfileImage, f => f.Person.Avatar);
		}
	}
}
