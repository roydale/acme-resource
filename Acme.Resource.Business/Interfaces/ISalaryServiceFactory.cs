using Acme.Resource.Common.DataTransferObjects;

namespace Acme.Resource.Business.Interfaces
{
	public interface ISalaryServiceFactory
	{
		ISalaryService Create(CalculateSalaryDto payload);
	}
}
