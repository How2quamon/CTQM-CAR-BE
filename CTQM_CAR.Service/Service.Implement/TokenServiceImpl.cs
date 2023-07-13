using CTQM_CAR.Service.Service.Interface;
using CTQM_CAR.Shared.DTO.CustomerDTO;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTQM_CAR.Service.Service.Implement
{
	public class TokenServiceImpl : ITokenService
	{
		private readonly IDistributedCache _cache;
		public TokenServiceImpl(IDistributedCache cache)
		{
			_cache = cache;
		}

		public async Task<bool> SetCustomerLoginIfo(string customerId, string token)
		{
			try
			{
				//var options = new DistributedCacheEntryOptions
				//{
				//	AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(20)
				//};
				await _cache.SetStringAsync(customerId, token);
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return false;
			}
		}

		public async Task<bool> CheckTokenExist(string customerId)
		{
			try
			{
				var token = await _cache.GetStringAsync(customerId);
				if (token != null)
					return true;
				return false;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return false;
			}
		}

		public async Task<bool> CheckTokenExpire(string customerId)
		{
			try
			{
				var token = await _cache.GetStringAsync(customerId);
				if (token != null)
				{
					var jwtHandler = new JwtSecurityTokenHandler();
					var jwtToken = jwtHandler.ReadJwtToken(token);
					var expirationClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "exp");
					if (expirationClaim != null && long.TryParse(expirationClaim.Value, out long expirationTime))
					{
						var currentTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
						if (expirationTime <= currentTime)
						{
							return true;
						}
					}
					return false;
				}
				return false;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return false;
			}
		}

		public async Task<string> GetCustomerToken(string customerId)
		{
			try
			{
				return await _cache.GetStringAsync(customerId);
			}
			catch (Exception ex)
			{
				Console.Write(ex.Message);
				return null;
			}
		}

		public async Task<CustomerTokenDTO> GetCustomerLoginIfo(string customerId)
		{
			try
			{
				var token = await _cache.GetStringAsync(customerId);
				if (token != null)
				{
					var tokenHandler = new JwtSecurityTokenHandler();
					var jwtToken = tokenHandler.ReadJwtToken(token);

					// Get Claims List From JWT
					var claims = jwtToken.Claims.ToList();
					string id = "";
					string name = "";
					string email = "";
					string password = "";
					string admin = "";
					// Access Into Claims Data
					foreach (var claim in claims)
					{
						var claimType = claim.Type;
						var claimValue = claim.Value;
						if (claim.Type == "CustomerId")
							id = claimValue;
						if (claim.Type == JwtRegisteredClaimNames.Sub)
							name = claimValue;
						if (claim.Type == JwtRegisteredClaimNames.Email)
							email = claimValue;
						if (claim.Type == "Password")
							password = claimValue;
						if (claim.Type == "admin")
							admin = claimValue;
					}

					CustomerTokenDTO CustomerData = new CustomerTokenDTO
					{
						Id = id,
						Email = email,
						Password = password,
						Name = name,
						admin = bool.Parse(admin)
					};
					return CustomerData;
				}
				return null;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return null;
			}
		}

		public async Task<bool> RemoveCustomerToken(string customerId)
		{
			try
			{
				await _cache.RemoveAsync(customerId);
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return false;
			}
		}

		public async Task<bool> SetCustomerOldTokenBlackList(string token)
		{
			try
			{
				var jwtHandler = new JwtSecurityTokenHandler();
				var jwt = jwtHandler.ReadJwtToken(token);

				// JTI is JWT unique value in a token 
				string jti = jwt.Id;
				var options = new DistributedCacheEntryOptions
				{
					AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(20)
				};
				await _cache.SetStringAsync($"BlacklistTokens:{jti}", token, options);

				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return false;
			}
		}

		public async Task<bool> CheckOldTokenBlackList(string token)
		{
			try
			{
				var jwtHandler = new JwtSecurityTokenHandler();
				var jwt = jwtHandler.ReadJwtToken(token);

				// JTI is JWT unique value in a token 
				string jti = jwt.Id;
				var value = await _cache.GetStringAsync($"BlacklistTokens:{jti}");
				// False is the token is not in blacklist
				if (value == null)
					return false;
				return true;

			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return false;
			}
		}

		public async Task<bool> CheckCustomerTokenAuthen(string token)
		{

			try
			{
				var tokenHandler = new JwtSecurityTokenHandler();
				var jwtToken = tokenHandler.ReadJwtToken(token);

				// Get Claims List From JWT
				string id = jwtToken.Claims.FirstOrDefault(c => c.Type == "CustomerId")?.Value;
				if (id != null)
				{
					var CustomerCurrentToken = await GetCustomerToken(id);
					if (CustomerCurrentToken != null && token.Equals(CustomerCurrentToken))
						return true;
				}
				return false;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return false;
			}

		}

		public async Task<bool> CheckCustomerTokenExpired(string token)
		{

			try
			{
				var tokenHandler = new JwtSecurityTokenHandler();
				var jwtToken = tokenHandler.ReadJwtToken(token);

				// Get Claims List From JWT
				string id = jwtToken.Claims.FirstOrDefault(c => c.Type == "CustomerId")?.Value;
				if (id != null)
				{
					var isExpired = await CheckTokenExpire(id);
					// True is expired || False is not expire yet
					if (isExpired) return true;
				}
				return false;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return false;
			}

		}
	}
}
