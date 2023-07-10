using CTQM_CAR.Shared.DTO.CustomerDTO;

namespace CTQM_CAR.Service.Service.Interface
{
	public interface ICustomerService
	{
		Task<CustomerDTO> GetProfileById(Guid customerId);
		Task<List<CustomerDTO>> GetAllCustomer();
		Task<bool> CreateNewCustomer(CustomerCreateDTO customerData);
		Task<CustomerDTO> ChangeCustomerInfo(ChangeInfoDTO customerData, Guid customerId);
		Task<bool> ChangeCustomerPassword(Guid customerId, ChangePasswordDTO customerData, string currentPassword);
		Task<bool> DeleteCustomer(Guid customerId);
	}
}
