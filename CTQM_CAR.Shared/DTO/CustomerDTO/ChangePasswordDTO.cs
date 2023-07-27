using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTQM_CAR.Shared.DTO.CustomerDTO
{
	public class ChangePasswordDTO
	{
		public string? OldPassword { get; set; }
		public string? NewPassword { get; set; }
	}
}
