using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCsDiner
{
    public abstract class WaiterTask
    {
        public int Time { get; set; }
        public abstract void DoTask(int id);
        public abstract void StartTask(int id);
    }

    public class GetOrderTask : WaiterTask
    {
        public Party Party { get; set; }
        public GetOrderTask(Party party)
        {
            Party = party;
            Time = Party.Customers.Count * 1000;
        }

        public override void DoTask(int id)
        {
            Console.WriteLine($"Waiter {id} got the order of table {Party.ID}");
        }

        public override void StartTask(int id)
        {
            Console.WriteLine($"Waiter {id} is getting the order of table {Party.ID}");
        }
    }

    public class GetCheckTask : WaiterTask
    {
        public Party Party { get; set; }
        public GetCheckTask(Party party)
        {
            Time = 1000;
            Party = party;
        }

        public override void DoTask(int id)
        {
            Console.WriteLine($"Waiter {id} got the check of table {Party.ID}");
        }

        public override void StartTask(int id)
        {
            Console.WriteLine($"Waiter {id} is getting the check of party {Party.ID}");
        }
    }
    public class ReturnOrderTask : WaiterTask
    {
        public Order Order { get; set; }
        public ReturnOrderTask(Order order)
        {
            Order = order;
        }
        public override void DoTask(int id)
        {
            Console.WriteLine($"Waiter {id} returned the the order to the party {Order.Table.Party}");
        }

        public override void StartTask(int id)
        {
            Console.WriteLine($"Waiter {id} is returning the order to the party {Order.Table.Party}");
        }
    }
}
