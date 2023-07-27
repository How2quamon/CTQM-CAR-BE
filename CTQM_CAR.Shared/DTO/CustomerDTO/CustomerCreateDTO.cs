using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTQM_CAR.Shared.DTO.CustomerDTO
{
	public class CustomerCreateDTO
	{
		public string? CustomerName { get; set; }
		public string? CustomerEmail { get; set; }
		public string? CustomerPassword { get; set; }
	}
}
