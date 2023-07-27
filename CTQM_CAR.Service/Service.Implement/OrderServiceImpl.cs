﻿using CTQM_CAR.Domain;
using CTQM_CAR.Repositories.IRepository;
using CTQM_CAR.Service.Service.Interface;
using CTQM_CAR.Shared.DTO.CarDetailDTO;
using CTQM_CAR.Shared.DTO.OrderDTO;

namespace CTQM_CAR.Service.Service.Implement
{
    public class OrderServiceImpl : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderServiceImpl(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


		public async Task<List<OrderDTO>> GetAllOrder()
        {
			List<OrderDTO> orderList = new List<OrderDTO>();
			foreach (var order in await _unitOfWork.ordersRepo.GetAll())
			{
				OrderDTO orderDTO = new OrderDTO();
                orderDTO.OrderId = order.OrderId;
			    orderDTO.CarId = order.CarId;
			    orderDTO.OrderDate = order.OrderDate;
			    orderDTO.OrderStatus = order.OrderStatus;
			    orderDTO.Amount = order.Amount;
			    orderDTO.TotalPrice = order.TotalPrice;
			    orderDTO.CustomerId = order.CustomerId;
                orderList.Add(orderDTO);
			}
			return orderList;
		}

		public async Task AddOrder(AddOrderDTO _orderDTO)
        {
            Guid id = Guid.NewGuid();
            var orderData = new Order
            {
                OrderId = id,
                CarId = _orderDTO.CarId,
                OrderDate = _orderDTO.OrderDate,
                OrderStatus = _orderDTO.OrderStatus,
                Amount = _orderDTO.Amount,
                TotalPrice = _orderDTO.TotalPrice,
                CustomerId = _orderDTO.CustomerId,
            };
            await _unitOfWork.ordersRepo.Add(orderData);
            await _unitOfWork.SaveAsync();

        }

        public async Task DeleteOrder(Guid id)
        {
            await _unitOfWork.ordersRepo.Delete(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<bool> FindOrderById(Guid id)
        {
            var result = await _unitOfWork.ordersRepo.GetById(id);
            if (result != null)
                return true;
            return false;
        }

        public async Task<OrderDTO> GetOrderById(Guid id)
        {
            var order = await _unitOfWork.ordersRepo.GetById(id);
            if (order != null)
            {
                OrderDTO orderDTO = new OrderDTO();
                orderDTO.OrderId = order.OrderId;
                orderDTO.CarId = order.CarId;
                orderDTO.OrderDate = order.OrderDate;
                orderDTO.OrderStatus = order.OrderStatus;
                orderDTO.Amount = order.Amount;
                orderDTO.TotalPrice = order.TotalPrice;
                orderDTO.CustomerId = order.CustomerId;

                return orderDTO;
            }
            return null;
        }

        public async Task UpdateOrder(Guid orderId, UpdateOrderDTO _orderDTO)
        {
            Order orderContent = await _unitOfWork.ordersRepo.GetById(orderId);

            if (orderContent != null)
            {
                if (_orderDTO.OrderDate != null)
                    orderContent.OrderDate = _orderDTO.OrderDate;
                if (!string.IsNullOrEmpty(_orderDTO.OrderStatus))
                    orderContent.OrderStatus = _orderDTO.OrderStatus;
                if (_orderDTO.Amount != null)
                    orderContent.Amount = _orderDTO.Amount;
                if (_orderDTO.TotalPrice != null)
                    orderContent.TotalPrice = _orderDTO.TotalPrice;

                _unitOfWork.ordersRepo.Update(orderContent);
                await _unitOfWork.SaveAsync();
            }
        }

        public async Task UpdateStatus(Guid orderId, bool _status)
        {
            Order orderContent = await _unitOfWork.ordersRepo.GetById(orderId);

            if (orderContent != null)
            {

                if (_status)
                {
                    orderContent.OrderStatus = "PayPal";
                }
                else orderContent.OrderStatus = "COD";
                
                _unitOfWork.ordersRepo.Update(orderContent);
                await _unitOfWork.SaveAsync();
            }
        }
    }
}