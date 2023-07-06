using CTQM_CAR.Service.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CTQM__CAR_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CarDetailController : ControllerBase
	{
		private readonly ICarDetailService _carDetailService;

		public CarDetailController(ICarDetailService carDetailService)
		{
			_carDetailService = carDetailService;
		}
	}
}
