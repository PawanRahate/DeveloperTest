using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderCalculator.Models
{
    public class Product
    {
        public int Id { get; set; } = 1;
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public decimal Price { get; set; } = 1;
        public int Quantity { get; set; } = 1;
        public List<State> LuxuryItemState{ get; set; }
        public decimal CouponDiscount { get; set; } = 0m;
        public decimal PromotionalDiscount { get; set; } = 0m;
    }

    public interface IProductController
    {
        IList<Product> GetProducts(int[] productIds);
        Product GetProduct(int productId);
    }

    public class ProductController : IProductController
    {
        public IList<Product> GetAllProducts()
        {
            try
            {
                return new List<Product>()
                {
                    new Product(){ Id=1, Name="Cricket Bat", Description="This is Cricket Bat", Price=100, Quantity =1, LuxuryItemState= new List<State>(){ } },
                    new Product(){ Id=2, Name="Hyundai Car", Description="This is Hyundai Car", Price=1000, Quantity =1, LuxuryItemState= new List<State>(){ State.FL, State.GA}},
                    new Product(){ Id=3, Name="Product 3", Description="This is Product 3", Price=30, Quantity =1, LuxuryItemState= new List<State>(){ State.FL, State.GA}},
                    new Product(){ Id=4, Name="Product 4", Description="This is Product 4", Price=40, Quantity =1, LuxuryItemState= new List<State>(){ State.FL, State.GA}},
                    new Product(){ Id=5, Name="Product 5", Description="This is Product 5", Price=50, Quantity =1, LuxuryItemState= new List<State>(){ }},
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Product GetProduct(int productId)
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
        public IList<Product> GetProducts(int[] productIds)
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
