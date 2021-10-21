using System;
using System.Collections.Generic;

namespace JCsDiner
{
    public class Party
    {
        public List<Customer> Customers { get; internal set; }

        public Party(int numOfCustomers)
        {
            Customers = new List<Customer>();
           for(int i = 0; i < numOfCustomers; i++)
            {
                Customers.Add(new Customer());
            }
        }

        public Order Order()
        {
            Order order = new Order(this);
            foreach(Customer customer in Customers)
            {
                (int appitizers, int platers) = customer.Order();
                order.Appetizers += appitizers;
                order.Platers += platers;
            }
            return order;
        }
    }
}