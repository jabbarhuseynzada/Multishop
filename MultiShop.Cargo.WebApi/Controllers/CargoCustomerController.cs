using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Cargo.Business.Abstract;
using MultiShop.Cargo.DTOLayer.Dtos.CargoCustomerDtos;
using MultiShop.Cargo.EntityLayer.Concrete;

namespace MultiShop.Cargo.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CargoCustomerController(ICargoCustomerService cargoCustomerService) : ControllerBase
    {
        private readonly ICargoCustomerService _cargoCustomerService = cargoCustomerService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _cargoCustomerService.GetAllTAsync();
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("No cargo customers found.");
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _cargoCustomerService.GetTByIdAsync(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound("Cargo customer not found.");
        }
        [HttpPost]
        public async Task<IActionResult> Add(CreateCargoCustomerDto createCargoCustomerDto)
        {
            if (createCargoCustomerDto == null || string.IsNullOrEmpty(createCargoCustomerDto.Name))
            {
                return BadRequest("Invalid cargo customer data.");
            }
            var cargoCustomer = new CargoCustomer
            {
                Name = createCargoCustomerDto.Name,
                Surname = createCargoCustomerDto.Surname,
                Email = createCargoCustomerDto.Email,
                Phone = createCargoCustomerDto.Phone,
                District = createCargoCustomerDto.District,
                City = createCargoCustomerDto.City,
                Address = createCargoCustomerDto.Address,
                UserCustomerId = createCargoCustomerDto.UserCustomerId
            };
            await _cargoCustomerService.AddTAsync(cargoCustomer);
            return Ok("Cargo customer added successfully.");
        }
        [HttpPut]
        public async Task<IActionResult> Update(UpdateCargoCustomerDto updateCargoCustomerDto)
        {
            if (updateCargoCustomerDto == null || string.IsNullOrEmpty(updateCargoCustomerDto.Name))
            {
                return BadRequest("Invalid cargo customer data.");
            }
            var cargoCustomer = await _cargoCustomerService.GetTByIdAsync(updateCargoCustomerDto.CargoCustomerId);
            if (cargoCustomer == null)
            {
                return NotFound("Cargo customer not found.");
            }
            cargoCustomer.Name = updateCargoCustomerDto.Name;
            cargoCustomer.Surname = updateCargoCustomerDto.Surname;
            cargoCustomer.Email = updateCargoCustomerDto.Email;
            cargoCustomer.Phone = updateCargoCustomerDto.Phone;
            cargoCustomer.District = updateCargoCustomerDto.District;
            cargoCustomer.City = updateCargoCustomerDto.City;
            cargoCustomer.Address = updateCargoCustomerDto.Address;
            cargoCustomer.UserCustomerId = updateCargoCustomerDto.UserCustomerId;
            await _cargoCustomerService.UpdateTAsync(cargoCustomer);
            return Ok("Cargo customer updated successfully.");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var cargoCustomer = await _cargoCustomerService.GetTByIdAsync(id);
            if (cargoCustomer == null)
            {
                return NotFound("Cargo customer not found.");
            }
            await _cargoCustomerService.DeleteTAsync(id);
            return Ok("Cargo customer deleted successfully.");
        }
    }
}
