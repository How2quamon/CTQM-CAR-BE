using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTQM_CAR.Shared.DTO.CustomerDTO
{
	public class CustomerDTO
	{
		public Guid CustomerId { get; set; }

		public string? CustomerName { get; set; }

		public string? CustomerPhone { get; set; }

		public string? CustomerAddress { get; set; }

		public DateTime CustomerDate { get; set; }

		public string? CustomerLicense { get; set; }

		public string? CustomerEmail { get; set; }

		public string? CustomerPassword { get; set; }
	}
}
