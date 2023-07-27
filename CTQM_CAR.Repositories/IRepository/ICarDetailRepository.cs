using CTQM_CAR.Domain;
using CTQM_CAR.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTQM_CAR.Repositories.IRepository
{
    public interface ICarDetailRepository : IRepository<CarDetail>
    {
        Task<CarDetail> GetCarDetailWithCar(Guid carId);
    }
}
