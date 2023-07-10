using CTQM__CAR_API.CTQM_CAR.Domain;
using CTQM_CAR.Repositories.IRepository;
using CTQM_CAR.Service.Service.Interface;
using CTQM_CAR.Shared.DTO.CarDetailDTO;
using CTQM_CAR.Shared.DTO.CarDTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CTQM_CAR.Service.Service.Implement
{
	public class CarDetailServiceImpl : ICarDetailService
	{
		private readonly IUnitOfWork _unitOfWork;
		public CarDetailServiceImpl(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

        public async Task AddCarDetail(CarDetailDTO carDetailDTO)
        {
            var carDetailData = new CarDetail
            {
                DetailId = carDetailDTO.DetailId,
                CarId = carDetailDTO.CarId,
                Head1 = carDetailDTO.Head1,
                Head2 = carDetailDTO.Head2,
                Head3 = carDetailDTO.Head3,
                Title1 = carDetailDTO.Title1,
                Title2 = carDetailDTO.Title2,
                Title3 = carDetailDTO.Title3,
            };
            await _unitOfWork.carDetailsRepo.Add(carDetailData);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteCarDetail(Guid id)
        {
            await _unitOfWork.carDetailsRepo.Delete(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<bool> FindCarDetailById(Guid id)
        {
            var result = await _unitOfWork.carDetailsRepo.GetById(id);
            if (result != null)
                return true;
            return false;
        }

        public async Task<CarDetailDTO> GetCarDetailById(Guid id)
        {
            var carDetail = await _unitOfWork.carDetailsRepo.GetById(id);
            if (carDetail != null)
            {
                CarDetailDTO carDetailDTO = new CarDetailDTO();
                carDetailDTO.DetailId = carDetail.DetailId;
                carDetailDTO.CarId = carDetail.CarId;
                carDetailDTO.Head1 = carDetail.Head1;
                carDetailDTO.Title1 = carDetail.Title1;
                carDetailDTO.Head2 = carDetail.Head2;
                carDetailDTO.Title2 = carDetail.Title2;
                carDetailDTO.Head3 = carDetail.Head3;
                carDetailDTO.Title3 = carDetail.Title3;

                return carDetailDTO;
            }
            return null;
        }

        public async Task UpdateCarDetail(CarDetailDTO carDetailDTO)
        {
            
                CarDetail carDetailContent = await _unitOfWork.carDetailsRepo.GetById(carDetailDTO.DetailId);

                if (carDetailContent != null)
                {
                    if (carDetailDTO.CarId == null || carDetailDTO.CarId == Guid.Empty)
                        carDetailContent.CarId = carDetailDTO.CarId;
                    if (!string.IsNullOrEmpty(carDetailDTO.Head1))
                        carDetailContent.Head1 = carDetailDTO.Head1;
                    if (!string.IsNullOrEmpty(carDetailDTO.Head1))
                        carDetailContent.Head1 = carDetailDTO.Head1;
                    if (!string.IsNullOrEmpty(carDetailDTO.Head2))
                        carDetailContent.Head2 = carDetailDTO.Head2;
                    if (!string.IsNullOrEmpty(carDetailDTO.Head1))
                        carDetailContent.Head3 = carDetailDTO.Head3;
                    if (!string.IsNullOrEmpty(carDetailDTO.Title1))
                        carDetailContent.Title1 = carDetailDTO.Title1;
                    if (!string.IsNullOrEmpty(carDetailDTO.Title2))
                        carDetailContent.Title2 = carDetailDTO.Title2;
                    if (!string.IsNullOrEmpty(carDetailDTO.Title3))
                        carDetailContent.Title3 = carDetailDTO.Title3;

                    await _unitOfWork.carDetailsRepo.Update(carDetailContent);
                    await _unitOfWork.SaveAsync();
                }                
        }
    }
}
