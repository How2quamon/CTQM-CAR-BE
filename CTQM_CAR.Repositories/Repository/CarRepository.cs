
using CTQM__CAR_API.CTQM_CAR.Infrastructure;
using CTQM__CAR_API.CTQM_CAR.Domain;
using CTQM_CAR.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTQM_CAR.Repositories.Repository
{
	public class CarRepository : Repository<Car>, ICarRepository

	{
        
        public CarRepository(MEC_DBContext context) : base(context) { }

		public MEC_DBContext MecDBContext
		{
			get { return _context as MEC_DBContext; }
		}

        public async Task<Car> GetByName(string carName)
        {
            return await MecDBContext.Cars
                .Where(c => c.CarName.Contains(carName))
                .FirstOrDefaultAsync();
        }

        public async Task<List<Car>> GetByModel(string carModel)
        {
            return await MecDBContext.Cars
                .Where(c => c.CarModel.Contains(carModel))
                //.FirstOrDefaultAsync()
                .ToListAsync();
        }

        public async Task<List<Car>> SearchCars(string search)
        {
            var result = await MecDBContext.Cars
                .Where(c => c.CarModel.Contains(search))
                .ToListAsync();
            if (result.Count == 0)
            {
                result = await MecDBContext.Cars
                .Where(c => c.CarName.Contains(search))
                .ToListAsync();
            }
            return result;
        }
    }
}
