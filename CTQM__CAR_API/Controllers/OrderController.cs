
using CTQM_CAR.Service.Service.Interface;
using CTQM_CAR.Shared.DTO.CarDetailDTO;
using CTQM_CAR.Shared.DTO.CarDTO;
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


		[HttpGet]
		public async Task<ActionResult<List<OrderDTO>>> GetAll()
		{
			return Ok(await _orderService.GetAllOrder());
		}

		//Add new order
		[HttpPost("NewOrder")]
        public async Task<ActionResult> AddOrder([FromBody] AddOrderDTO order)
        {
            try
            {
                // Check the validation
                //var validator = new NewDetailValidator();
                //ValidationResult validaResult = await validator.ValidateAsync(newCarDetail);
                //if (!validaResult.IsValid)
                //return BadRequest(validaResult.Errors);


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

        // Customer Payment
        [HttpPost("CustomerPayment")]
        public async Task<ActionResult> CustomerPayment([FromBody] CustomerPaymentDTO payment)
        {
            try
            {
                // Check the validation

                // Add New Order
                await _orderService.CustomerPayment(payment);
                return Ok(new
                {
                    message = "Customer Payment Success."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Select order by CustomerId
        [HttpGet("CustomerOrder/{customerId}")]
        public async Task<ActionResult<List<CustomerOrderDTO>>> GetOrderWithCustomerId([FromRoute] Guid customerId)
        {
            // Get Order With CustomerID
            return Ok(await _orderService.GetOrderByCustomerId(customerId));
        }

        //Select order by Id
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> GetOrderWithId([FromRoute] Guid id)
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
        public async Task<ActionResult<OrderDTO>> UpdateOrder([FromRoute] Guid id, [FromBody] UpdateOrderDTO orderDTO)
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
                    
                    //Update Order
                    await _orderService.UpdateOrder(id, orderDTO);
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

        //Update order status by id
        [HttpPut("UpdateOrderStatus/{id}")]
        public async Task<ActionResult<OrderDTO>> UpdateOrderStatus([FromRoute] Guid id, [FromBody] bool orderStatus)
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
                    await _orderService.UpdateStatus(id, orderStatus);
                    return Ok(new
                    {
                        message = "Update Car Success."
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
