using CTQM_CAR.Repositories.IRepository;
using CTQM_CAR.Service.Service.Interface;

namespace CTQM_CAR.Service.Service.Implement
{
	public class CarServiceImpl : ICarService
	{
		private readonly IUnitOfWork _unitOfWork;
		public CarServiceImpl(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
	}
}
