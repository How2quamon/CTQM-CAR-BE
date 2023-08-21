using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTQM_CAR.Shared.DTO.CartDTO
{
    public class UpdateCartDTO
    {
        public Guid CartId { get; set; }

        public int Amount { get; set; }
    }
}
