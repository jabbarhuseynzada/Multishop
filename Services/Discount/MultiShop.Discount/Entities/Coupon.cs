﻿namespace MultiShop.Discount.Entities;
public class Coupon
{
    public int CouponId { get; set; }
    public string Code { get; set; }
    public decimal Rate { get; set; }
    public bool IsActive { get; set; }
    public DateTime ValidDate { get; set; }

    //Servis yazilacaq Dapperle
}
