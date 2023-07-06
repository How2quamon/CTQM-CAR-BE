using CTQM_CAR.Repositories.IRepository;
using CTQM_CAR.Service.Service.Interface;

namespace CTQM_CAR.Service.Service.Implement
{
	public class CarDetailServiceImpl : ICarDetailService
	{
		private readonly IUnitOfWork _unitOfWork;
		public CarDetailServiceImpl(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
	}
}
