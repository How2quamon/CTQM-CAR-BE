﻿using CTQM_CAR.Domain;
using CTQM_CAR.Repositories.IRepository;
using CTQM_CAR.Service.Service.Interface;
using CTQM_CAR.Shared.DTO.CartDTO;
using System.Net.Http.Headers;


namespace CTQM_CAR.Service.Service.Implement
{
	public class CartServiceImpl : ICartService
	{
		private readonly IUnitOfWork _unitOfWork;
		public CartServiceImpl(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<CartNotiDTO> AddToCart(AddCartDTO cartData)
		{
			try
			{
				// Check Cart exist
				string check = await CheckCustomerCart(cartData.CustomerId, cartData.CarId);
				if (check != null)
				{
					Guid cartId = Guid.Parse(check);
					await UpdateCustomerCart(cartId, 1);
				}
				else
				{
					Guid newGuid = Guid.NewGuid();
					Cart cartAdding = new Cart
					{
						CartId = newGuid,
						CustomerId = cartData.CustomerId,
						CarId = cartData.CarId,
						Amount = (int)cartData.Amount,
						Price = (double)cartData.Price
					};
					await _unitOfWork.cartsRepo.Add(cartAdding);
				}
				var carData = await _unitOfWork.carsRepo.GetById(cartData.CarId);

				CartNotiDTO cartResult = new CartNotiDTO
				{
					CarName = carData.CarName,
					Amount = (int) cartData.Amount,
					Price = (double) cartData.Price
				};

				await _unitOfWork.SaveAsync();
				return cartResult;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return null;
			}
		}

		public async Task<bool> DeleteCart(Guid cartId)
		{
			try
			{
				await _unitOfWork.cartsRepo.Delete(cartId);
				await _unitOfWork.SaveAsync();
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return false;
			}
		}

		public async Task<bool> DeleteCustomerCart(Guid customerId)
		{
			try
			{
				await _unitOfWork.cartsRepo.DeleteCustomerCart(customerId);
				await _unitOfWork.SaveAsync();
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return false;
			}
		}

		public async Task<List<CartDTO>> GetAllCart()
		{
			List<Cart> cartData = await _unitOfWork.cartsRepo.GetAll();
			List<CartDTO> cartResult = new List<CartDTO>();
			foreach (var cart in cartData)
			{
				CartDTO tmpCart = new CartDTO
				{
					CartId = cart.CartId,
					CustomerId = cart.CustomerId,
					CarId = cart.CarId,
					Amount = cart.Amount,
					Price = cart.Price
				};
				cartResult.Add(tmpCart);
			}
			return cartResult;
		}

        public async Task<CartDTO> GetCartById(Guid _cartId)
        {
			try
			{
				Cart cartData = await _unitOfWork.cartsRepo.GetById(_cartId);
				CartDTO cart = new CartDTO();
				cart.CartId = cartData.CartId;
				cart.CarId = cartData.CarId;
				cart.Amount = cartData.Amount;
				cart.Price = cartData.Price;
				return cart;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return null;
			}
        }

        public async Task<CustomerCartDTO> GetCustomerCart(Guid customerId)
		{
			try
			{
				List<Cart> cartData = await _unitOfWork.cartsRepo.GetCustomerCart(customerId);
				CustomerCartDTO customerResult = new CustomerCartDTO();
				customerResult.setCustomerCarts(cartData);
				return customerResult;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return null;
			}
		}

		public async Task<CartDTO> UpdateCustomerCart(Guid cartId, int amount)
		{
			try
			{
				Cart cartData = await _unitOfWork.cartsRepo.GetById(cartId);
				cartData.Amount = amount;
				CartDTO cartResult = new CartDTO
				{
					CartId = cartData.CartId,
					CustomerId = cartData.CustomerId,
					CarId = cartData.CarId,
					Amount = cartData.Amount,
					Price = cartData.Price
				};
				_unitOfWork.cartsRepo.Update(cartData);
				await _unitOfWork.SaveAsync();
				return cartResult;
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex);
				return null;
			}
		}

		public async Task<string> CheckCustomerCart(Guid customerId, Guid carId)
		{
			try
			{
				return await _unitOfWork.cartsRepo.GetCustomerCartWithCar(customerId, carId);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return null;
			}
		}
	}
}
