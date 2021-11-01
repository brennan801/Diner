using System;
using System.Threading;

namespace JCsDiner
{
    public class Customer
    {
        public string State { get; set; }
        public int Appetizers { get; private set; }
        public int Platers { get; private set; }

        public (int appetizers,int platers) Order()
        {
            var rand = new Random();
            var appitizerRandomNumber = rand.Next(100);
            var platerRandomNumber = rand.Next(100);
            if (appitizerRandomNumber < 60)
            {
                Appetizers = 0;
            }
            else if (appitizerRandomNumber < 90)
            {
                Appetizers = 1;
            }
            else Appetizers = 2;

            Platers = platerRandomNumber < 90 ? 1 : 2;

            return (Appetizers, Platers);
        }
        public bool Eat()
        {
            //wait about 8 min per appetizer, 20 min per platter
            return true;
        }
    }
}