﻿using CTQM__CAR_API.CTQM_CAR.Domain;
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
	public class OrderRepository : Repository<Order>, IOrderRepository
	{
		public OrderRepository(MEC_DBContext context) : base(context) { }

		public MEC_DBContext MecDBContext
		{
			get { return _context as MEC_DBContext; }
		}

        public async Task<List<Order>> GetByCustomerId(Guid id)
        {
			return await MecDBContext.Orders
				.Where(x => x.CustomerId == id)
				.ToListAsync();
        }
    }
}
