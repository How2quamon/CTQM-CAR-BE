using CTQM__CAR_API.CTQM_CAR.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTQM_CAR.Shared.DTO.CartDTO
{
    public class CartDetailDTO
    {
        public Guid CartId { get; set; }

        public Guid CustomerId { get; set; }

        public Guid CarId { get; set; }

        public int? Amount { get; set; }

        public double? Price { get; set; }

        public string CarName { get; set; }

        public string CarModel { get; set; }
        
        public int CarAmount { get; set; }
        
        public double CarPrice { get; set; }
    }
}
