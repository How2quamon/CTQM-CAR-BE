using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTQM_CAR.Shared.DTO.CarDetailDTO
{
	public class CarDetailDTO
	{
		public Guid DetailId { get; set; }

		public Guid CarId { get; set; }

		public string? Head1 { get; set; }

		public string? Title1 { get; set; }

		public string? Head2 { get; set; }

		public string? Title2 { get; set; }

		public string? Head3 { get; set; }

		public string? Title3 { get; set; }
	}
}
