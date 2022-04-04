using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCsDiner
{
    public abstract class WaiterTask
    {
        public Party Party { get; set; }
        public int Time { get; set; }
        public abstract void DoTask(int id);
        public abstract void StartTask(int id);
    }

    public class GetOrderTask : WaiterTask
    {
        public Restaurant Restaurant { get; }

        public CookPCQ CookPCQ;
        public GetOrderTask(Party party, CookPCQ cookPCQ, Restaurant restaurant)
        {
            CookPCQ = cookPCQ;
            Restaurant = restaurant;
            Party = party;
            Time = Party.Customers;
        }

        public override void DoTask(int id)
        {

            var order = Party.CreateOrder();
            order.State = "waiting to be cooked";
            Party.State = new PartyWaitingForFood(Party);
            Restaurant.CurrentOrders.Add(order);
            CookPCQ.EnqueueTask(new CookTask(order));
            Console.WriteLine($"\t\t Waiter {id} got the order of party {Party.ID}");
        }

        public override void StartTask(int id)
        {
            Console.WriteLine($"\t\t Waiter {id} is getting the order of party {Party.ID}");
            Party.State = new PartyOrdering(Party);
        }
    }

    public class GetCheckTask : WaiterTask
    {
        public GetCheckTask(Party party)
        {
            Time = 1;
            Party = party;
        }

        public override void DoTask(int id)
        {
            Party.State = new PartyRecievedCheck(Party);
            Console.WriteLine($"\t\t Waiter {id} got the check of party {Party.ID}");
        }

        public override void StartTask(int id)
        {
            Console.WriteLine($"\t\t Waiter {id} is getting the check of party {Party.ID}");
        }
    }
    public class ReturnOrderTask : WaiterTask
    {
        public Order Order { get; set; }
        public int AverageEatingTime { get; }

        public ReturnOrderTask(Order order, int averageEatingTime)
        {
            Time = 1;
            Order = order;
            AverageEatingTime = averageEatingTime;
            Party = order.Table.Party;
        }
        public override void DoTask(int id)
        {
            Order.State = "beingEaten";
            Order.Table.Party.State = new PartyEating(Party, AverageEatingTime);
            Console.WriteLine($"\t\t Waiter {id} returned the the order to the party {Party.ID}");
        }
        public override void StartTask(int id)
        {
            Console.WriteLine($"\t\t Waiter {id} is returning the order to the party {Party.ID}");
            Order.State = "being returned";
        }
    }
}
