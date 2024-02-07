using Acme.Resource.Common.DataTransferObjects;

namespace Acme.Resource.Web
{
	public static class StaticEmployees
	{
		public static List<EmployeeDto> ResultList = new()
		{
			new EmployeeDto
			{
				Birthdate = "1992-10-11",
				FullName = "Jane Doe",
				Id = 1,
				Tin = "00123789456",
				TypeId = 1
			},
			new EmployeeDto
			{
				Birthdate = "1990-09-01",
				FullName = "John Doe",
				Id = 2,
				Tin = "00789321654",
				TypeId = 2
			}
		};
	}
}
