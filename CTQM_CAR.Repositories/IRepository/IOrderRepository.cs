﻿
using CTQM__CAR_API.CTQM_CAR.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTQM_CAR.Repositories.IRepository
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<List<Order>> GetByCustomerId(Guid id);
    }
}
