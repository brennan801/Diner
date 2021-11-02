using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCsDiner
{
    public class Waiter
    {
        private readonly int id;
        private Room assignedRoom;
        private Queue<string> workQueue;

        public string State { get; set; }
        public Room AssignedRoom { get { return assignedRoom; } }

        public Waiter()
        {

        }
        public Waiter(Room assignedRoom)
        {
            this.assignedRoom = assignedRoom;
        }

        public Order GetAndSendOrder(Party party)
        {
            Order order = party.Order();
            //send to cooks
            order.State = "sent to cooks";
            return order;
        }

        public Order DeliverOrder(Order order)
        {
            order.State = "sent to customer";
            return order;
        }

        public Table PickUpCheck(Table table)
        {
            table.State = "ready to be cleaned";
            return table;
        }
    }
}
