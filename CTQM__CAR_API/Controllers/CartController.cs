using Azure.Core;
using CTQM_CAR.Service.Service.Interface;
using CTQM_CAR.Shared.DTO.CartDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Formats.Asn1;

namespace CTQM__CAR_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CartController : ControllerBase
	{
		private readonly ICartService _cartService;

		public CartController(ICartService cartService)
		{
			_cartService = cartService;
		}

		// Get All Cart
		[HttpGet("GetCallCart")]
		public async Task<ActionResult<List<CartDTO>>> GetAllCart()
		{
			return await _cartService.GetAllCart();
		}

        // Get Cart By Id
        [HttpGet("GetCart/{_cartId}")]
        public async Task<CartDTO> GetCartById([FromRoute] Guid _cartId)
        {
            return await _cartService.GetCartById(_cartId);
        }

        // Get Customer Cart
        [HttpGet("GetCustomerCart/{id}")]
		public async Task<ActionResult<CustomerCartDTO>> GetCustomerCart([FromRoute] Guid customerId)
		{
			var data = await _cartService.GetCustomerCart(customerId);
			if (data == null)
			{
				return BadRequest("Get Customer Cart Failed.");
			}
			return Ok(data);
		}

		// Add to Cart
		[HttpPost("AddToCart")]
		public async Task<ActionResult<CartNotiDTO>> AddToCart([FromBody] CartDTO cartData)
		{
			// Check Validate

			// Add To Cart
			var result = await _cartService.AddToCart(cartData);
			if (result == null)
				return BadRequest("Add To Cart Failed.");
			return Ok(result);
		}

		// Update Cart
		[HttpPut("UpdateCart/{id}")]
		public async Task<ActionResult<CartDTO>> UpdateCart([FromRoute] Guid cartId, [FromBody] int amount)
		{
			// Check Validate

			// Update Cart
			var result = await _cartService.UpdateCustomerCart(cartId, amount);
			if (result == null)
				return BadRequest("Update Cart Failed.");
			return Ok(result);
		}

		// Delete Cart
		[HttpDelete("DeleteCart/{id}")]
		public async Task<ActionResult> DeleteCart([FromRoute] Guid cartId)
		{
			// Delete Cart
			var result = await _cartService.DeleteCart(cartId);
			if (!result)
			{
				return BadRequest("Delete Cart Failed.");
			}
			return Ok("Delete Cart Success.");
		}
	}
}
