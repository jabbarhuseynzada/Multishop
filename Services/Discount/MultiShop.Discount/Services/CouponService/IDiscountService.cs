using MultiShop.Discount.Dtos.CouponDtos;
using MultiShop.Discount.Entities;

namespace MultiShop.Discount.Services.CouponService;
public interface IDiscountService
{
    public Task CreateCouponAsync(CreateCouponDto coupon);
    public Task UpdateCouponAsync(UpdateCouponDto coupon);
    public Task DeleteCouponAsync(int couponId);
    public Task<GetByIdCouponDto> GetByIdAsync(int couponId);
    public Task<List<ResultCouponDto>> GetAllAsync();
}
