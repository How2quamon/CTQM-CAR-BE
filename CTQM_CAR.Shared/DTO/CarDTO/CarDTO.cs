using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTQM_CAR.Shared.DTO.CarDTO
{
	public class CarDTO
	{
		public Guid CarId { get; set; }

		public string? CarName { get; set; }

		public string? CarModel { get; set; }

		public string? CarClass { get; set; }

		public string? CarEngine { get; set; }

		public int CarAmount { get; set; }

		public double CarPrice { get; set; }

		public string? MoTa { get; set; }

		public string? Head1 { get; set; }

		public string? MoTa2 { get; set; }
	}
}
