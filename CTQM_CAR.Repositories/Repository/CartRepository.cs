
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
	public class CartRepository : Repository<Cart>, ICartRepository
	{
		public CartRepository(MEC_DBContext context) : base(context) { }

		public MEC_DBContext MecDBContext
		{
			get { return _context as MEC_DBContext; }
		}

		public async Task<List<Cart>> GetCustomerCart(Guid customerId)
		{
			return await MecDBContext.Carts
				.Where(c => c.CustomerId == customerId)
				.ToListAsync();
		}

		public async Task<bool> DeleteCustomerCart(Guid customerId)
		{
			try
			{
				List<Cart> customerCarts = await MecDBContext.Carts
											.Where(c => c.CustomerId == customerId)
											.ToListAsync();

				MecDBContext.Carts.RemoveRange(customerCarts);
				return true;				
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex);
				return false;
			}
		}
	}
}
