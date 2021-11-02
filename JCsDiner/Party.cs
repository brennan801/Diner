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

        public Party()
        {
            this.State = "new";
            this.Customers = generateRandomCustomers();
        }

        private List<Customer> generateRandomCustomers()
        {
            var rand = new Random();
            var customerRandNumber = rand.Next(100);
            if(customerRandNumber < 10)
            {
                return new List<Customer>() { new Customer() };
            }
            if(customerRandNumber < 40)
            {
                List<Customer> customers = new();
                for (int i = 0; i < 2; i++)
                {
                    customers.Add(new Customer());
                }
                return customers;
            }
            if(customerRandNumber < 60)
            {
                List<Customer> customers = new();
                for (int i = 0; i < 3; i++)
                {
                    customers.Add(new Customer());
                }
                return customers;
            }
            if (customerRandNumber < 80)
            {
                List<Customer> customers = new();
                for (int i = 0; i < 4; i++)
                {
                    customers.Add(new Customer());
                }
                return customers;
            }
            if (customerRandNumber < 85)
            {
                List<Customer> customers = new();
                for (int i = 0; i < 5; i++)
                {
                    customers.Add(new Customer());
                }
                return customers;
            }
            if (customerRandNumber < 90)
            {
                List<Customer> customers = new();
                for (int i = 0; i < 6; i++)
                {
                    customers.Add(new Customer());
                }
                return customers;
            }
            if (customerRandNumber < 95)
            {
                List<Customer> customers = new();
                for (int i = 0; i < 10; i++)
                {
                    customers.Add(new Customer());
                }
                return customers;
            }
            if (customerRandNumber < 99)
            {
                List<Customer> customers = new();
                for (int i = 0; i < 13; i++)
                {
                    customers.Add(new Customer());
                }
                return customers;
            }
            else
            {
                List<Customer> customers = new();
                for (int i = 0; i < 15; i++)
                {
                    customers.Add(new Customer());
                }
                return customers;
            }
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