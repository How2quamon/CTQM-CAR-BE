using CTQM_CAR.Shared.DTO.OrderDTO;

namespace CTQM_CAR.Service.Service.Interface
{
    public interface IOrderService
    {
        Task<List<OrderDTO>> GetAllOrder();
        Task AddOrder(AddOrderDTO _order);
        Task<bool> CustomerPayment(CustomerPaymentDTO payment);
        Task DeleteOrder(Guid id);
        Task<bool> FindOrderById(Guid id);
        Task<OrderDTO> GetOrderById(Guid id);
        Task<List<CustomerOrderDTO>> GetOrderByCustomerId(Guid id);
        Task UpdateOrder(Guid orderId, UpdateOrderDTO _orderDTO);
        Task UpdateStatus(Guid orderId, bool _orderStatus);
    }
}
