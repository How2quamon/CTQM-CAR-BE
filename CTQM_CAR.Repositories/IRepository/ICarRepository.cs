using CTQM__CAR_API.CTQM_CAR.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTQM_CAR.Repositories.IRepository
{
    public interface ICarRepository : IRepository<Car>
    {
        Task<Car> GetByName(string carName);
        Task<List<Car>> GetByModel(string carModel);
        Task<List<Car>> SearchCars(string search);
    }
}
