using Acme.Resource.Business.Interfaces;
using Acme.Resource.Common.DataTransferObjects;
using Acme.Resource.Data;
using Acme.Resource.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Acme.Resource.Business.Services
{
	public class EmployeeService(AcmeResourceDbContext context) : IEmployeeService
	{
		private readonly AcmeResourceDbContext _context = context;

		public async Task<List<EmployeeDto>> RetrieveEmployeesAsync()
		{
			return await _context.Employees
			.Where(e => !e.IsDeleted)
			.Select(e => ConvertToDto(e)).ToListAsync();
		}

		public async Task<EmployeeDto> RetrieveEmployeeByIdAsync(int id)
		{
			//var employee = await _context.Employees.SingleOrDefaultAsync(e => e.Id == id);
			var employee = await _context.Employees.FindAsync(id);
			return ConvertToDto(employee);
		}

		public async Task<EmployeeDto> UpdateEmployeeAsync(EditEmployeeDto employeeDto)
		{
			var employeeToUpdate = await _context.Employees.FindAsync(employeeDto.Id);
			if (employeeToUpdate == null)
			{
				return null;
			}

			employeeToUpdate.FullName = employeeDto.FullName;
			employeeToUpdate.TIN = employeeDto.Tin;
			employeeToUpdate.Birthdate = employeeDto.Birthdate;
			employeeToUpdate.EmployeeTypeId = employeeDto.TypeId;
			employeeToUpdate.ProfileImage = employeeDto.ProfileImage;
			_context.SaveChanges();

			return ConvertToDto(employeeToUpdate);
		}

		public async Task<int> CreateEmployeeAsync(CreateEmployeeDto employeeDto)
		{
			var employeeToCreate = new Employee
			{
				FullName = employeeDto.FullName,
				Birthdate = employeeDto.Birthdate,
				TIN = employeeDto.Tin,
				EmployeeTypeId = employeeDto.TypeId,
				ProfileImage = employeeDto.ProfileImage,
				IsDeleted = false
			};
			await _context.Employees.AddAsync(employeeToCreate);
			_context.SaveChanges();

			return employeeToCreate.Id;
		}

		public async Task<int> DeleteEmployeeAsync(int id)
		{
			var employeeToDelete = await _context.Employees.FindAsync(id);
			if (employeeToDelete == null)
			{
				return 0;
			}

			employeeToDelete.IsDeleted = true;
			_context.SaveChanges();

			return employeeToDelete.Id;
		}

		private static EmployeeDto ConvertToDto(Employee employee)
		{
			if (employee == null)
			{
				return null;
			}

			return new EmployeeDto
			{
				Id = employee.Id,
				Birthdate = employee.Birthdate.ToString("yyyy-MM-dd"),
				FullName = employee.FullName,
				Tin = employee.TIN,
				TypeId = employee.EmployeeTypeId,
				ProfileImage = employee.ProfileImage
			};
		}
	}
}
