using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTQM_CAR.Shared.DTO.OrderDTO
{
	public class AddOrderDTO
	{
		public Guid CarId { get; set; }

		public Guid CustomerId { get; set; }

		public DateTime OrderDate { get; set; }

		public string? OrderStatus { get; set; }

		public int Amount { get; set; }

		public double TotalPrice { get; set; }
	}
}
