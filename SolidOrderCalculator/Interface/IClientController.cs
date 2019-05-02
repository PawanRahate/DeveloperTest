using System.Collections.Generic;

namespace SolidOrderCalculator
{
    public interface IClientController
    {
        IList<Client> GetClients();
        Client GetClient(int clientId);
    }
}
