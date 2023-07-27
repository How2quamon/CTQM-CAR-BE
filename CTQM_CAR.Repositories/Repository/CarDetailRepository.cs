
using CTQM__CAR_API.CTQM_CAR.Infrastructure;
using CTQM_CAR.Domain;
using CTQM_CAR.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTQM_CAR.Repositories.Repository
{
	public class CarDetailRepository : Repository<CarDetail>, ICarDetailRepository
	{
		public CarDetailRepository(MEC_DBContext context) : base(context) { }

		public MEC_DBContext MecDBContext
		{
			get { return _context as MEC_DBContext; }
		}

		public async Task<CarDetail> GetCarDetailWithCar(Guid carId)
		{
			return await MecDBContext.CarDetails
				.Where(c => c.CarId == carId)
				.FirstAsync();
		}
	}
}
