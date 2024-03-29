﻿using Azure.Core;
using CTQM_CAR.Service.Service.Interface;
using CTQM_CAR.Shared.DTO.CartDTO;
using CTQM_CAR_API.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Formats.Asn1;

namespace CTQM__CAR_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class CartController : ControllerBase
	{
		private readonly ICartService _cartService;

		public CartController(ICartService cartService)
		{
			_cartService = cartService;
		}

		// Get All Cart
		[HttpGet("GetCallCart")]
        [RequiresClaim(IdentityData.AdminCustomerClaimName, "true")]
        public async Task<ActionResult<List<CartDTO>>> GetAllCart()
		{
			return await _cartService.GetAllCart();
		}

        // Get Cart By Id
        [HttpGet("GetCart/{cartId}")]
        public async Task<CartDTO> GetCartById([FromRoute] Guid cartId)
        {
            return await _cartService.GetCartById(cartId);
        }

        // Get Customer Cart
        [HttpGet("GetCustomerCart/{customerId}")]
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
		public async Task<ActionResult<CartNotiDTO>> AddToCart([FromBody] AddCartDTO cartData)
		{
			// Check Validate

			// Add To Cart
			var result = await _cartService.AddToCart(cartData);
			if (result == null)
				return BadRequest("Add To Cart Failed.");
			return Ok(result);
		}
        
		// Quick add to Cart
        [HttpPost("QuickAddToCart")]
        public async Task<ActionResult<CartNotiDTO>> QuickAddToCart([FromBody] QuickAddCartDTO quickCart)
        {
            // Check Validate

            // Add To Cart
            var result = await _cartService.QuickAddToCart(quickCart);
            if (result == null)
                return BadRequest("Add To Cart Failed.");
            return Ok(result);
        }


        // Update Cart
        [HttpPut("UpdateCart")]
		public async Task<ActionResult<CartDTO>> UpdateCart([FromBody] UpdateCartDTO update)
		{
			// Check Validate
			//if (amount < 0)
			//{
			//	// Delete
			//	await DeleteCart(update.CartId);
			//	return Ok("Delete Cart Success.");
			//}
			// Update Cart
			var result = await _cartService.UpdateCustomerCart(update.CartId, update.Amount);
			if (result == null)
				return BadRequest("Update Cart Failed.");
			return Ok(result);
		}

		// Delete Cart
		[HttpDelete("DeleteCart/{cartId}")]
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
