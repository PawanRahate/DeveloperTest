using Microsoft.VisualStudio.TestTools.UnitTesting;
using SolidOrderCalculator;
using System;
using System.Collections.Generic;

namespace OrderCalculatorTest
{
    [TestClass]
    public class SolidOrderCalculatorTest
    {
        #region Test cases for Client

        [TestMethod]
        public void GetClients()
        {
            IClientController clientController = new ClientController();
            var clients = clientController.GetClients();
            Assert.AreEqual(clients.Count, 5);
        }

        [TestMethod]
        public void GetClient()
        {
            IClientController clientController = new ClientController();
            var client = clientController.GetClient(1);
            Assert.AreEqual(client.State, State.FL);
        }

        #endregion

        #region Calculate Order
        [TestMethod]
        public void CalculateOrder_Simple()
        {
            IOrderController orderController = new OrderController();
            IProductController productController = new ProductController();

            var products = productController.GetProducts(new int[] { 1, 2 });
            products[0].Quantity = 10;
            products[1].Quantity = 200;

            Order order = new Order()
            {
                Products = products,
                Offers = new Offers(),
                OrderTax = 33.33m,
                OrderDateTime = DateTime.Now
            };

            var orderResult = orderController.CalculateOrder(order);

            Assert.AreEqual(267993.30m, orderResult.TotalCost);
            Assert.AreEqual(66993.30m, orderResult.TotalOrderTax);
            Assert.AreEqual(201000.00m, orderResult.TotalPreTaxCost);
        }

        [TestMethod]
        public void CalculateOrder_Discounts()
        {
            IOrderController orderController = new OrderController();
            IProductController productController = new ProductController();
            ICouponController couponController = new CouponController();
            IPromotionController promotionController = new PromotionController();

            List<Coupon> coupons = new List<Coupon>();
            coupons.Add(couponController.GetProductCoupon(1));
            coupons.Add(couponController.GetProductCoupon(2));

            var products = productController.GetProducts(new int[] { 1, 2 });
            products[0].Quantity = 10;
            products[1].Quantity = 200;

            Order order = new Order()
            {
                Products = products,
                Offers = new Offers()
                {
                    Coupons = coupons,
                    Promotion = promotionController.GetPromotions()[0]
                },
                OrderTax = 33.33m,
                OrderDateTime = DateTime.Now
            };

            var orderResult = orderController.CalculateOrder(order);

            Assert.AreEqual(112906.64m, orderResult.TotalCost);
            Assert.AreEqual(74992.50m, orderResult.TotalOrderTax);
            Assert.AreEqual(113000.00m, orderResult.TotalPreTaxCost);
        }
        #endregion
    }
}
