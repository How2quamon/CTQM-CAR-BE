using CTQM_CAR.Service.Service.Interface;
using CTQM_CAR.Shared.DTO.CarDetailDTO;
using CTQM_CAR.Shared.DTO.CarDTO;
using CTQM_CAR.Shared.DTO.CustomerDTO;
using CTQM_CAR_API.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CTQM__CAR_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
    [Authorize]
	public class CarController : ControllerBase
	{
		private readonly ICarService _carService;

		public CarController(ICarService carService)
		{
			_carService = carService;
		}

        //Show all Car
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<CarDTO>>> Get()
        {
            return Ok(await _carService.GetAllCars());
        }

        //Show selected car
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<CarDTO>> Get([FromRoute] Guid id)
        {
            // Check Exist
            bool isExist = await _carService.FindCarById(id);
            if (isExist)
            {
                // Get Car With ID
                var car = await _carService.GetCarById(id);

                // Check Get Car Success
                if (car == null)
                    return NotFound("Get Car Failed.");
                return Ok(car);
            }
            else
                return BadRequest("No Car Found.");
        }

        //Create new car
        [HttpPost("NewCar")]
        public async Task<ActionResult> AddCar([FromBody] AddCarDTO car)
        {
            try
            {
                // Check the validation

                // Add New Car
                await _carService.AddCar(car);
                return Ok(new
                {
                    message = "Add Car Success."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Delete car
        [HttpDelete("DeleteCar/{id}")]
        [RequiresClaim(IdentityData.AdminCustomerClaimName, "true")]
        public async Task<ActionResult> DeleteCar([FromRoute] Guid id)
        {
            try
            {
                // Check Exist
                bool isExist = await _carService.FindCarById(id);
                if (isExist)
                {
                    // Delete Comment
                    await _carService.DeleteCar(id);

                    return Ok(new
                    {
                        message = "Delete Car Success."
                    });
                }
                else
                    return NotFound("No Car Found.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Update Car
        [HttpPut("UpdateCar/{id}")]
        [RequiresClaim(IdentityData.AdminCustomerClaimName, "true")]
        public async Task<ActionResult<CarDetailDTO>> UpdateCar([FromRoute] Guid id, [FromBody] AddCarDTO carData)
        {
            try
            {
                // Check validation

                // Check Exist
                bool isExist = await _carService.FindCarById(id);
                if (isExist)
                {

                    //Update Car
                    await _carService.UpdateCar(id, carData);
                    return Ok(new
                    {
                        message = "Update Car Success."
                    });
                }
                else
                    return NotFound("No Car Found.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Search Car By Name
        [HttpGet("GetCarByName/{carName}")]
        [AllowAnonymous]
        public async Task<ActionResult<List<CarDTO>>> Search([FromRoute] string carName)
        {
            var car = await _carService.GetCarByName(carName);

            // Check Get Car Success
            if (car == null)
                return NotFound("Get Car Failed.");
            return Ok(car);
        }

        //Recommended Car By Type
        [HttpGet("GetCarWithModel/{carModel}")]
        [AllowAnonymous]
        public async Task<ActionResult<List<CarDTO>>> Recommened(string? carModel)
        {
            var car = await _carService.GetCarByModel(carModel);

            if (car == null)
                return NotFound("Get Car Failed.");
            return Ok(car);
        }

        [HttpGet("SearchForCars/{search}")]
        [AllowAnonymous]
        public async Task<ActionResult<List<CarDTO>>> SearchCars(string search)
        {
            var car = await _carService.SearchCars(search);

            if (car == null)
                return NotFound("Get Car Failed.");
            return Ok(car);
        }
    }
}
