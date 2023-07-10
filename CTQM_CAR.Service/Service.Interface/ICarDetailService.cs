using CTQM_CAR.Shared.DTO.CarDetailDTO;

namespace CTQM_CAR.Service.Service.Interface
{
    public interface ICarDetailService
    {
        Task AddCarDetail(CarDetailDTO carDetailDTO);
        Task<CarDetailDTO> GetCarDetailById(Guid id);
        Task UpdateCarDetail(CarDetailDTO carDetailDTO);
        Task DeleteCarDetail(Guid id);
        Task<bool> FindCarDetailById(Guid id);
    }
}
