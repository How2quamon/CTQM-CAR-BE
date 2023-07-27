using CTQM_CAR.Shared.DTO.CustomerDTO;
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
		private readonly IConfiguration? _configuration;

		[HttpPost]
		public string Authenticate(CustomerTokenDTO Customer)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var tokenKey = Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]);

			var claims = new List<Claim>
			{
				new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new(JwtRegisteredClaimNames.Sub, Customer.Name),
				new(JwtRegisteredClaimNames.Email, Customer.Email),
				new("admin", Customer.admin.ToString()!, ClaimValueTypes.Boolean),
				new("Password", Customer.Password),
				new("CustomerId", Customer.Id)
			};

			var tokenDescriptior = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				//Expires = DateTime.UtcNow.AddMinutes(20),
				Issuer = _configuration["JwtSettings:Issuer"],
				Audience = _configuration["JwtSettings:Audience"],
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256)
			};

			var token = tokenHandler.CreateToken(tokenDescriptior);
			Console.WriteLine(token);

			return tokenHandler.WriteToken(token);
		}
	}
}
