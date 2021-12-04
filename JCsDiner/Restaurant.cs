using System.Collections.Generic;

namespace JCsDiner
{
    public class Restaurant
    {
        public List<Room> Rooms = new List<Room> { new Room(4), new Room(4), new Room(4) };
        public List<Order> CurrentOrders;
        public List<Party> CurrentParties;

        public Restaurant()
        {
            CurrentOrders = new List<Order>();
            CurrentParties = new List<Party>();
        }
    }
}