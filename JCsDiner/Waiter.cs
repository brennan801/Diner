using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCsDiner
{
    public class Waiter
    {
        static int count;
        private readonly int id;
        private string currentState;
        private Room assignedRoom;
        private Queue<string> workQueue;

        public int ID { get { return id; } }
        public string CurrentState { get { return currentState; } }
        public Room AssignedRoom { get { return assignedRoom; } }

        public Waiter(Room assignedRoom)
        {
            count++;
            this.id = count;
            this.assignedRoom = assignedRoom;
        }

        public Order GetOrder(Party party)
        {
            Order newOrder = party.Order();
            return newOrder;
        }
    }
}
