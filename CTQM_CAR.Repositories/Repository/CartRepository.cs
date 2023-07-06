using CTQM__CAR_API.CTQM_CAR.Domain;
using CTQM__CAR_API.CTQM_CAR.Infrastructure;
using CTQM_CAR.Repositories.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTQM_CAR.Repositories.Repository
{
	public class CartRepository : Repository<Cart>, ICartRepository
	{
		public CartRepository(MEC_DBContext context) : base(context) { }

		public MEC_DBContext MecDBContext
		{
			get { return _context as MEC_DBContext; }
		}
	}
}
