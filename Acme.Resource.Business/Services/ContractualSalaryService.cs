using Acme.Resource.Business.DataTransferObjects;
using Acme.Resource.Business.Interfaces;
using Acme.Resource.Common.Enums;

namespace Acme.Resource.Business.Services
{
	public class ContractualSalaryService : IContractualSalaryService
	{
		private const decimal PerDayRate = 500.00M;

		public EmployeeType EmployeeType => EmployeeType.Contractual;

		public CalculateSalaryDto SalaryDependency { get; set; }

		public decimal Compute()
		{
			var salary = PerDayRate * SalaryDependency.WorkedDays;
			return Math.Round(salary, 2);
		}
	}
}
