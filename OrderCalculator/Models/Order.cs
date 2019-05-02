using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderCalculator.Models
{
    public class Order
    {
        public IList<Product> Products { get; set; }
        public Customer Customer { get; set; }
        public DateTime OrderDateTime { get; set; }
        public decimal OrderTax { get; set; } = 10.12m;
        public decimal TotalCost { get; set; } = 0m;
        public decimal TotalPreTaxCost { get; set; } = 0m;
        public decimal TotalOrderTax { get; set; } = 0m;
        public Coupon OrderCoupon { get; set; }
    }

    public interface IOrderController
    {
        Order CalculateOrder(Order order);
    }

    public class OrderController : IOrderController
    {
        public Order CalculateOrder(Order order)
        {
            try
            {
                Order _order = new Order();
                if (order.Products.Count > 0)
                {
                    //the tax must be calculated prior to applying the discount: FL, NM, and NV
                    switch (order.Customer.State)
                    {
                        case State.GA:
                        case State.NY:
                            _order = CalculateTax(order);
                            break;
                        case State.FL:
                        case State.NM:
                        case State.NV:
                            _order = CalculateTax(order, true);
                            break;
                    }
                }
                return _order;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Order CalculateTax(Order order, bool taxBeforeDiscount = false)
        {
            try
            {
                var orderCost = 0m;
                var orderTax = 0m;
                var orderDateTime = order.OrderDateTime;

                if (taxBeforeDiscount)
                {
                    foreach (var product in order.Products)
                    {
                        var quantity = product.Quantity; // check if quantity is 0
                        var price = product.Price;
                        var grossCost = (price * quantity);
                        var couponDiscount = CalculateCouponDiscount(orderDateTime, product.Id);

                        var productDiscount = grossCost * (couponDiscount / 100);
                        var productTax = grossCost * (order.OrderTax / 100);

                        orderCost += grossCost + productTax - productDiscount;
                    }
                }
                else
                {
                    foreach (var product in order.Products)
                    {
                        var quantity = product.Quantity; // check if quantity is 0
                        var price = product.Price;
                        var grossCost = (price * quantity);
                        var couponDiscount = CalculateCouponDiscount(orderDateTime, product.Id);

                        var productDiscount = grossCost * (couponDiscount / 100);
                        grossCost -= productDiscount;

                        var productTax = grossCost * (order.OrderTax / 100);

                        orderCost += grossCost + productTax;
                    }
                }

                var promotionalDiscount = CalculatePromotionalDiscount(orderDateTime);

                order.TotalCost = orderCost + orderTax;
                order.TotalOrderTax = orderTax;
                order.TotalPreTaxCost = orderCost;
                return order;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private decimal CalculatePromotionalDiscount(DateTime orderDateTime)
        {
            try
            {
                var promotionDiscount = 0m;

                IPromotionController promotionController = new PromotionController();
                var promotion = promotionController.GetPromotionalDiscount();
                
                if (orderDateTime >= promotion.StartDateTime && orderDateTime <= promotion.EndDateTime)
                    promotionDiscount = promotion.DiscountPercent;

                return promotionDiscount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private decimal CalculateCouponDiscount(DateTime orderDateTime, int id)
        {
            try
            {
                ICouponController couponController = new CouponController();
                var coupon = couponController.GetProductCoupon(id);
                var couponDiscount = 0m;
                if (orderDateTime >= coupon.StartDateTime && orderDateTime <= coupon.EndDateTime)
                {
                    couponDiscount = coupon.DiscountPercent;
                }
                return couponDiscount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private decimal CalculateCouponsDiscount(DateTime orderDateTime, int id)
        {
            try
            {
                ICouponController couponController = new CouponController();
                var coupons = couponController.GetProductCoupons(id);
                var couponDiscount = 0m;
                foreach (var coupon in coupons)
                {
                    if (orderDateTime >= coupon.StartDateTime && orderDateTime <= coupon.EndDateTime)
                    {
                        couponDiscount += coupon.DiscountPercent;
                    }
                }
                return couponDiscount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
