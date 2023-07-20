using CTQM_CAR.Shared.DTO.OrderDTO;

namespace CTQM_CAR.Service.Service.Interface
{
    public interface IOrderService
    {
        Task AddOrder(OrderDTO _order);
        Task DeleteOrder(Guid id);
        Task<bool> FindOrderById(Guid id);
        Task<OrderDTO> GetOrderById(Guid id);
        Task UpdateOrder(OrderDTO _orderDTO);
        Task UpdateStatus(OrderDTO _orderData, bool _orderStatus);
    }
}
