using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.Business.Abstract;
using MultiShop.Cargo.DTOLayer.Dtos.CargoOperationDtos;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CargoOperationController(ICargoOperationService cargoOperationService) : ControllerBase
    {
        private readonly ICargoOperationService _cargoOperationService = cargoOperationService;
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _cargoOperationService.GetAllTAsync();
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("No cargo operations found.");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _cargoOperationService.GetTByIdAsync(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound("Cargo operation not found.");
        }
        [HttpPost]
        public async Task<IActionResult> Add(CreateCargoOperationDto createCargoOperationDto)
        {
            if (createCargoOperationDto == null || string.IsNullOrEmpty(createCargoOperationDto.Barcode))
            {
                return BadRequest("Invalid cargo operation data.");
            }
            var cargoOperation = new CargoOperation
            {
                Barcode = createCargoOperationDto.Barcode,
                Description = createCargoOperationDto.Description,
                OperationDate = createCargoOperationDto.OperationDate
            };
            await _cargoOperationService.AddTAsync(cargoOperation);
            return Ok("Cargo operation added successfully.");
        }
        [HttpPut]
        public async Task<IActionResult> Update(UpdateCargoOperationDto updateCargoOperationDto)
        {
            if (updateCargoOperationDto == null || string.IsNullOrEmpty(updateCargoOperationDto.Barcode))
            {
                return BadRequest("Invalid cargo operation data.");
            }
            var cargoOperation = new CargoOperation
            {
                CargoOperationId = updateCargoOperationDto.CargoOperationId,
                Barcode = updateCargoOperationDto.Barcode,
                Description = updateCargoOperationDto.Description,
                OperationDate = updateCargoOperationDto.OperationDate
            };
            await _cargoOperationService.UpdateTAsync(cargoOperation);
            return Ok("Cargo operation updated successfully.");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var cargoOperation = await _cargoOperationService.GetTByIdAsync(id);
            if (cargoOperation == null)
            {
                return NotFound("Cargo operation not found.");
            }
            await _cargoOperationService.DeleteTAsync(id);
            return Ok("Cargo operation deleted successfully.");
        }
    }
}
