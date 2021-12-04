using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCsDiner
{
    public class Waiter : IRunable
    {
        public Room AssignedRoom { get; set; }
        public string Name { get; set; }
        public int FreeCounter { get; set; }

        public WaiterState State { get; set; }

        public Waiter()
        {
            State = new WaiterFree(this);
            FreeCounter = 0;
        }
        public Waiter(Room assignedRoom, string name)
        {
            this.AssignedRoom = assignedRoom;
            this.Name = name;
            State = new WaiterFree(this);
            FreeCounter = 0;
        }

        public void Run1(Restaurant resturant)
        {
            State.Run1(resturant);
        }
    }
}
