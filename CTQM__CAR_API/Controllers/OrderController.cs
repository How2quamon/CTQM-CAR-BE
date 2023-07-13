
using CTQM_CAR.Service.Service.Interface;
using CTQM_CAR.Shared.DTO.CarDetailDTO;
using CTQM_CAR.Shared.DTO.OrderDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CTQM__CAR_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        //Add new order
        [HttpPost("NewOrder")]
        public async Task<ActionResult> AddOrder([FromBody] OrderDTO _order)
        {
            try
            {
                // Check the validation
                //var validator = new NewDetailValidator();
                //ValidationResult validaResult = await validator.ValidateAsync(newCarDetail);
                //if (!validaResult.IsValid)
                //return BadRequest(validaResult.Errors);

                // Create New Order
                //Random rnd = new Random();
                Guid id = Guid.NewGuid();
                var order = new OrderDTO
                {
                    OrderId = id,
                    CarId = _order.CarId,
                    OrderDate = _order.OrderDate,
                    OrderStatus = _order.OrderStatus,
                    Amount = _order.Amount,
                    TotalPrice = _order.TotalPrice,
                    CustomerId = _order.CustomerId,
                };

                // Add New Order
                await _orderService.AddOrder(order);
                return Ok(new
                {
                    message = "Add Order Success."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Select order by id
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> Get([FromRoute] Guid id)
        {
            // Check Exist
            bool isExist = await _orderService.FindOrderById(id);
            if (isExist)
            {
                // Get Order With ID
                var order = await _orderService.GetOrderById(id);

                // Check Get Order Success
                if (order == null)
                    return NotFound("Get Order Failed.");
                return Ok(order);
            }
            else
                return BadRequest("No Order Found.");
        }

        //Update order by id
        [HttpPut("UpdateOrder/{id}")]
        public async Task<ActionResult<OrderDTO>> UpdateOrder([FromRoute] Guid id, [FromBody] OrderDTO _orderDTO)
        {
            try
            {
                // Check validation
                //var validator = new UpdateCommentValidator();
                //ValidationResult validaResult = await validator.ValidateAsync(updateComment);
                //if (!validaResult.IsValid)
                //return BadRequest(validaResult.Errors);

                // Check Exist
                bool isExist = await _orderService.FindOrderById(id);
                if (isExist)
                {
                    //Create New OrderDTO
                    var orderData = new OrderDTO
                    {
                        OrderId = id,
                        CarId = _orderDTO.CarId,
                        OrderDate = _orderDTO.OrderDate,
                        OrderStatus = _orderDTO.OrderStatus,
                        Amount = _orderDTO.Amount,
                        TotalPrice = _orderDTO.TotalPrice,
                        CustomerId = _orderDTO.CustomerId,
                    };

                    //Update Order
                    await _orderService.UpdateOrder(orderData);
                    return Ok(new
                    {
                        message = "Update Order Success."
                    });
                }
                else
                    return NotFound("No Order Found.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Delete order by id
        [HttpDelete("DeleteOrder/{id}")]
        public async Task<ActionResult> DeleteOrder([FromRoute] Guid id)
        {
            try
            {
                // Check Exist
                bool isExist = await _orderService.FindOrderById(id);
                if (isExist)
                {
                    // Delete Order
                    await _orderService.DeleteOrder(id);

                    return Ok(new
                    {
                        message = "Delete Order Success."
                    });
                }
                else
                    return NotFound("No Order Found.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
