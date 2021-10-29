using System;
using System.Collections.Generic;

namespace JCsDiner
{
    public class Party
    {
        public List<Customer> Customers { get; internal set; }
        public string State { get; set; }

        public Party(int numOfCustomers, string state)
        {
            Customers = new List<Customer>();
           for(int i = 0; i < numOfCustomers; i++)
            {
                Customers.Add(new Customer());
            }
            this.State = state;
        }

        public bool Enter()
        {
            //TODO: this should trigger the host.. somehow. 
            return false;
        }

        public Order Order()
        {
            Order order = new Order(this);
            foreach(Customer customer in Customers)
            {
                (int appetizers, int platers) = customer.Order();
                order.Appetizers += appetizers;
                order.Platers += platers;
            }
            return order;
        }

        public bool Eat()
        {
            //TODO: wait until all customers are done.
            bool success = false;
            foreach(Customer customer in Customers)
            {
                success = customer.Eat() && success;
            }
            return success;
        }

        public bool Pay()
        {
            //TODO: poop
            return false;
        }
    }
}