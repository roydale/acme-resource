using Acme.Resource.Business.DataTransferObjects;

namespace Acme.Resource.Business.Interfaces
{
	public interface ISalaryServiceFactory
	{
		ISalaryService Create(CalculateSalaryDto payload);
	}
}
