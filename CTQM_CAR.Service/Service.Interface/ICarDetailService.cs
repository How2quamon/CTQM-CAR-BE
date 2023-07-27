using CTQM_CAR.Shared.DTO.CarDetailDTO;

namespace CTQM_CAR.Service.Service.Interface
{
    public interface ICarDetailService
    {
        Task AddCarDetail(AddCarDetailDTO carDetailDTO);
        Task<List<CarDetailDTO>> GetAllCarDetail();
		Task<CarDetailDTO> GetCarDetailById(Guid id);
		Task<CarDetailDTO> GetCarDetailByCarId(Guid id);
        Task UpdateCarDetail(Guid carDetailId, UpdateCarDetailDTO carDetailDTO);
        Task DeleteCarDetail(Guid id);
		Task<bool> FindCarDetailById(Guid id);
		Task<bool> FindCarDetailByCarId(Guid id);
	}
}
