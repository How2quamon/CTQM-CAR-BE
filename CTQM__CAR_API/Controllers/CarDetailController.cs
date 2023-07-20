using CTQM_CAR.Service.Service.Interface;
using CTQM_CAR.Shared.DTO.CarDetailDTO;
using CTQM_CAR.Shared.DTO.CarDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

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

        [HttpPost("NewCarDetail")]
        public async Task<ActionResult> AddCarDetail([FromBody] CarDetailDTO _carDetail)
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
                /*Guid id = Guid.NewGuid();
                var carDetail = new CarDetailDTO
                {
                    DetailId = id,
                    CarId = _carDetail.CarId,
                    Head1 = _carDetail.Head1,
                    Title1 = _carDetail.Title1,
                    Head2 = _carDetail.Head2,
                    Title2  = _carDetail.Title2,
                    Head3 = _carDetail.Head3,
                    Title3  = _carDetail.Title3,
                };*/

                // Add New Comment
                CarDetailDTO carDetail = new CarDetailDTO();
                await _carDetailService.AddCarDetail(carDetail);
                return Ok(new
                {
                    message = "Add Car Detail Success."
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarDetailDTO>> Get([FromRoute] Guid id)
        {
            // Check Exist
            bool isExist = await _carDetailService.FindCarDetailById(id);
            if (isExist)
            {
                // Get Car With ID
                var carDetail = await _carDetailService.GetCarDetailById(id);

                // Check Get Car Success
                if (carDetail == null)
                    return NotFound("Get Car Failed.");
                return Ok(carDetail);
            }
            else
                return BadRequest("No Car Found.");
        }

        [HttpPut("UpdateCarDetail/{id}")]
        public async Task<ActionResult<CarDetailDTO>> UpdateCarDetail([FromRoute] Guid id, [FromBody] CarDetailDTO carDetail)
        {
            try
            {
                // Check validation
                //var validator = new UpdateCommentValidator();
                //ValidationResult validaResult = await validator.ValidateAsync(updateComment);
                //if (!validaResult.IsValid)
                    //return BadRequest(validaResult.Errors);

                // Check Exist
                bool isExist = await _carDetailService.FindCarDetailById(id);
                if (isExist)
                {
                    //Create New CarDetailDTO
                    /*var carDetailData = new CarDetailDTO
                    {
                        DetailId = id,
                        CarId = carDetail.CarId,
                        Head1 = carDetail.Head1,
                        Title1 = carDetail.Title1,
                        Head2 = carDetail.Head2,
                        Title2 = carDetail.Title2,
                        Head3 = carDetail.Head3,
                        Title3 = carDetail.Title3,
                    };*/

                    //Update CarDetail
                    CarDetailDTO carDetailData = await _carDetailService.GetCarDetailById(id);
                    await _carDetailService.UpdateCarDetail(carDetailData);
                    return Ok(new
                    {
                        message = "Update Car Detail Success."
                    });
                }
                else
                    return NotFound("No Car Detail Found.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteCarDetail/{id}")]
        public async Task<ActionResult> DeleteCarDetail([FromRoute] Guid id)
        {
            try
            {
                // Check Exist
                bool isExist = await _carDetailService.FindCarDetailById(id);
                if (isExist)
                {
                    // Delete Comment
                    await _carDetailService.DeleteCarDetail(id);

                    return Ok(new
                    {
                        message = "Delete Car Detail Success."
                    });
                }
                else
                    return NotFound("No Car Detail Found.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
