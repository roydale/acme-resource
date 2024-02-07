using Acme.Resource.Business.Interfaces;
using Acme.Resource.Common.DataTransferObjects;

namespace Acme.Resource.Business.ServiceCreators
{
	public class SalaryServiceFactory : ISalaryServiceFactory
	{
		private readonly IEnumerable<ISalaryService> _services;

		public SalaryServiceFactory(IEnumerable<ISalaryService> services)
		{
			_services = services;
		}

		public ISalaryService Create(CalculateSalaryDto payload)
		{
			var service = _services.FirstOrDefault(service => service.EmployeeType == payload.EmployeeType);
			service.SalaryDependency = payload;
			return service;
		}
	}
}
