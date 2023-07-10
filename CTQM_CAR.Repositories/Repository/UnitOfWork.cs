using CTQM__CAR_API.CTQM_CAR.Infrastructure;
using CTQM_CAR.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTQM_CAR.Repositories.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly MEC_DBContext _context;

		public ICustomerRepository customersRepo { get; private set; }
		public ICarRepository carsRepo { get; private set; }
		public ICarDetailRepository carDetailsRepo { get; private set; }
		public ICartRepository cartsRepo { get; private set; }
		public IOrderRepository ordersRepo { get; private set; }

		public UnitOfWork(MEC_DBContext context)
		{
			_context = context;
			customersRepo = new CustomerRepository(_context);
			carsRepo = new CarRepository(_context);
			carDetailsRepo = new CarDetailRepository(_context);
			cartsRepo = new CartRepository(_context);
			ordersRepo = new OrderRepository(_context);
		}

		public async Task<int> SaveAsync()
		{
			try
			{
				return await _context.SaveChangesAsync();
			}
			catch (Exception ecx)
			{
				Console.WriteLine(ecx);
				return 0;
			}
		}

		public async void Dispose()
		{
			await _context.DisposeAsync();
		}
	}
}
