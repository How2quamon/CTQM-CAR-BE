using CTQM__CAR_API.CTQM_CAR.Domain;
using CTQM_CAR.Shared.DTO.CarDTO;

namespace CTQM_CAR.Service.Service.Interface
{
    public interface ICarService
    {
        Task AddCar(CarDTO car);
        Task DeleteCar(Guid id);
        Task<bool> FindCarById(Guid id);
        Task<List<CarDTO>> GetAllCars();
        Task<CarDTO> GetCarById(Guid id);
        Task UpdateCar(CarDTO carDTO);
    }
}
