﻿namespace MultiShop.Discount.Dtos.CouponDtos;
public class ResultCouponDto
{
    public int CouponId { get; set; }
    public string Code { get; set; }
    public decimal Rate { get; set; }
    public bool IsActive { get; set; }
    public DateTime ValidDate { get; set; }
}
