using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTQM_CAR.Shared.DTO.CarDTO
{
    public class CarTypeDTO
    {
        public List<CarDTO> Cars { get; set; }
        public string? Type { get;set; }
    }
}
