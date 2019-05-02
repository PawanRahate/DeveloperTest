using System;
using System.Collections.Generic;

namespace SolidOrderCalculator
{
    public abstract class Discount : GlobalEntity
    {
        public decimal DiscountPercentage { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
    }

    public class Coupon : Discount
    {
        public int ProductId { get; set; }
    }

    public class Promotion : Discount
    {

    }

    public class Offers 
    {
        public IList<Coupon> Coupons { get; set; }
        public Promotion Promotion { get; set; }
    }
}
