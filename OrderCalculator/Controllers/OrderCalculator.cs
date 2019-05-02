using OrderCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderCalculator.Controllers
{
    //public class OrderCalculator
    //{
    //    public virtual OrderExtension CalculateTax(Order order)
    //    {
    //        try
    //        {
    //            return new OrderExtension() { TotalCost = 0, TotalOrderTax = 0, TotalPreTaxCost = 0 };
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }
    //}

    //public class OrderDiscountCalculator : OrderCalculator
    //{
    //    public override OrderExtension CalculateTax(Order order)
    //    {
    //        return base.CalculateTax(order);
    //    }

    //    public OrderExtension CalculateTaxWithDiscount(Order order)
    //    {
    //        return new OrderExtension() { TotalCost = 0, TotalOrderTax = 0, TotalPreTaxCost = 0 };
    //    }
    //}

    //public class calc
    //{
    //    void donow()
    //    {
    //        OrderDiscountCalculator calculator = new OrderDiscountCalculator();
    //        OrderExtension orderExtension = new OrderExtension();

    //        calculator.CalculateTaxWithDiscount(orderExtension);
    //        calculator.CalculateTax(orderExtension);
    //    }
    //}
}
