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
        private Room assignedRoom;
        private Queue<string> workQueue;

        public int ID { get { return id; } }
        public string State { get; set; }
        public Room AssignedRoom { get { return assignedRoom; } }

        public Waiter()
        {

        }
        public Waiter(Room assignedRoom)
        {
            /*count++;
            this.id = count;*/
            this.assignedRoom = assignedRoom;
        }

        public Order GetOrder(Party party)
        {
            return party.Order();
        }

        public Order SendOrder(Order order)
        {
            //TODO
            return null;
        }

        public void DeliverOrder(Order order)
        {
           //TODO
        }
    }
}
