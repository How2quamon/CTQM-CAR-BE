using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTQM_CAR.Repositories.IRepository
{
	public interface IUnitOfWork : IDisposable
	{
		ICustomerRepository customersRepo { get; }
		ICarRepository carsRepo { get; }
		ICarDetailRepository carDetailsRepo { get; }
		ICartRepository cartsRepo { get; }
		IOrderRepository ordersRepo { get; }
		Task<int> SaveAsync();
	}
}
