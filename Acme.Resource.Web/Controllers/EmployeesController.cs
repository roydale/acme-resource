using Acme.Resource.Business.Interfaces;
using Acme.Resource.Common.DataTransferObjects;
using Acme.Resource.Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Acme.Resource.Web.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class EmployeesController(ISalaryServiceFactory _serviceFactory
									, IEmployeeService _service
									, IFakerService _fakerService) : ControllerBase
	{
		/// <summary>
		/// Refactor this method to go through proper layers and fetch from the DB.
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var result = await _service.RetrieveEmployeesAsync();
			return Ok(result);
		}

		/// <summary>
		/// Refactor this method to go through proper layers and fetch from the DB.
		/// </summary>
		/// <returns></returns>
		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var result = await _service.RetrieveEmployeeByIdAsync(id);
			return Ok(result);
		}

		/// <summary>
		/// Refactor this method to go through proper layers and update changes to the DB.
		/// </summary>
		/// <returns></returns>
		[HttpPut("{id}")]
		public async Task<IActionResult> Put(EditEmployeeDto input)
		{
			var item = await _service.UpdateEmployeeAsync(input);
			if (item == null)
				return NotFound();
			return Ok(item);
		}

		/// <summary>
		/// Refactor this method to go through proper layers and insert employees to the DB.
		/// </summary>
		/// <returns></returns>
		[HttpPost]
		public async Task<IActionResult> Post(CreateEmployeeDto input)
		{
			var id = await _service.CreateEmployeeAsync(input);
			return Created($"/api/employees/{id}", id);
		}

		/// <summary>
		/// Refactor this method to go through proper layers and perform soft deletion of an employee to the DB.
		/// </summary>
		/// <returns></returns>
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var result = await _service.DeleteEmployeeAsync(id);
			if (result == 0)
				return NotFound();
			return Ok(id);
		}

		/// <summary>
		/// Refactor this method to go through proper layers and use Factory pattern
		/// </summary>
		/// <param name="id"></param>
		/// <param name="absentDays"></param>
		/// <param name="workedDays"></param>
		/// <returns></returns>
		[HttpPost("{id}/calculate")]
		public async Task<IActionResult> Calculate([FromBody] CalculateSalaryDto payload)
		{
			var result = await _service.RetrieveEmployeeByIdAsync(payload.Id);
			if (result == null)
				return NotFound();

			payload.EmployeeType = (EmployeeType)result.TypeId;
			var service = _serviceFactory.Create(payload);
			var salary = service.Compute();
			return Ok(salary);
		}

		[HttpGet("generate")]
		public IActionResult Generate()
		{
			var fakeEmployeeData = _fakerService.GenerateRandomEmployee();
			return Ok(fakeEmployeeData);
		}

		[HttpGet("refresh-image")]
		public IActionResult GetProfileImage()
		{
			var fakeProfileImage = _fakerService.GenerateProfileImage();
			return Ok(new { profileImage = fakeProfileImage });
		}
	}
}
