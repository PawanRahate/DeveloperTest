using System.Collections.Generic;

namespace SolidOrderCalculator
{
    public interface IProductController
    {
        IList<ProductExtension> GetProducts(int[] productIds);
        ProductExtension GetProduct(int productId);
    }
}
