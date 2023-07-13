using CTQM_CAR.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTQM_CAR.Repositories.IRepository
{
	public interface ICartRepository : IRepository<Cart>
	{
		Task<List<Cart>> GetCustomerCart(Guid customerId);
		Task<bool> DeleteCustomerCart(Guid customerId);
    }
}
