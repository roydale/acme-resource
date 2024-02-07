using Acme.Resource.Business.Interfaces;
using Acme.Resource.Business.Services;
using Acme.Resource.Common.DataTransferObjects;
using Acme.Resource.Common.Enums;
using Acme.Resource.Web;
using Acme.Resource.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System.Net;

namespace Acme.Resource.Api.Tests
{
	[TestClass]
	public class EmployeesControllerTests
	{
		public readonly EmployeesController controller;
		public readonly ISalaryServiceFactory mockServiceFactory = Substitute.For<ISalaryServiceFactory>();
		public readonly IEmployeeService mockService = Substitute.For<IEmployeeService>();

		public EmployeesControllerTests()
		{
			controller = new EmployeesController(mockServiceFactory, mockService);
		}

		[TestMethod]
		public async Task Get_HasData()
		{
			// Arrange
			mockService.RetrieveEmployeesAsync().Returns(e => StaticEmployees.ResultList);

			// Act
			var response = await controller.Get();

			// Assert
			await mockService.Received(1).RetrieveEmployeesAsync();
			var actualEmployees = (response as OkObjectResult).Value as List<EmployeeDto>;
			Assert.IsNotNull(actualEmployees);
			Assert.AreEqual(actualEmployees.Count, StaticEmployees.ResultList.Count);
			Assert.AreEqual((int)HttpStatusCode.OK, (response as OkObjectResult).StatusCode);
		}

		[TestMethod]
		public async Task Get_NoData()
		{
			// Arrange

			// Act
			var response = await controller.Get();

			// Assert
			await mockService.Received(1).RetrieveEmployeesAsync();
			var actualEmployees = (response as OkObjectResult).Value as List<EmployeeDto>;
			Assert.IsNull(actualEmployees);
			Assert.AreEqual((int)HttpStatusCode.OK, (response as OkObjectResult).StatusCode);
		}

		[TestMethod]
		public async Task GetById_HasData()
		{
			// Arrange
			var expectedEmployee = StaticEmployees.ResultList[0];
			mockService.RetrieveEmployeeByIdAsync(expectedEmployee.Id).Returns(e => expectedEmployee);

			// Act
			var response = await controller.GetById(expectedEmployee.Id);

			// Assert
			await mockService.Received(1).RetrieveEmployeeByIdAsync(expectedEmployee.Id);
			var actualEmployee = (response as OkObjectResult).Value as EmployeeDto;
			Assert.IsNotNull(actualEmployee);
			Assert.IsInstanceOfType(actualEmployee, typeof(EmployeeDto));
			Assert.AreEqual(expectedEmployee.FullName, actualEmployee.FullName);
			Assert.AreEqual((int)HttpStatusCode.OK, (response as OkObjectResult).StatusCode);
		}

		[TestMethod]
		public async Task GetById_NoData()
		{
			// Arrange

			// Act
			var response = await controller.GetById(1);

			// Assert
			await mockService.Received(1).RetrieveEmployeeByIdAsync(Arg.Any<int>());
			var actualEmployee = (response as OkObjectResult).Value;
			Assert.IsNull(actualEmployee);
			Assert.AreEqual((int)HttpStatusCode.OK, (response as OkObjectResult).StatusCode);
		}

		[TestMethod]
		public async Task Put_UpdateEmployee_Success()
		{
			// Arrange
			var expectedEmployee = StaticEmployees.ResultList[0];
			var editEmployeeDto = new EditEmployeeDto()
			{
				Id = expectedEmployee.Id,
				FullName = expectedEmployee.FullName,
				Birthdate = DateTime.Parse(expectedEmployee.Birthdate),
				Tin = expectedEmployee.Tin,
				TypeId = expectedEmployee.TypeId
			};
			mockService.UpdateEmployeeAsync(editEmployeeDto).Returns(e => expectedEmployee);

			// Act
			var response = await controller.Put(editEmployeeDto);

			// Assert
			await mockService.Received(1).UpdateEmployeeAsync(editEmployeeDto);
			var actualEmployee = (response as OkObjectResult).Value as EmployeeDto;
			Assert.IsNotNull(actualEmployee);
			Assert.IsInstanceOfType(actualEmployee, typeof(EmployeeDto));
			Assert.AreEqual(expectedEmployee.FullName, actualEmployee.FullName);
			Assert.AreEqual((int)HttpStatusCode.OK, (response as OkObjectResult).StatusCode);
		}

		[TestMethod]
		public async Task Put_EmployeeToUpdate_NotFound()
		{
			// Arrange
			mockService.UpdateEmployeeAsync(Arg.Any<EditEmployeeDto>()).ReturnsNull();

			// Act
			var response = await controller.Put(new EditEmployeeDto());

			// Assert
			await mockService.Received(1).UpdateEmployeeAsync(Arg.Any<EditEmployeeDto>());
			var actualStatusCode = (response as StatusCodeResult).StatusCode;
			Assert.AreEqual((int)HttpStatusCode.NotFound, actualStatusCode);
		}

