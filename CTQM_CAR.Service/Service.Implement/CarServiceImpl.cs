using CTQM_CAR.Domain;
using CTQM_CAR.Repositories.IRepository;
using CTQM_CAR.Service.Service.Interface;
using CTQM_CAR.Shared.DTO.CarDetailDTO;
using CTQM_CAR.Shared.DTO.CarDTO;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Runtime.Intrinsics.X86;

namespace CTQM_CAR.Service.Service.Implement
{
	public class CarServiceImpl : ICarService
	{
		private readonly IUnitOfWork _unitOfWork;
		public CarServiceImpl(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

        public async Task<List<CarDTO>> GetAllCars()
        {
            List<CarDTO> cars = new List<CarDTO>();
            foreach (var car in await _unitOfWork.carsRepo.GetAll())
            {
                CarDTO carDTO = new CarDTO();
                carDTO.CarId = car.CarId;
                carDTO.CarName = car.CarName;
                carDTO.CarModel = car.CarModel;
                carDTO.CarClass = car.CarClass;
                carDTO.CarEngine= car.CarEngine;
                carDTO.CarAmount = car.CarAmount;
                carDTO.CarPrice = car.CarPrice;
                carDTO.MoTa = car.MoTa;
                carDTO.Head1 = car.Head1;
                carDTO.MoTa2 = car.MoTa2;
                cars.Add(carDTO);
            }
            return cars;
        }

        public async Task<bool> FindCarById(Guid id)
        {
            var result = await _unitOfWork.carsRepo.GetById(id);
            if (result != null)
                return true;
            return false;
        }

        public async Task<CarDTO> GetCarById(Guid id)
        {
            var car = await _unitOfWork.carsRepo.GetById(id);
            if (car != null)
            {
                CarDTO carDTO = new CarDTO();
                carDTO.CarId = car.CarId;
                carDTO.CarName = car.CarName;
                carDTO.CarModel = car.CarModel;
                carDTO.CarClass = car.CarClass;
                carDTO.CarEngine = car.CarEngine;
                carDTO.CarAmount = car.CarAmount;
                carDTO.CarPrice = car.CarPrice;
                carDTO.MoTa = car.MoTa;
                carDTO.Head1 = car.Head1;
                carDTO.MoTa2 = car.MoTa2;
                return carDTO;
            }
            return null;
        }

        public async Task AddCar(CarDTO car)
        {
            Guid id = Guid.NewGuid();
            var carData = new Car
            {
                CarId = id,
                CarName = car.CarName,
                CarModel = car.CarModel,
                CarClass = car.CarClass,
                CarEngine = car.CarEngine,
                CarAmount = car.CarAmount,
                CarPrice = car.CarPrice,
                MoTa = car.MoTa,
                Head1 = car.Head1,
                MoTa2 = car.MoTa2,
            };
            await _unitOfWork.carsRepo.Add(carData);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteCar(Guid id)
        {
            await _unitOfWork.carsRepo.Delete(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task UpdateCar(CarDTO carDTO)
        {
            Car carContent = await _unitOfWork.carsRepo.GetById(carDTO.CarId);

            if (carContent != null)
            {
                if (!string.IsNullOrEmpty(carDTO.CarName))
                    carContent.CarName = carDTO.CarName;
                if (!string.IsNullOrEmpty(carDTO.CarModel))
                    carContent.CarModel = carDTO.CarModel;
                if (!string.IsNullOrEmpty(carDTO.CarClass))
                    carContent.CarClass = carDTO.CarClass;
                if (!string.IsNullOrEmpty(carDTO.CarEngine))
                    carContent.CarEngine = carDTO.CarEngine;
                if (carDTO.CarAmount!=null)
                    carContent.CarAmount = carDTO.CarAmount;
                if (carDTO.CarPrice!=null)
                    carContent.CarPrice = carDTO.CarPrice;
                if (!string.IsNullOrEmpty(carDTO.Head1))
                    carContent.Head1 = carDTO.Head1;
                if (!string.IsNullOrEmpty(carDTO.MoTa))
                    carContent.MoTa = carDTO.MoTa;
                if (!string.IsNullOrEmpty(carDTO.MoTa2))
                    carContent.MoTa2 = carDTO.MoTa2;


                _unitOfWork.carsRepo.Update(carContent);
                await _unitOfWork.SaveAsync();
            }
        }

        public async Task<List<CarDTO>> GetCarByName(string carName)
        {
            //return await _unitOfWork.carsRepo.GetByName(carName);
            List<CarDTO> cars = new List<CarDTO>();
            foreach (var car in await _unitOfWork.carsRepo.GetByName(carName))
            {
                CarDTO carDTO = new CarDTO();
                carDTO.CarId = car.CarId;
                carDTO.CarName = car.CarName;
                carDTO.CarModel = car.CarModel;
                carDTO.CarClass = car.CarClass;
                carDTO.CarEngine = car.CarEngine;
                carDTO.CarAmount = car.CarAmount;
                carDTO.CarPrice = car.CarPrice;
                carDTO.MoTa = car.MoTa;
                carDTO.Head1 = car.Head1;
                carDTO.MoTa2 = car.MoTa2;
                cars.Add(carDTO);
            }
            return cars;
        }

        public async Task<List<CarDTO>> GetCarByType(string carType)
        {
            //return await _unitOfWork.carsRepo.GetByName(carName);
            List<CarDTO> cars = new List<CarDTO>();
            foreach (var car in await _unitOfWork.carsRepo.GetByType(carType))
            {
                CarDTO carDTO = new CarDTO();
                carDTO.CarId = car.CarId;
                carDTO.CarName = car.CarName;
                carDTO.CarModel = car.CarModel;
                carDTO.CarClass = car.CarClass;
                carDTO.CarEngine = car.CarEngine;
                carDTO.CarAmount = car.CarAmount;
                carDTO.CarPrice = car.CarPrice;
                carDTO.MoTa = car.MoTa;
                carDTO.Head1 = car.Head1;
                carDTO.MoTa2 = car.MoTa2;
                cars.Add(carDTO);
            }
            return cars;
        }
    }
}
