using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Discount.Dtos.CouponDtos;
using MultiShop.Discount.Services.CouponService;

namespace MultiShop.Discount.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountService _discountService;
        public DiscountController(IDiscountService discountService)
        {
            _discountService = discountService;
        }
        [HttpPost("CreateCoupon")]
        public async Task<IActionResult> CreateCouponAsync([FromBody] CreateCouponDto coupon)
        {
            await _discountService.CreateCouponAsync(coupon);
            return Ok();
        }
        [HttpPut("UpdateCoupon")]
        public async Task<IActionResult> UpdateCouponAsync([FromBody] UpdateCouponDto coupon)
        {
            await _discountService.UpdateCouponAsync(coupon);
            return Ok();
        }
        [HttpDelete("DeleteCoupon/{couponId}")]
        public async Task<IActionResult> DeleteCouponAsync(int couponId)
        {
            await _discountService.DeleteCouponAsync(couponId);
            return Ok();
        }
        [HttpGet("GetById/{couponId}")]
        public async Task<IActionResult> GetByIdAsync(int couponId)
        {
            var result = await _discountService.GetByIdAsync(couponId);
            return Ok(result);
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _discountService.GetAllAsync();
            return Ok(result);
        }
    }
}
