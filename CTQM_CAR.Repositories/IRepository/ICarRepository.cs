using CTQM_CAR.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTQM_CAR.Repositories.IRepository
{
    public interface ICarRepository : IRepository<Car>
    {
        Task<List<Car>> GetByName(string carName);
        Task<List<Car>> GetByType(string carType);
    }
}
