using CTQM_CAR.Service.Service.Interface;
using CTQM_CAR.Shared.DTO.CarDetailDTO;
using CTQM_CAR.Shared.DTO.CarDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CTQM__CAR_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CarController : ControllerBase
	{
		private readonly ICarService _carService;

		public CarController(ICarService carService)
		{
			_carService = carService;
		}

        //Show all Car
        //[AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<List<CarDTO>>> Get()
        {
            return Ok(await _carService.GetAllCars());
        }

        //Show selected car
        [HttpGet("{id}")]
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
        public async Task<ActionResult> AddCar([FromBody] CarDTO _car)
        {
            try
            {
                // Check the validation
                //var validator = new NewDetailValidator();
                //ValidationResult validaResult = await validator.ValidateAsync(newCarDetail);
                //if (!validaResult.IsValid)
                //return BadRequest(validaResult.Errors);

                // Create New Comment
                //Random rnd = new Random();
                Guid id = Guid.NewGuid();
                var car = new CarDTO
                {
                    CarId = id,
                    CarName = _car.CarName,
                    CarModel= _car.CarModel,
                    CarClass = _car.CarClass,
                    CarEngine = _car.CarEngine,
                    CarAmount = _car.CarAmount,
                    CarPrice = _car.CarPrice,
                    MoTa= _car.MoTa,
                    Head1 = _car.Head1,
                    MoTa2 = _car.MoTa2,
                };

                // Add New Comment
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
        public async Task<ActionResult<CarDetailDTO>> UpdateCar([FromRoute] Guid id, [FromBody] CarDTO car)
        {
            try
            {
                // Check validation
                //var validator = new UpdateCommentValidator();
                //ValidationResult validaResult = await validator.ValidateAsync(updateComment);
                //if (!validaResult.IsValid)
                //return BadRequest(validaResult.Errors);

                // Check Exist
                bool isExist = await _carService.FindCarById(id);
                if (isExist)
                {
                    //Create New CarDTO
                    var carData = new CarDTO
                    {
                        CarId = id,
                        CarName = car.CarName,
                        CarModel = car.CarModel,
                        CarClass = car.CarClass,
                        CarEngine = car.CarEngine,
                        CarAmount = car.CarAmount,
                        CarPrice = car.CarPrice,
                        MoTa = car.MoTa,
                        Head1 = car.Head1,
                        MoTa2 = car.MoTa2,
                    };

                    //Update Car
                    await _carService.UpdateCar(carData);
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
    }
}
