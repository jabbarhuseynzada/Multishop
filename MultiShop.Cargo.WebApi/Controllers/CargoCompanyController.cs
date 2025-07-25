using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.Business.Abstract;
using MultiShop.Cargo.DTOLayer.Dtos.CargoCompanyDtos;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CargoCompanyController(ICargoCompanyService cargoCompanyService) : ControllerBase
    {
        private readonly ICargoCompanyService _cargoCompanyService = cargoCompanyService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _cargoCompanyService.GetAllTAsync();
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _cargoCompanyService.GetTByIdAsync(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Add(CreateCargoCompanyDto createCargoCompanyDto)
        {
            if (createCargoCompanyDto == null || string.IsNullOrEmpty(createCargoCompanyDto.CargoCompanyName))
            {
                return BadRequest("Invalid cargo company data.");
            }
            var cargoCompany = new CargoCompany
            {
                CargoCompanyName = createCargoCompanyDto.CargoCompanyName
            };
            await _cargoCompanyService.AddTAsync(cargoCompany);
            return Ok("Cargo company added successfully.");
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCargoCompanyDto updateCargoCompanyDto)
        {
            if (updateCargoCompanyDto == null || string.IsNullOrEmpty(updateCargoCompanyDto.CargoCompanyName))
            {
                return BadRequest("Invalid cargo company data.");
            }
            var cargoCompany = await _cargoCompanyService.GetTByIdAsync(updateCargoCompanyDto.CargoCompanyId);
            if (cargoCompany == null)
            {
                return NotFound("Cargo company not found.");
            }
            cargoCompany.CargoCompanyName = updateCargoCompanyDto.CargoCompanyName;
            await _cargoCompanyService.UpdateTAsync(cargoCompany);
            return Ok("Cargo company updated successfully."); 
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var cargoCompany = await _cargoCompanyService.GetTByIdAsync(id);
            if (cargoCompany == null)
            {
                return NotFound("Cargo company not found.");
            }
            await _cargoCompanyService.DeleteTAsync(id);
            return Ok("Cargo company deleted successfully.");
        }
    }

}
