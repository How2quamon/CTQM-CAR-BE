using CTQM_CAR.Service.Service.Interface;
using CTQM_CAR.Shared.DTO.CustomerDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CTQM__CAR_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CustomerController : ControllerBase
	{
		private readonly ICustomerService _customerService;

		public CustomerController(ICustomerService customerService)
		{
			_customerService = customerService;
		}


		// Login

		// LogOut

		// GetAllCustomer
		[HttpGet("GetAllCustomer")]
		public async Task<ActionResult<List<CustomerDTO>>> GetAllCustomer()
		{
			var customerList = await _customerService.GetAllCustomer();
			if (customerList == null)
				return BadRequest("No Customer Found.");
			return Ok(customerList);
		}

		// GetProfile
		[HttpGet("UserInfo/{id}")]
		public async Task<ActionResult<CustomerDTO>> GetCustomerProfile([FromRoute] Guid customerId)
		{
			// CheckUserExist
			var customerData = await _customerService.GetProfileById(customerId);
			if (customerData == null)
				return BadRequest("No Customer Found.");
			return Ok(customerData);
		}

		// SignUp
		[HttpPost("CreateNewCustomer")]
		public async Task<ActionResult> CreateNewCustomer([FromBody] CustomerCreateDTO customerData)
		{
			// Check Validate

			// Create New Customer
			bool isSuccess = await _customerService.CreateNewCustomer(customerData);
			if (isSuccess)
			{
				return Ok("Create Customer Success.");
			}
			return BadRequest("Create Customer Failed.");
		}

		// EditProfile
		[HttpPut("ChangeCustomerInfo/{id}")]
		public async Task<ActionResult<CustomerDTO>> ChangeCustomerInfo([FromRoute] Guid id, [FromBody] ChangeInfoDTO customerData)
		{
			// Check Validate

			// Change Customer Info
			var customerResult = await _customerService.ChangeCustomerInfo(customerData, id);
			if (customerResult == null)
				return BadRequest("Change Infomation Failed.");
			return Ok(customerResult);
		}

		// EditPassword
		[HttpPut("ChangeCustomerPassword/{id}")]
		public async Task<ActionResult> ChangePassword([FromRoute] Guid id, [FromBody] ChangePasswordDTO customerData)
		{
			// Check Validate

			// Change Customer Info
			CustomerDTO currentPassword = await _customerService.GetProfileById(id);
			bool isSuccess = await _customerService.ChangeCustomerPassword(id, customerData, currentPassword.CustomerPassword);
			if (!isSuccess)
				return BadRequest("Password Doesn't Match.");
			return Ok("Change Password Success.");
		}

		// DeleteCustomer
		[HttpDelete("DeleteCustomer/{id}")]
		public async Task<ActionResult> DeleteCustomer([FromRoute] Guid id)
		{
			// Check Validate

			// Delete Customer
			bool isSuccess = await _customerService.DeleteCustomer(id);
			if (!isSuccess)
				return BadRequest("Delete Customer Faild.");
			return Ok("Delete Success.");
		}
	}
}
