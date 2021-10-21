using System;
using System.Threading;

namespace JCsDiner
{
    public class Customer
    {
        private int appetizers;
        private int platers;
        public string State { get; set; }
        public int Appetizers { get { return appetizers; } }
        public int Platers { get { return platers; } }

        public (int appetizers,int platers) Order()
        {
            var rand = new Random();
            appetizers = rand.Next(3);
            platers = rand.Next(1, 3);
            return (appetizers, platers);
        }
        public bool Eat()
        {
            //wait about 8 min per appetizer, 20 min per platter
            return true;
        }
    }
}