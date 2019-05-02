using System;
using System.Collections.Generic;

namespace SolidOrderCalculator
{
    public class Order
    {
        public IList<ProductExtension> Products { get; set; }
        public decimal OrderTax { get; set; } = 10.12m;
        public Offers Offers { get; set; }
        public DateTime OrderDateTime { get; set; }
    }

    public class OrderExtension : Order
    {
        public decimal TotalCost { get; set; } = 0m;
        public decimal TotalPreTaxCost { get; set; } = 0m;
        public decimal TotalOrderTax { get; set; } = 0m;
    }
}
