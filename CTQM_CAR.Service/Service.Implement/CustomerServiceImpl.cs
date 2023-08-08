using CTQM_CAR.Domain;
using CTQM_CAR.Repositories.IRepository;
using CTQM_CAR.Service.Service.Interface;
using CTQM_CAR.Shared.DTO.CustomerDTO;
using Microsoft.EntityFrameworkCore;

namespace CTQM_CAR.Service.Service.Implement
{
	public class CustomerServiceImpl : ICustomerService
	{
		private readonly IUnitOfWork _unitOfWork;
		public CustomerServiceImpl(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

        public async Task<Customer> GetCustomerById(Guid customerId)
        {
            return await _unitOfWork.customersRepo.GetById(customerId);
        }

        public async Task<CustomerDTO> GetProfileById(Guid customerId)
		{
			var customerData = await _unitOfWork.customersRepo.GetById(customerId);
			if (customerData == null) return null;
			CustomerDTO customerResult = new CustomerDTO(
				customerData.CustomerId,
				customerData.CustomerName,
				customerData.CustomerPhone,
				customerData.CustomerAddress,
				customerData.CustomerDate,
				customerData.CustomerLicense,
				customerData.CustomerEmail,
                customerData.CustomerVaild
                );
			return customerResult;
		}

		public async Task<List<CustomerDTO>> GetAllCustomer()
		{
			var customerListData = await _unitOfWork.customersRepo.GetAll();
			if (customerListData == null) return null;
			List<CustomerDTO> result = new List<CustomerDTO>();
			foreach (var customer in customerListData)
			{
				CustomerDTO customerTmp = new CustomerDTO(
					customer.CustomerId,
					customer.CustomerName,
					customer.CustomerPhone,
					customer.CustomerAddress,
					customer.CustomerDate,
					customer.CustomerLicense,
					customer.CustomerEmail,
                    (bool)customer.CustomerVaild,
					customer.CustomerPassword);
				result.Add(customerTmp);
			}
			return result;
		}

		public async Task<CustomerTokenDTO?> GetCustomerByEmail(string email)
		{
			try
			{
				Customer customer = await _unitOfWork.customersRepo.GetCustomerWithEmail(email);
				if (customer != null)
				{
					CustomerTokenDTO userData = new CustomerTokenDTO
					{
						Id = customer.CustomerId,
						Email = customer.CustomerEmail,
						Password = customer.CustomerPassword,
						Name = customer.CustomerName
					};
					return userData;
				}
				return null;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return null;
			}
		}

		public async Task<bool> CreateNewCustomer(CustomerCreateDTO customerData)
		{
			try
			{
				Guid newGuid = Guid.NewGuid();
				Customer newCustomer = new Customer
				{
					CustomerId = newGuid,
					CustomerName = customerData.CustomerName,
					CustomerPhone = "Unknow",
					CustomerAddress = "Unknow",
					CustomerDate = DateTime.Now,
					CustomerLicense = "Unknow",
					CustomerEmail = customerData.CustomerEmail,
					CustomerPassword = customerData.CustomerPassword,
					CustomerVaild = false
				};
				await _unitOfWork.customersRepo.Add(newCustomer);
				await _unitOfWork.SaveAsync();
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return false;
			}
		}

		public async Task<CustomerDTO> ChangeCustomerInfo(ChangeInfoDTO customerData, Guid customerId)
		{
			try
			{
				Customer updateCustomer = await GetCustomerById(customerId);
				if (!string.IsNullOrEmpty(customerData.CustomerName))
					updateCustomer.CustomerName = customerData.CustomerName;
				if (!string.IsNullOrEmpty(customerData.CustomerPhone))
					updateCustomer.CustomerPhone = customerData.CustomerPhone;
				if (!string.IsNullOrEmpty(customerData.CustomerAddress))
					updateCustomer.CustomerAddress = customerData.CustomerAddress;
				if (customerData.CustomerDate != null)
					updateCustomer.CustomerDate = customerData.CustomerDate;
				if (!string.IsNullOrEmpty(customerData.CustomerLicense))
					updateCustomer.CustomerLicense = customerData.CustomerLicense;
				if (!string.IsNullOrEmpty(customerData.CustomerEmail))
					updateCustomer.CustomerEmail = customerData.CustomerEmail;
                if (customerData.CustomerVaild != null)
                    updateCustomer.CustomerVaild = customerData.CustomerVaild;
                _unitOfWork.customersRepo.Update(updateCustomer);
				await _unitOfWork.SaveAsync();
				CustomerDTO customerResult = new CustomerDTO(updateCustomer.CustomerId, customerData.CustomerName, customerData.CustomerPhone, customerData.CustomerAddress, customerData.CustomerDate, customerData.CustomerLicense, customerData.CustomerEmail, (bool)customerData.CustomerVaild);
				return customerResult;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return null;
			}
		}

		public async Task<bool> ChangeCustomerPassword(Guid customerId, ChangePasswordDTO customerData)
		{
			try
            {
				string currentPassword = GetCustomerById(customerId).Result.CustomerPassword;

                if (customerData.OldPassword != currentPassword)
					return false;

				Customer updateCustomer = await GetCustomerById(customerId);
				updateCustomer.CustomerPassword = customerData.NewPassword;
				_unitOfWork.customersRepo.Update(updateCustomer);
				await _unitOfWork.SaveAsync();
				return true;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				return false;
			}
		}

		public async Task<bool> DeleteCustomer(Guid customerId)
		{
			try
			{
				// Delete Order
				
				// Delete Cart
				bool checkCartDelete = await _unitOfWork.customersRepo.Delete(customerId);
				bool checkCustomerDelete = await _unitOfWork.cartsRepo.DeleteCustomerCart(customerId);
				if (checkCartDelete && checkCustomerDelete)
				{
					await _unitOfWork.SaveAsync();
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
	}
}
