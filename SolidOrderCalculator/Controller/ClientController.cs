using System;
using System.Collections.Generic;
using System.Linq;

namespace SolidOrderCalculator
{
    public class ClientController : IClientController
    {
        public Client GetClient(int clientId)
        {
            try
            {
                return GetClients().Where(c => c.Id == clientId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IList<Client> GetClients()
        {
            try
            {
                return new List<Client>()
                {
                    new Client(){ Id = 1, Name = "Walmart", Description = "Walmart Client", State = State.FL},
                    new Client(){ Id = 2, Name = "Costco", Description = "Costco Client", State = State.GA},
                    new Client(){ Id = 3, Name = "Walgreens", Description = "Walgreens Client", State = State.NM},
                    new Client(){ Id = 4, Name = "Manpasand", Description = "Manpasand Client", State = State.NV},
                    new Client(){ Id = 5, Name = "Sam's", Description = "Sam's Client", State = State.NY}
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
