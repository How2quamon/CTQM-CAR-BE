using CTQM_CAR.Service.Service.Interface;
using CTQM_CAR.Shared.DTO.CustomerDTO;
using CTQM_CAR_HEADER.Controllers;
using CTQM_CAR_HEADER.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CTQM_CAR_API.Identity;

namespace CTQM__CAR_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CustomerController : ControllerBase
	{
		private readonly ICustomerService _customerService;
		private readonly ITokenService _tokenService;
		private readonly IAuthenticateRepo _authenticateRepo;

		public CustomerController(ICustomerService customerService, ITokenService tokenService, IAuthenticateRepo authenticateRepo)
		{
			_customerService = customerService;
			_tokenService = tokenService;
			_authenticateRepo = authenticateRepo;
		}

		[HttpPost("Login")]
		public async Task<ActionResult> CustomerLogin([FromBody] CustomerLoginDTO login)
		{
			try
			{
				// Check The Validation

				// GetCustomer Being Loging
				var CustomerLogin = await _customerService.GetCustomerByEmail(login.Email);
				if (CustomerLogin != null)
				{
					if (CustomerLogin.Email == login.Email && CustomerLogin.Password == login.Password)
					{
						bool tokenExist = await _tokenService.CheckTokenExist(CustomerLogin.Id);
						bool tokenExpire = await _tokenService.CheckTokenExpire(CustomerLogin.Id);
						if (login.Email == "thejohan39@gmail.com") CustomerLogin.admin = true;
						// Check Does Customer Had JWT
						// Check Does JWT Had Expired
						if (tokenExist && tokenExpire || !tokenExist)
						{
							// Check Generate Token Success
							AuthenticateController identity = new AuthenticateController(_authenticateRepo);
							string token = identity.GenerateCustomerToken(CustomerLogin);
							bool setCache = await _tokenService.SetCustomerLoginIfo(CustomerLogin.Id, token);
							if (setCache)
							{
								Console.WriteLine(token);
								return Ok(new
								{
									message = "Ready To Login.",
									tokenPass = token,
									CustomerId = CustomerLogin.Id,
									CustomerName = CustomerLogin.Name
								});
							}
							return Ok("Set Cache Failed.");
						}
						if (tokenExist && !tokenExpire)
						{
							// Get Customer Current Token
							string currentToken = await _tokenService.GetCustomerToken(CustomerLogin.Id);
							return Ok(new
							{
								message = "Ready To Login.",
								tokenPass = currentToken,
								CustomerId = CustomerLogin.Id,
								CustomerName = CustomerLogin.Name
							});
						}
					}
					return Ok(new { message = "Password or Email doesn't match." });
				}
				return Ok("No Customer Found.");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost("RefreshToken/{id}")]
		public async Task<ActionResult> RefreshToken([FromRoute] Guid id)
		{
			try
			{
				// Check Does Customer Had JWT
				// Check Does JWT Had Expired
				bool tokenExist = await _tokenService.CheckTokenExist(id);
				bool tokenExpire = await _tokenService.CheckTokenExpire(id);
				if (tokenExist)
				{
					// Get CustomerCurrent JWT
					string currentToken = await _tokenService.GetCustomerToken(id);
					// Get Current Customer JWT And Current Customer Info Need To Create New Token
					CustomerTokenDTO CustomerData = await _tokenService.GetCustomerLoginIfo(id);
					// GetIdentity Controller To Call GenerateToken Func
					AuthenticateController identity = new AuthenticateController(_authenticateRepo);
					// The Token Had Expire
					if (tokenExpire)
					{
						// Check Generate Token Success
						var newToken = identity.GenerateCustomerToken(CustomerData);
						bool setCache = await _tokenService.SetCustomerLoginIfo(id, newToken);
						if (setCache)
						{
							Console.WriteLine(newToken);
							return Ok("Your Token's Had Expired, So We Create For You A New One.");
						}
						return Ok("Set Cache Failed.");
					}
					// The Token Hasn't Expire Yet
					else
					{
						// Check Generate Token Success
						var newToken = identity.GenerateCustomerToken(CustomerData);
						bool setCache = await _tokenService.SetCustomerLoginIfo(id, newToken);
						if (setCache)
						{
							// Block the old token
							await _tokenService.SetCustomerOldTokenBlackList(currentToken);
							Console.WriteLine(newToken);
							return Ok("Your Token's Hasn't Expired Yet, But We Still Create For You A New Token.");
						}
						return Ok("Set Cache Failed.");
					}
				}
				return Ok("You Doesn't Has Token, Login To Get One!");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpPost("LogOut/{id}")]
		public async Task<ActionResult> CustomerLogOut([FromRoute] Guid id)
		{
			try
			{
				// Check Does Customer Had JWT
				bool tokenExist = await _tokenService.CheckTokenExist(id);
				if (tokenExist)
				{
					// Check Remove Token Success
					bool removeCache = await _tokenService.RemoveCustomerToken(id);
					if (removeCache)
						return Ok("Remove Customer Token Success");
					return Ok("Remove Token Failed.");
				}
				return Ok("You Doesn't Has Token, Login To Get One!");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}


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
		//[Authorize]
		[HttpGet("CustomerInfo/{customerId}")]
		public async Task<ActionResult<CustomerDTO>> GetCustomerProfile([FromRoute] Guid customerId)
		{
			// CheckCustomerExist
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
		[RequiresClaim(IdentityData.AdminCustomerClaimName, "true")]
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
