using CTQM_CAR.Shared.DTO.CartDTO;

namespace CTQM_CAR.Service.Service.Interface
{
	public interface ICartService
	{
		Task<List<CartDTO>> GetAllCart();
		Task<CustomerCartDTO> GetCustomerCart(Guid customerId);
		Task<CartNotiDTO> AddToCart(AddCartDTO cartData); 
		Task<CartDTO> UpdateCustomerCart(Guid cartId, int amount);
		Task<bool> DeleteCart(Guid cartId);
		Task<bool> DeleteCustomerCart(Guid customerId);
        Task<CartDTO> GetCartById(Guid cartId);
		Task<string> CheckCustomerCart(Guid customerId, Guid carId);
    }
}
