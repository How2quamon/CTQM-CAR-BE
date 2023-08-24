using CTQM_CAR.Service.Service.Interface;
using CTQM_CAR.Shared.DTO.CarDetailDTO;
using CTQM_CAR.Shared.DTO.CarDTO;
using CTQM_CAR_API.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CTQM__CAR_API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
    [Authorize]
	public class CarDetailController : ControllerBase
	{
		private readonly ICarDetailService _carDetailService;

		public CarDetailController(ICarDetailService carDetailService)
		{
			_carDetailService = carDetailService;
		}

		[HttpGet]
		[RequiresClaim(IdentityData.AdminCustomerClaimName, "true")]
        public async Task<ActionResult<List<CarDTO>>> Get()
		{
			return Ok(await _carDetailService.GetAllCarDetail());
		}

		[HttpPost("NewCarDetail")]
        [RequiresClaim(IdentityData.AdminCustomerClaimName, "true")]
        public async Task<ActionResult> AddCarDetail([FromBody] AddCarDetailDTO carDetail)
        {
            try
            {
                // Check the validation

                // Add New CarDetail
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

        [HttpGet("GetByCarId/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<CarDetailDTO>> GetByCarId([FromRoute] Guid id)
        {
            // Check Exist
            bool isExist = await _carDetailService.FindCarDetailByCarId(id);
            if (isExist)
            {
                // Get Car With ID
                var carDetail = await _carDetailService.GetCarDetailByCarId(id);

                // Check Get Car Success
                if (carDetail == null)
                    return NotFound("Get Car Failed.");
                return Ok(carDetail);
            }
            else
                return BadRequest("No Car Found.");
        }

		[HttpGet("GetById/{id}")]
        [AllowAnonymous]
		public async Task<ActionResult<CarDetailDTO>> GetById([FromRoute] Guid id)
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
		[RequiresClaim(IdentityData.AdminCustomerClaimName, "true")]
        public async Task<ActionResult<CarDetailDTO>> UpdateCarDetail([FromRoute] Guid id, [FromBody] UpdateCarDetailDTO carDetail)
        {
            try
            {
                // Check validation

                // Check Exist
                bool isExist = await _carDetailService.FindCarDetailById(id);
                if (isExist)
                {
                    //Update CarDetail
                    await _carDetailService.UpdateCarDetail(id, carDetail);
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
		[RequiresClaim(IdentityData.AdminCustomerClaimName, "true")]
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
