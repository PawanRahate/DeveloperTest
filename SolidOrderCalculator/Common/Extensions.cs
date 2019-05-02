using System;

namespace SolidOrderCalculator
{
    public static class Extensions
    {
        public static decimal ToDecimal2Places(this object value)
        {
            return Convert.ToDecimal(string.Format("{0:0.00}", value));
        }
    }
}
