using System;
using System.Collections.Generic;

namespace JCsDiner
{
    public class Party
    {
        public int Customers { get; internal set; }
        public PartyState State { get; set; }
        public Order Order { get; set; }
        public Table Table { get; set; }
        public int EnterLobbyTime { get; set; }
        public int ExitLobbyTime { get; set; }
        public int ID { get; set; }

        public Party(int numOfCustomers, int id)
        {
            this.ID = id;
            Customers = numOfCustomers;
            this.State = new PartyWaitingInLobby(this);
        }

        public Party(int id)
        {
            this.ID = id;
            this.State = new PartyWaitingInLobby(this);
            this.Customers = generateRandomCustomers();
        }

        private int generateRandomCustomers()
        {
            var rand = new Random();
            var customerRandNumber = rand.Next(100);
            if(customerRandNumber < 10)
            {
                return 1;
            }
            if(customerRandNumber < 40)
            {
                return 2;
            }
            if(customerRandNumber < 60)
            {
                return 3;
            }
            if (customerRandNumber < 80)
            {
                return 4;
            }
            if (customerRandNumber < 85)
            {
                return 5;
            }
            if (customerRandNumber < 90)
            {
                return 6;
            }
            if (customerRandNumber < 95)
            {
                return 10;
            }
            if (customerRandNumber < 99)
            {
                return 13;
            }
            else
            {
                return 15;
            }
        }

        public Order CreateOrder()
        {
            Order order = new Order(Table);
            for(int i = 0; i < Customers; i++)
            {
                (int appetizers, int platers) = CustomerOrder();
                order.Appetizers += appetizers;
                order.Platers += platers;
            }
            this.Order = order;
            return order;
        }
        public (int appetizers, int platers) CustomerOrder()
        {
            int appetizers;
            int platers;
            var rand = new Random();
            var appitizerRandomNumber = rand.Next(100);
            var platerRandomNumber = rand.Next(100);
            if (appitizerRandomNumber < 60)
            {
                appetizers = 0;
            }
            else if (appitizerRandomNumber < 90)
            {
                appetizers = 1;
            }
            else appetizers = 2;

            platers = platerRandomNumber < 90 ? 1 : 2;

            return (appetizers, platers);
        }

        public void Run1(Restaurant resturant)
        {
            State.Run1();
        }
    }
}