using Dapper;
using MultiShop.Discount.Context;
using MultiShop.Discount.Dtos.CouponDtos;

namespace MultiShop.Discount.Services.CouponService;
public class DiscountService : IDiscountService
{
    private readonly DapperContext _context;
    public DiscountService(DapperContext context)
    {
        _context = context;
    }
    public async Task CreateCouponAsync(CreateCouponDto coupon)
    {
        var query = "INSERT INTO Coupons (Code, Rate, IsActive, ValidDate) VALUES (@Code, @Rate, @IsActive, @ValidDate)";
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(query, coupon);
    }
    public async Task UpdateCouponAsync(UpdateCouponDto coupon)
    {
        var query = "UPDATE Coupons SET Code = @Code, Rate = @Rate, IsActive = @IsActive, ValidDate = @ValidDate WHERE CouponId = @CouponId";
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(query, coupon);
    }
    public async Task DeleteCouponAsync(int couponId)
    {
        var query = "DELETE FROM Coupons WHERE CouponId = @CouponId";
        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(query, new { CouponId = couponId });
    }
    public async Task<GetByIdCouponDto> GetByIdAsync(int couponId)
    {
        var query = "SELECT * FROM Coupons WHERE CouponId = @CouponId";
        using var connection = _context.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<GetByIdCouponDto>(query, new { CouponId = couponId });
    }
    public async Task<List<ResultCouponDto>> GetAllAsync()
    {
        var query = "SELECT * FROM Coupons";
        using var connection = _context.CreateConnection();
        return (await connection.QueryAsync<ResultCouponDto>(query)).ToList();
    }
}
