using CTQM_CAR.Repositories.IRepository;
using CTQM_CAR.Service.Service.Interface;

namespace CTQM_CAR.Service.Service.Implement
{
	public class OrderServiceImpl : IOrderService
	{
		private readonly IUnitOfWork _unitOfWork;
		public OrderServiceImpl(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
	}
}
