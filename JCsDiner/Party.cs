using System;
using System.Collections.Generic;

namespace JCsDiner
{
    public class Party
    {
        public List<Customer> Customers { get; internal set; }
        public PartyState State { get; set; }
        public Order Order { get; set; }
        public Table Table { get; set; }
        public int EnterLobbyTime { get; set; }
        public int ExitLobbyTime { get; set; }
        public int ID { get; set; }

        public Party(int numOfCustomers, int id)
        {
            this.ID = id;
            Customers = new List<Customer>();
           for(int i = 0; i < numOfCustomers; i++)
            {
                Customers.Add(new Customer());
            }
            this.State = new PartyWaitingInLobby(this);
        }

        public Party(int id)
        {
            this.ID = id;
            this.State = new PartyWaitingInLobby(this);
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

        public Order CreateOrder()
        {
            Order order = new Order(Table);
            foreach(Customer customer in Customers)
            {
                (int appetizers, int platers) = customer.Order();
                order.Appetizers += appetizers;
                order.Platers += platers;
            }
            this.Order = order;
            return order;
        }

        public void Run1(Restaurant resturant)
        {
            State.Run1();
        }
    }
}