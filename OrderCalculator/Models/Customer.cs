using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderCalculator.Models
{
    public class Customer
    {
        public int Id { get; set; } = 1;
        public string Name { get; set; } = "";
        public State State { get; set; }
    }

    public interface ICustomerController
    {
        Customer GetCustomer(int customerId);
    }

    public class CustomerController : ICustomerController
    {
        private IList<Customer> GetAllCustomers()
        {
            try
            {
                return new List<Customer>()
                {
                    new Customer(){ Id=1, Name= "Pawan", State = State.GA },
                    new Customer(){ Id=2, Name= "Harshal", State = State.FL },
                    new Customer(){ Id=3, Name= "Scott", State = State.NY },
                    new Customer(){ Id=4, Name= "Kyle", State = State.NM },
                    new Customer(){ Id=5, Name= "Boyd", State = State.NV }
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Customer GetCustomer(int customerId)
        {
            try
            {
                return GetAllCustomers().Where(c => c.Id == customerId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
