using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTQM_CAR.Shared.DTO.CustomerDTO
{
	public class CustomerLoginDTO
	{
		public string Email { get; set; }
		public string Password { get; set; }
		public string? Token { get; set; } = "";
		public bool? admin { get; set; } = false;
	}
}
