using OrderCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderCalculatorClient
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                List<Order> orders = new List<Order>();
                DateTime dtOrderDatetime = DateTime.Now;
                decimal decOrderTax = 33.33m;
                int intOrderQuantity = 10;

                ICustomerController customerController = new CustomerController();
                var customer = customerController.GetCustomer(2);

                IProductController productController = new ProductController();
                var products = productController.GetProducts(new int[] { 1, 2 });
                products[0].Quantity = intOrderQuantity;
                products[1].Quantity = 20;

                IOrderController orderController = new OrderController();
                var order = orderController.CalculateOrder(
                    new Order()
                    {
                        Customer = customer,
                        Products = products,
                        OrderDateTime = dtOrderDatetime,
                        OrderTax = decOrderTax
                    });

                orders.Add(order);

                var totalOrderCost = orders.Sum(o => o.TotalCost);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.ReadLine();
        }

        void SeperateProducts()
        {
            List<Order> orders = new List<Order>();
            DateTime dtOrderDatetime = DateTime.Now;
            decimal decOrderTax = 33.33m;
            int intOrderQuantity = 10;

            ICustomerController customerController = new CustomerController();
            var customer = customerController.GetCustomer(1);

            IProductController productController = new ProductController();
            var products = new List<Product>() { productController.GetProduct(1) };
            products.FirstOrDefault().Quantity = intOrderQuantity;

            IOrderController orderController = new OrderController();
            var order = orderController.CalculateOrder(
                new Order()
                {
                    Customer = customer,
                    Products = products,
                    OrderDateTime = dtOrderDatetime,
                    OrderTax = decOrderTax
                });

            orders.Add(order);

            decOrderTax = 20;
            intOrderQuantity = 20;

            //customer = customerController.GetCustomer(1);

            products = new List<Product>() { productController.GetProduct(2) };
            products.FirstOrDefault().Quantity = intOrderQuantity;

            order = orderController.CalculateOrder(
                new Order()
                {
                    Customer = customer,
                    Products = products,
                    OrderDateTime = dtOrderDatetime,
                    OrderTax = decOrderTax
                });

            orders.Add(order);
        }

        static void UI()
        {
            Console.WriteLine("Enter Customer Id: ");
            string strCustomerId = Console.ReadLine();
            int intCustomerId;
            if (int.TryParse(strCustomerId, out intCustomerId))
            {
                ICustomerController customerController = new CustomerController();
                var customer = customerController.GetCustomer(intCustomerId);

                Console.WriteLine("Select Products: ");
                string strProductId = Console.ReadLine();
            }
            else
                Console.WriteLine("Not an integer!");
        }
    }
}
