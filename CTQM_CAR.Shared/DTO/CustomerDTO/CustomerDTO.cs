using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTQM_CAR.Shared.DTO.CustomerDTO
{
    public class CustomerDTO
    {
        public Guid CustomerId { get; set; }

        public string? CustomerName { get; set; }

        public string? CustomerPhone { get; set; }

        public string? CustomerAddress { get; set; }

        public DateTime CustomerDate { get; set; }

        public string? CustomerLicense { get; set; }

        public string? CustomerEmail { get; set; }

        public string? CustomerPassword { get; set; }

        public bool? CustomerVaild { get; set; }

        public CustomerDTO(Guid id, string name, string phone, string address, DateTime date, string license, string email, bool vaild, string? password = "unknow")
        {
            CustomerId = id;
            CustomerName = name;
            CustomerPhone = phone;
            CustomerAddress = address;
            CustomerDate = date;
            CustomerLicense = license;
            CustomerEmail = email;
            CustomerPassword = password;
            CustomerVaild = vaild;
        }
    }
}