		[TestMethod]
		public async Task Post_AddEmployee_Success()
		{
			// Arrange
			var expectedNewEmployeeId = 5;
			var expectedEmployee = StaticEmployees.ResultList[0];
			var createEmployeeDto = new CreateEmployeeDto()
			{
				FullName = expectedEmployee.FullName,
				Birthdate = DateTime.Parse(expectedEmployee.Birthdate),
				Tin = expectedEmployee.Tin,
				TypeId = expectedEmployee.TypeId
			};
			mockService.CreateEmployeeAsync(createEmployeeDto).Returns(e => expectedNewEmployeeId);

			// Act
			var response = await controller.Post(createEmployeeDto);

			// Assert
			await mockService.Received(1).CreateEmployeeAsync(createEmployeeDto);
			var actualResult = response as CreatedResult;
			Assert.IsNotNull(actualResult);
			Assert.IsInstanceOfType(actualResult, typeof(CreatedResult));
			Assert.AreEqual(expectedNewEmployeeId, actualResult.Value);
			Assert.AreEqual((int)HttpStatusCode.Created, actualResult.StatusCode);
			Assert.IsTrue(actualResult.Location.Contains($"/api/employees/{expectedNewEmployeeId}"));
		}

		[TestMethod]
		public async Task Delete_Employee_Success()
		{
			// Arrange
			var expectedEmployee = StaticEmployees.ResultList[0];
			mockService.DeleteEmployeeAsync(expectedEmployee.Id).Returns(e => expectedEmployee.Id);

			// Act
			var response = await controller.Delete(expectedEmployee.Id);

			// Assert
			await mockService.Received(1).DeleteEmployeeAsync(expectedEmployee.Id);
			var actualEmployeeId = (int)(response as OkObjectResult).Value;
			Assert.IsNotNull(actualEmployeeId);
			Assert.IsInstanceOfType(actualEmployeeId, typeof(int));
			Assert.AreEqual(expectedEmployee.Id, actualEmployeeId);
			Assert.AreEqual((int)HttpStatusCode.OK, (response as OkObjectResult).StatusCode);
		}

		[TestMethod]
		public async Task Delete_EmployeeToDelete_NotFound()
		{
			// Arrange
			mockService.DeleteEmployeeAsync(Arg.Any<int>()).Returns(i => 0);

			// Act
			var response = await controller.Delete(1);

			// Assert
			await mockService.Received(1).DeleteEmployeeAsync(Arg.Any<int>());
			var actualStatusCode = (response as StatusCodeResult).StatusCode;
			Assert.AreEqual((int)HttpStatusCode.NotFound, actualStatusCode);
		}

		[TestMethod]
		public async Task Calculate_EmployeeNotFound()
		{
			// Arrange
			var employee = StaticEmployees.ResultList[1];
			employee.Id = 3;
			var calculateDtoRegular = new CalculateSalaryDto
			{
				Id = employee.Id,
				EmployeeType = (EmployeeType)employee.TypeId
			};

			// Act
			var response = await controller.Calculate(calculateDtoRegular);

			// Assert
			mockServiceFactory.Received(0).Create(calculateDtoRegular);
			var actualStatusCode = (response as StatusCodeResult).StatusCode;
			Assert.AreEqual((int)HttpStatusCode.NotFound, actualStatusCode);
		}

		[TestMethod]
		public async Task Calculate_RegularSalaryAsync()
		{
			// Arrange
			var regularEmployee = StaticEmployees.ResultList[0];
			var calculateDtoRegular = new CalculateSalaryDto
			{
				Id = regularEmployee.Id,
				AbsentDays = 1,
				EmployeeType = (EmployeeType)regularEmployee.TypeId
			};
			var regularSalaryService = new RegularSalaryService();
			regularSalaryService.SalaryDependency = calculateDtoRegular;

			mockService.RetrieveEmployeeByIdAsync(regularEmployee.Id).Returns(e => regularEmployee);
			mockServiceFactory.Create(calculateDtoRegular).Returns(s => regularSalaryService);

			// Act
			var response = await controller.Calculate(calculateDtoRegular);

			// Assert
			mockServiceFactory.Received(1).Create(calculateDtoRegular);
			var expectedRegularSalary = regularSalaryService.Compute();
			var actualRegularSalary = (response as OkObjectResult).Value;
			Assert.AreEqual(expectedRegularSalary, actualRegularSalary);
		}

		[TestMethod]
		public async Task Calculate_ContractualSalaryAsync()
		{
			// Arrange
			var contractualEmployee = StaticEmployees.ResultList[1];
			var calculateDtoRegular = new CalculateSalaryDto
			{
				Id = contractualEmployee.Id,
				WorkedDays = 15.5M,
				EmployeeType = (EmployeeType)contractualEmployee.TypeId
			};
			var contractualSalaryService = new ContractualSalaryService();
			contractualSalaryService.SalaryDependency = calculateDtoRegular;

			mockService.RetrieveEmployeeByIdAsync(contractualEmployee.Id).Returns(e => contractualEmployee);
			mockServiceFactory.Create(calculateDtoRegular).Returns(s => contractualSalaryService);

			// Act
			var response = await controller.Calculate(calculateDtoRegular);

			// Assert
			mockServiceFactory.Received(1).Create(calculateDtoRegular);
			var expectedRegularSalary = contractualSalaryService.Compute();
			var actualRegularSalary = (response as OkObjectResult).Value;
			Assert.AreEqual(expectedRegularSalary, actualRegularSalary);
		}
	}
}
