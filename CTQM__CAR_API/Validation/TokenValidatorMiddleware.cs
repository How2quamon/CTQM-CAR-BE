using Microsoft.AspNetCore.Authorization;
using System.Net;
using CTQM_CAR.Service.Service.Interface;

namespace CTQM_CAR_API.Validators
{
	public class TokenValidatorMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ITokenService _tokenService;
		public TokenValidatorMiddleware(RequestDelegate next, ITokenService tokenService)
		{
			_next = next;
			_tokenService = tokenService;
		}

		public async Task InvokeAsync(HttpContext context) 
		{
			// Get User JWT Send To Header Authorization From HTTPContext
			string authorizationHeader = context.Request.Headers["Authorization"];
			var endpoint = context.GetEndpoint();
			bool isAllowAnonymous = false;
			if (endpoint?.Metadata.GetMetadata<AllowAnonymousAttribute>() != null)
				isAllowAnonymous = true;

			// Check The AuthorizationHeader Not Null, Empty And It Start With Bearer
			if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer ") && isAllowAnonymous == false)
			{
				// Get JWT From Header Authorization
				string jwt = authorizationHeader.Substring("Bearer ".Length);

				// Check Token In BlackList
				bool isInBlackList = await _tokenService.CheckOldTokenBlackList(jwt);
				// True Meaning Token Is In BlackList, And User Can't Use It
				if (!isInBlackList)
				{
					// Check Token Expired || True => Token Expired
					bool isExpired = await _tokenService.CheckCustomerTokenExpired(jwt);
					if (!isExpired)
					{
						// Check Token Vaild To Authen | True => Token vaild
						bool isVaild = await _tokenService.CheckCustomerTokenAuthen(jwt);
						if (isVaild)
						{
							await _next(context);
						}
						else
						{
							Console.WriteLine("You're Token Is Not Vaild.");
							context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
						}
					}
					else
					{
						context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
						Console.WriteLine("You're Token Had Expired.");
					}
				}
				else
				{
					Console.WriteLine("You Are Using A Token In Black List, Mean That Is Can't Be Use Anymore.");
					context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
				}
			}
			else await _next(context);
		}
	}
}
