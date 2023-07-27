using CTQM_CAR.Shared.DTO.CustomerDTO;

namespace CTQM_CAR_HEADER.IRepository
{
	public interface IAuthenticateRepo
	{
		string Authenticate(CustomerTokenDTO customer);
	}
}
