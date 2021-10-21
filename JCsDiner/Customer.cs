using System;

namespace JCsDiner
{
    public class Customer
    {
        public (int appitizers,int platers) Order()
        {
            var rand = new Random();
            return (rand.Next(3), rand.Next(1,3));
        }
    }
}