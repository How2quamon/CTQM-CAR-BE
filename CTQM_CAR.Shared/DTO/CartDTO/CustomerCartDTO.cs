using CTQM_CAR.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTQM_CAR.Shared.DTO.CartDTO
{
	public class CustomerCartDTO
	{
		private List<CartDTO> customerCarts = new List<CartDTO>();
		public int? totalAmount { get; set; }
		public double? totalDiscount { get; set; }
		public void setCustomerCarts(List<Cart> cartsData)
		{
			int? tmpTotalAmount = 0;
			double? tmpTotalDiscount = 0;
			foreach(Cart cart in cartsData)
			{
				CartDTO tmpCart = new CartDTO
				{
					CartId = cart.CartId,
					CustomerId = cart.CustomerId,
					CarId = cart.CarId,
					Amount = cart.Amount,
					Price = cart.Price
				};
				tmpTotalAmount += tmpCart.Amount;
				tmpTotalDiscount += (tmpCart.Amount * tmpCart.Price);
				customerCarts.Add(tmpCart);
			}
		}

		public List<CartDTO> GetCustomerCarts()
		{
			return customerCarts;
		}
	}
}
