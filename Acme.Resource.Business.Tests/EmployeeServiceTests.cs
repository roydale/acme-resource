using Acme.Resource.Data;
using Acme.Resource.Data.Models;
using Acme.Resource.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Acme.Resource.Business.Tests
{
	[TestClass]
	public class EmployeeServiceTests
	{
		private static DbContextOptions<AcmeResourceDbContext> dbContextOptions = new DbContextOptionsBuilder<AcmeResourceDbContext>()
			.UseInMemoryDatabase("AcmeResourceMemory")
			.Options;
		private AcmeResourceDbContext dbContext;

		[TestInitialize()]
		public void Initialize()
		{
			dbContext = new AcmeResourceDbContext(dbContextOptions);
			dbContext.Database.EnsureCreated();
			PopulateDatabase();
		}

		[TestMethod]
		public void RetrieveEmployeesAsync_HasData()
		{
			// Arrange

			// Act

			// Assert
		}

		[TestCleanup()]
		public void Cleanup()
		{
			dbContext.Database.EnsureDeleted();
		}

		private void PopulateDatabase()
		{
			var employees = new List<Employee>();
			employees.AddRange(StaticEmployees.ResultList.Select(e => new Employee
			{
				Id = e.Id,
				Birthdate = DateTime.Parse(e.Birthdate),
				FullName = e.FullName,
				TIN = e.Tin,
				EmployeeTypeId = e.TypeId,
				ProfileImage = string.Empty,
				IsDeleted = false
			}));
			dbContext.Employees.AddRange(employees);

			dbContext.SaveChanges();
		}
	}
}
