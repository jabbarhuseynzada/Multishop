using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.Business.Abstract;
using MultiShop.Cargo.DTOLayer.Dtos.CargoDetailDtos;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CargoDetailController(ICargoDetailService cargoDetailService) : ControllerBase
    {
        private readonly ICargoDetailService _cargoDetailService = cargoDetailService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _cargoDetailService.GetAllTAsync();
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("No cargo details found.");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _cargoDetailService.GetTByIdAsync(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound("Cargo detail not found.");
        }
        [HttpPost]
        public async Task<IActionResult> Add(CreateCargoDetailDto createCargoDetailDto)
        {
            if (createCargoDetailDto == null || string.IsNullOrEmpty(createCargoDetailDto.SenderCustomer) || string.IsNullOrEmpty(createCargoDetailDto.ReceiverCustomer))
            {
                return BadRequest("Invalid cargo detail data.");
            }
            var cargoDetail = new CargoDetail
            {
                SenderCustomer = createCargoDetailDto.SenderCustomer,
                ReceiverCustomer = createCargoDetailDto.ReceiverCustomer,
                Barcode = createCargoDetailDto.Barcode,
                CargoCompanyId = createCargoDetailDto.CargoCompanyId
            };
            await _cargoDetailService.AddTAsync(cargoDetail);
            return Ok("Cargo detail added successfully.");
        }
        [HttpPut]
        public async Task<IActionResult> Update(UpdateCargoDetailDto updateCargoDetailDto)
        {
            if (updateCargoDetailDto == null || string.IsNullOrEmpty(updateCargoDetailDto.SenderCustomer) || string.IsNullOrEmpty(updateCargoDetailDto.ReceiverCustomer))
            {
                return BadRequest("Invalid cargo detail data.");
            }
            var cargoDetail = new CargoDetail
            {
                CargoDetailId = updateCargoDetailDto.CargoDetailId,
                SenderCustomer = updateCargoDetailDto.SenderCustomer,
                ReceiverCustomer = updateCargoDetailDto.ReceiverCustomer,
                Barcode = updateCargoDetailDto.Barcode,
                CargoCompanyId = updateCargoDetailDto.CargoCompanyId
            };
            await _cargoDetailService.UpdateTAsync(cargoDetail);
            return Ok("Cargo detail updated successfully.");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var cargoDetail = await _cargoDetailService.GetTByIdAsync(id);
            if (cargoDetail == null)
            {
                return NotFound("Cargo detail not found.");
            }
            await _cargoDetailService.DeleteTAsync(id);
            return Ok("Cargo detail deleted successfully.");
        }
    }
}

