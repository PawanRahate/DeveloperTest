using System;
using System.Collections.Generic;
using System.Linq;

namespace SolidOrderCalculator
{
    public class ProductController : IProductController
    {
        private IList<ProductExtension> GetAllProducts()
        {
            try
            {
                return new List<ProductExtension>()
                {
                    new ProductExtension(){ Id=1, ClientId = 1, Name="Cricket Bat", Description="This is Cricket Bat", Price=100, Quantity =1, LuxuryStates= new List<State>(){ } },
                    new ProductExtension(){ Id=2, ClientId = 2, Name="Hyundai Car", Description="This is Hyundai Car", Price=1000, Quantity =1, LuxuryStates= new List<State>(){ State.FL, State.GA}},
                    new ProductExtension(){ Id=3, ClientId = 3, Name="Product 3", Description="This is Product 3", Price=30, Quantity =1, LuxuryStates= new List<State>(){ State.FL, State.GA}},
                    new ProductExtension(){ Id=4, ClientId = 4, Name="Product 4", Description="This is Product 4", Price=40, Quantity =1, LuxuryStates= new List<State>(){ State.FL, State.GA}},
                    new ProductExtension(){ Id=5, ClientId = 5, Name="Product 5", Description="This is Product 5", Price=50, Quantity =1, LuxuryStates= new List<State>(){ }},
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProductExtension GetProduct(int productId)
        {
            try
            {
                return GetAllProducts().Where(p => p.Id == productId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IList<ProductExtension> GetProducts(int[] productIds)
        {
            try
            {
                return GetAllProducts().Where(p => productIds.Contains(p.Id)).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
