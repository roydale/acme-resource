using Acme.Resource.Business.Interfaces;
using Acme.Resource.Common.DataTransferObjects;
using Acme.Resource.Common.Enums;

namespace Acme.Resource.Business.Services
{
	public class RegularSalaryService : IRegularSalaryService
	{
		private const decimal BasicMonthlySalary = 20000M;
		private const decimal Tax = 0.12M;

		public EmployeeType EmployeeType => EmployeeType.Regular;

		public CalculateSalaryDto SalaryDependency { get; set; }

		public decimal Compute()
		{
			var ratePerDay = BasicMonthlySalary / 22;

			var absenceDeduction = ratePerDay * SalaryDependency.AbsentDays;
			var taxDeduction = BasicMonthlySalary * Tax;
			//var grossSalary = BasicMonthlySalary - absenceDeduction;
			//var taxDeduction = grossSalary * Tax;

			var salary = BasicMonthlySalary - (absenceDeduction + taxDeduction);
			//var salary = grossSalary - taxDeduction;
			return Math.Round(salary, 2);
		}
	}
}
