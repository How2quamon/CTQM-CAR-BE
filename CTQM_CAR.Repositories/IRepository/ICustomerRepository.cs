﻿using CTQM__CAR_API.CTQM_CAR.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTQM_CAR.Repositories.IRepository
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer> GetCustomerWithEmail(string email);

	}
}
