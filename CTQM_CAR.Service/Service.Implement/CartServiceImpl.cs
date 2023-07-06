using CTQM_CAR.Repositories.IRepository;
using CTQM_CAR.Service.Service.Interface;

namespace CTQM_CAR.Service.Service.Implement
{
	public class CartServiceImpl : ICartService
	{
		private readonly IUnitOfWork _unitOfWork;
		public CartServiceImpl(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
	}
}
