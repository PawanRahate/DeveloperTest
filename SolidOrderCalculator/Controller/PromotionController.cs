using System;
using System.Collections.Generic;

namespace SolidOrderCalculator
{
    public class PromotionController : IPromotionController
    {
        public List<Promotion> GetPromotions()
        {
            try
            {
                return new List<Promotion>()
                {
                   new Promotion(){ DiscountPercentage = 20, StartDateTime = new DateTime(2019,4,25,0,0,0), EndDateTime= new DateTime(2019,5,2,0,0,0) },
                   new Promotion(){ DiscountPercentage = 30, StartDateTime = new DateTime(2019,4,25,0,0,0), EndDateTime= new DateTime(2019,5,2,0,0,0) },
                   new Promotion(){ DiscountPercentage = 14.22m, StartDateTime = new DateTime(2019,4,25,0,0,0), EndDateTime= new DateTime(2019,5,1,0,0,0) },
                   new Promotion(){ DiscountPercentage = 15.22m, StartDateTime = new DateTime(2019,4,25,0,0,0), EndDateTime= new DateTime(2019,5,1,0,0,0) },
                   new Promotion(){ DiscountPercentage = 16.22m, StartDateTime = new DateTime(2019,4,25,0,0,0), EndDateTime= new DateTime(2019,5,1,0,0,0) }
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
