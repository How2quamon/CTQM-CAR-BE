using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTQM_CAR.Shared.DTO.OrderDTO
{
    public class CustomerOrderDTO
    {
        public Guid OrderId { get; set; }

        public Guid CarId { get; set; }

        public string CarName { get; set; }

        public string CarModel { get; set; }

        public string CarClass { get; set; }
        
        public string CarEngine { get; set; }

        public Guid CustomerId { get; set; }

        public DateTime OrderDate { get; set; }

        public string? OrderStatus { get; set; }

        public int Amount { get; set; }

        public double TotalPrice { get; set; }

        public string? Image1 { get; set; }
    }
}
