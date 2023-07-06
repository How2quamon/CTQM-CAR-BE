using CTQM_CAR.Repositories.IRepository;
using CTQM_CAR.Service.Service.Interface;

namespace CTQM_CAR.Service.Service.Implement
{
	public class CustomerServiceImpl : ICustomerService
	{
		private readonly IUnitOfWork _unitOfWork;
		public CustomerServiceImpl(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
	}
}
