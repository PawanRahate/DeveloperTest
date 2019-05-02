using System;
using System.Collections.Generic;
using System.Linq;

namespace SolidOrderCalculator
{
    public class OrderController : IOrderController
    {
        public OrderExtension CalculateOrder(Order order)
        {
            try
            {
                OrderExtension orderExtension = new OrderExtension();
                if (order != null)
                {
                    orderExtension = MapOrder(order);
                    var productCosts = orderExtension.Products
                                        .GroupBy(p => p.Id)
                                        .Select(g => new ProductExtension()
                                        {
                                            Id = g.Key,
                                            ClientId = g.Select(p => p.ClientId).FirstOrDefault(),
                                            LuxuryStates = g.Select(p => p.LuxuryStates).FirstOrDefault(),
                                            SaleValue = g.Sum(p => p.Price) * g.Sum(p => p.Quantity),
                                            OrderTax = orderExtension.OrderTax
                                        })
                                        .ToList();

                    if (order.Offers.Coupons != null || order.Offers.Promotion != null)
                    {
                        orderExtension = RunBusinessRuleEngine(orderExtension, productCosts);
                    }
                    else
                    {
                        // Order with no coupon and discount

                        var totalCost = productCosts.Sum(p => p.SaleValue);
                        var totalOrderTax = totalCost * (orderExtension.OrderTax / 100);
                        var netOrderCost = totalCost + totalOrderTax;

                        orderExtension.TotalCost = netOrderCost.ToDecimal2Places();
                        orderExtension.TotalOrderTax = totalOrderTax.ToDecimal2Places();
                        orderExtension.TotalPreTaxCost = totalCost.ToDecimal2Places();
                    }
                }
                return orderExtension;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private OrderExtension RunBusinessRuleEngine(OrderExtension orderExtension, List<ProductExtension> productCosts)
        {
            try
            {
                if (productCosts.Count > 0)
                {
                    var totalCost = 0m;
                    var totalOrderTax = 0m;
                    var netOrderCost = 0m;

                    foreach (var product in productCosts)
                    {
                        IClientController clientController = new ClientController();
                        var client = clientController.GetClient(product.ClientId);
                        switch (client.State)
                        {
                            case State.GA:
                            case State.NY:
                                var orderResults = CalculateOrderCost(product, orderExtension);
                                totalCost += orderResults[0];
                                totalOrderTax += orderResults[1];
                                netOrderCost += orderResults[2];
                                break;
                            case State.FL:
                            case State.NM:
                            case State.NV:
                                orderResults = CalculateOrderCost(product, orderExtension, true);
                                totalCost += orderResults[0];
                                totalOrderTax += orderResults[1];
                                netOrderCost += orderResults[2];
                                break;
                        }
                    }

                    orderExtension.TotalCost = netOrderCost.ToDecimal2Places();
                    orderExtension.TotalOrderTax = totalOrderTax.ToDecimal2Places();
                    orderExtension.TotalPreTaxCost = totalCost.ToDecimal2Places();
                }

                return orderExtension;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private decimal[] CalculateOrderCost(ProductExtension product, OrderExtension orderExtension, bool taxBeforeDiscount = false)
        {
            try
            {
                decimal[] results = new decimal[3];
                var productCost = product.SaleValue;
                var totalCost = productCost;
                var totalProductTax = 0m;
                var netProductost = 0m;

                if (taxBeforeDiscount)
                {
                    if (product.LuxuryStates.Count > 0)
                        totalProductTax = productCost * ((product.OrderTax / 100) * 2);
                    else
                        totalProductTax = productCost * ((product.OrderTax / 100));

                    netProductost = ApplyCouponDiscount(orderExtension, product.Id, productCost, totalProductTax);
                    netProductost = ApplyPromotionalDiscount(orderExtension, netProductost);
                }
                else
                {
                    netProductost = ApplyCouponDiscount(orderExtension, product.Id, productCost, totalProductTax);
                    netProductost = ApplyPromotionalDiscount(orderExtension, netProductost);

                    totalCost = netProductost;

                    if (product.LuxuryStates.Count > 0)
                        totalProductTax = netProductost * ((product.OrderTax / 100) * 2);
                    else
                        totalProductTax = netProductost * ((product.OrderTax / 100));
                }

                results[0] = totalCost; results[1] = totalProductTax; results[2] = netProductost;
                return results;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private decimal ApplyPromotionalDiscount(OrderExtension orderExtension, decimal netProductost)
        {
            try
            {
                if (orderExtension.Offers.Promotion != null)
                {
                    if (orderExtension.Offers.Promotion.StartDateTime <= orderExtension.OrderDateTime
                        && orderExtension.Offers.Promotion.EndDateTime >= orderExtension.OrderDateTime)
                    {
                        var promotionValue = netProductost * (orderExtension.Offers.Promotion.DiscountPercentage / 100);
                        netProductost -= promotionValue;
                    }
                }
                return netProductost;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private decimal ApplyCouponDiscount(OrderExtension orderExtension, int productId, decimal totalCost, decimal totalProductTax)
        {
            try
            {
                var netProductost = 0m;
                if (orderExtension.Offers.Coupons != null)
                {
                    var coupon = orderExtension.Offers.Coupons.Where(c => c.ProductId == productId).FirstOrDefault();
                    if (coupon.StartDateTime <= orderExtension.OrderDateTime && coupon.EndDateTime >= orderExtension.OrderDateTime)
                    {
                        var couponValue = totalCost * (coupon.DiscountPercentage / 100);
                        netProductost = totalCost + totalProductTax - couponValue;
                    }
                    else
                        netProductost = totalCost + totalProductTax;
                }
                return netProductost;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private OrderExtension MapOrder(Order order)
        {
            try
            {
                return new OrderExtension()
                {
                    Products = order.Products,
                    Offers = order.Offers,
                    OrderTax = order.OrderTax,
                    OrderDateTime = order.OrderDateTime
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
