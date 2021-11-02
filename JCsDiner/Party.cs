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

        public Party Enter()
        {
            //TODO: Send event 
            this.State = "entered";
            return this;
        }

        public Order Order()
        {
            this.State = "ordering";
            Order order = new Order(this);
            foreach(Customer customer in Customers)
            {
                (int appetizers, int platers) = customer.Order();
                order.Appetizers += appetizers;
                order.Platers += platers;
            }
            this.State = "ordered";
            return order;
        }

        public Party Eat()
        {
            this.State = "eating";
            foreach(Customer customer in Customers)
            {
                customer.Eat();
            }
            this.State = "finished eating";
            return this;
        }

        public Party PayAndLeave()
        {
            //throw paid event
            this.State = "paid and left";
            return this;
        }
    }
}