using CTQM_CAR.Shared.DTO.CustomerDTO;
using CTQM_CAR_HEADER.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CTQM_CAR_HEADER.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthenticateController : ControllerBase
	{
		private readonly IAuthenticateRepo _authenticate;

		public AuthenticateController(IAuthenticateRepo authenticate)
		{
			_authenticate = authenticate;
		}

		[HttpPost("Login")]
		public string GenerateCustomerToken([FromBody] CustomerTokenDTO customer)
		{
			try
			{
				var token = _authenticate.Authenticate(customer);
				return token;
			}
			catch (Exception ex)
			{
				return ex.Message;
			}
		}
	}
}
