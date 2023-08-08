using CTQM_CAR.Domain;
using CTQM_CAR.Shared.DTO.CustomerDTO;

namespace CTQM_CAR.Service.Service.Interface
{
	public interface ICustomerService
	{
		Task<Customer> GetCustomerById(Guid customerId);
		Task<CustomerDTO> GetProfileById(Guid customerId);
        Task<List<CustomerDTO>> GetAllCustomer();
		Task<CustomerTokenDTO?> GetCustomerByEmail(string email);
		Task<bool> CreateNewCustomer(CustomerCreateDTO customerData);
		Task<CustomerDTO> ChangeCustomerInfo(ChangeInfoDTO customerData, Guid customerId);
		Task<bool> ChangeCustomerPassword(Guid customerId, ChangePasswordDTO customerData);
		Task<bool> DeleteCustomer(Guid customerId);
	}
}
