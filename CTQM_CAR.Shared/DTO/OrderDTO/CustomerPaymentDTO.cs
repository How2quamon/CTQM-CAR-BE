using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTQM_CAR.Shared.DTO.OrderDTO
{
    public class CustomerPaymentDTO
    {
        public Guid CustomerId { get; set; }

        public string? OrderStatus { get; set; }
    }
}
