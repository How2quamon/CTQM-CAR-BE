using CTQM_CAR.Shared.DTO.CustomerDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTQM_CAR.Service.Service.Interface
{
	public interface ITokenService
	{
		Task<bool> SetCustomerLoginIfo(Guid customerId, string token);
		Task<bool> CheckTokenExist(Guid customerId);
		Task<bool> CheckTokenExpire(Guid customerId);
        Task<bool> CheckCookieTokenExist(string token);
        Task<bool> CheckCookieTokenExpire(string token);
        Task<string> GetCustomerToken(Guid customerId);
        Task<CustomerTokenDTO> GetCustomerLoginIfo(Guid customerId);
        Task<CustomerTokenDTO> GetCustomerCookieLoginIfo(string token);
        Task<bool> RemoveCustomerToken(Guid customerId);
		Task<bool> SetCustomerOldTokenBlackList(string token);
		Task<bool> CheckOldTokenBlackList(string token);
		Task<bool> CheckCustomerTokenAuthen(string token);
		Task<bool> CheckCustomerTokenExpired(string token);
	}
}
