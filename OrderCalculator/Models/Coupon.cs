using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderCalculator.Models
{
    public abstract class Discount
    {
        public decimal DiscountPercent { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
    }

    public class Coupon: Discount
    {
        public int ProductId { get; set; }
    }

    public interface ICouponController
    {
        IList<Coupon> GetProductCoupons(int productId);
        Coupon GetProductCoupon(int productId);
    }

    public class CouponController : ICouponController
    {
        private IList<Coupon> GetAllCoupons()
        {
            try
            {
                return new List<Coupon>()
                {
                   new Coupon(){ ProductId=1, DiscountPercent = 20, StartDateTime = new DateTime(2019,4,25,0,0,0), EndDateTime= new DateTime(2019,5,2,0,0,0) },
                   new Coupon(){ ProductId=2, DiscountPercent = 30, StartDateTime = new DateTime(2019,4,25,0,0,0), EndDateTime= new DateTime(2019,5,2,0,0,0) },
                   new Coupon(){ ProductId=3, DiscountPercent = 14.22m, StartDateTime = new DateTime(2019,4,25,0,0,0), EndDateTime= new DateTime(2019,5,1,0,0,0) },
                   new Coupon(){ ProductId=4, DiscountPercent = 15.22m, StartDateTime = new DateTime(2019,4,25,0,0,0), EndDateTime= new DateTime(2019,5,1,0,0,0) },
                   new Coupon(){ ProductId=5, DiscountPercent = 16.22m, StartDateTime = new DateTime(2019,4,25,0,0,0), EndDateTime= new DateTime(2019,5,1,0,0,0) }
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IList<Coupon> GetProductCoupons(int productId)
        {
            try
            {
                return GetAllCoupons().Where(c => c.ProductId == productId).ToList();
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
                return GetAllCoupons().Where(c => c.ProductId == productId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
