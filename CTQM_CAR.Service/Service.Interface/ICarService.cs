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
        Task<List<CarDTO>> GetCarByName(string carName);
        Task<List<CarDTO>> GetCarByType(string? type);
        Task UpdateCar(CarDTO carDTO);
    }
}
