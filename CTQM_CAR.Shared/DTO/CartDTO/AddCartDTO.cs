using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTQM_CAR.Shared.DTO.CartDTO
{
	public class AddCartDTO
	{
		public Guid CustomerId { get; set; }

		public Guid CarId { get; set; }

		public int? Amount { get; set; }

		public double? Price { get; set; }
	}
}
