using System;
using System.Collections.Generic;
using System.Linq;

namespace SolidOrderCalculator
{
    public class CouponController : ICouponController
    {
        private IList<Coupon> GetCoupons()
        {
            try
            {
                return new List<Coupon>()
                {
                   new Coupon(){ ProductId=1, DiscountPercentage = 20, StartDateTime = new DateTime(2019,4,25,0,0,0), EndDateTime= new DateTime(2019,5,30,0,0,0) },
                   new Coupon(){ ProductId=2, DiscountPercentage = 30, StartDateTime = new DateTime(2019,4,25,0,0,0), EndDateTime= new DateTime(2019,5,30,0,0,0) },
                   new Coupon(){ ProductId=3, DiscountPercentage = 14.22m, StartDateTime = new DateTime(2019,4,25,0,0,0), EndDateTime= new DateTime(2019,5,30,0,0,0) },
                   new Coupon(){ ProductId=4, DiscountPercentage = 15.22m, StartDateTime = new DateTime(2019,4,25,0,0,0), EndDateTime= new DateTime(2019,5,30,0,0,0) },
                   new Coupon(){ ProductId=5, DiscountPercentage = 16.22m, StartDateTime = new DateTime(2019,4,25,0,0,0), EndDateTime= new DateTime(2019,5,30,0,0,0) }
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Coupon GetProductCoupon(int productId)
        {
            try
            {
                return GetCoupons().Where(c => c.ProductId == productId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
