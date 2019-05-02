namespace SolidOrderCalculator
{
    public interface IOrderController
    {
        OrderExtension CalculateOrder(Order order);
    }
}
