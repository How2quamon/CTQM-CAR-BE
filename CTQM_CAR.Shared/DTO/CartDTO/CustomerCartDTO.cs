using CTQM__CAR_API.CTQM_CAR.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTQM_CAR.Shared.DTO.CartDTO
{
	public class CustomerCartDTO
	{
		public List<CartDetailDTO> customerCarts { get; set; } =  new List<CartDetailDTO>();
		public int? totalAmount { get; set; }
		public double? totalDiscount { get; set; }
	}
}
