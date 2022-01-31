using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCsDiner
{
    public class Waiter : IRunable
    {
        public int ID { get; set; }
        public List<Table> AssignedTables { get; set; }
        public int CustomersServed { get; set; }
        public int FreeCounter { get; set; }
        public List<int> WaitingToOrderTimes = new();
        public List<int> WaitingForFoodTimes = new();
        public List<int> WaitingCheckTimes = new();


        public WaiterState State { get; set; }

        public Waiter()
        {
            State = new WaiterFree(this);
            FreeCounter = 0;
            CustomersServed = 0;
            AssignedTables = new();
        }
        public Waiter(int id)
        {
            this.ID = id;
            State = new WaiterFree(this);
            FreeCounter = 0;
            CustomersServed = 0;
            AssignedTables = new();
        }

        public void Run1(Restaurant resturant, int beatNumber)
        {
            State.Run1(resturant);
        }

        public void PrintStats()
        {
            Console.WriteLine(
                $"Waiter: {ID}" +
                $"\n\tPartysServed: {CustomersServed}" +
                $"\n\tWastedTime: {FreeCounter}" +
                $"\n\tAverage Party Waiting To Order Time: {WaitingToOrderTimes.Average()}" +
                $"\n\tAverage Party Waiting For Food Time: {WaitingForFoodTimes.Average()}" +
                $"\n\tAverage Party Waiting For Check Time: {WaitingCheckTimes.Average()}"
                );
        }
    }
}
