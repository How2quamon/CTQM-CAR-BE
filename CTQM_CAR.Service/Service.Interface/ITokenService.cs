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
		Task<bool> SetCustomerLoginIfo(string customerId, string token);
		Task<bool> CheckTokenExist(string customerId);
		Task<bool> CheckTokenExpire(string customerId);
		Task<string> GetCustomerToken(string customerId);
		Task<CustomerTokenDTO> GetCustomerLoginIfo(string customerId);
		Task<bool> RemoveCustomerToken(string customerId);
		Task<bool> SetCustomerOldTokenBlackList(string token);
		Task<bool> CheckOldTokenBlackList(string token);
		Task<bool> CheckCustomerTokenAuthen(string token);
		Task<bool> CheckCustomerTokenExpired(string token);
	}
}
