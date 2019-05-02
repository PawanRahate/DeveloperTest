using System.Collections.Generic;

namespace SolidOrderCalculator
{
    public class Product : GlobalEntity
    {
        public decimal Price { get; set; }
        public int ClientId { get; set; }
    }

    public class ProductExtension : Product
    {
        public IList<State> LuxuryStates{ get; set; }
        public int Quantity { get; set; }

        public decimal SaleValue { get; set; }
        public decimal OrderTax { get; set; }
    }
}
