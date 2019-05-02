using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderCalculator.Models
{
    public class Promotion: Discount
    {

    }

    public interface IPromotionController
    {
        Promotion GetPromotionalDiscount();
    }

    public class PromotionController : IPromotionController
    {
        public Promotion GetPromotionalDiscount()
        {
            try
            {
                return new Promotion()
                {
                    DiscountPercent = 20,
                    StartDateTime = new DateTime(2019, 4, 25, 0, 0, 0),
                    EndDateTime = new DateTime(2019, 5, 2, 0, 0, 0)
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
