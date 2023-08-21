using CTQM_CAR.Shared.DTO.CarDTO;

namespace CTQM_CAR.Service.Service.Interface
{
    public interface ICarService
    {
        Task AddCar(AddCarDTO car);
        Task DeleteCar(Guid id);
        Task<bool> FindCarById(Guid id);
        Task<List<CarDTO>> GetAllCars();
        Task<CarDTO> GetCarById(Guid id);
        Task<CarDTO> GetCarByName(string carName);
        Task<List<CarDTO>> GetCarByModel(string? carModel);
        Task<List<CarDTO>> SearchCars(string search);
        Task UpdateCar(Guid carId, AddCarDTO carDTO);
    }
}
