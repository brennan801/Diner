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

        public Party(int id, int averagePartySize)
        {
            this.ID = id;
            this.State = new PartyWaitingInLobby(this);
            this.Customers = generateRandomCustomers(averagePartySize);
        }

        private int generateRandomCustomers(int average)
        {
            var rand = new Random();
            var customerRandNumber = rand.Next(100);
            if(customerRandNumber < 5)
            {
                var partySize =  Math.Abs(average - 3);
                if(partySize == 0)
                {
                    partySize++;
                }
                return partySize;
            }
            if(customerRandNumber < 10)
            {
                var partySize = Math.Abs(average - 2);
                if (partySize == 0)
                {
                    partySize++;
                }
                return partySize;
            }
            if(customerRandNumber < 30)
            {
                var partySize = Math.Abs(average - 1);
                if (partySize == 0)
                {
                    partySize++;
                }
                return partySize;
            }
            if (customerRandNumber < 70)
            {
                return average;
            }
            if (customerRandNumber < 90)
            {
                return average + 1;
            }
            if (customerRandNumber < 95)
            {
                return average + 2;
            }
            else return average + 3;
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