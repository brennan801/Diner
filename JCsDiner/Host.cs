using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCsDiner
{
    public class Host : IRunable
    {
        private readonly int id;
        public HostState State { get; set; }
        public int FreeTimeCounter { get; set;}

        public Host()
        {
            State = new Free(this);
            FreeTimeCounter = 0;
        }

        public (Party, Table) Seat(Party party, Table table)
        {
            party.Table = table;
            party.State = new DecidingOrder(party);
            return (party, table);
        }

        public int getLargestCapacity(int numAvailableTables)
        {
            if (numAvailableTables >= 4)
            {
                return 16;
            }
            else if (numAvailableTables == 3)
            {
                return 13;
            }
            else if (numAvailableTables == 2)
            {
                return 10;
            }
            else if (numAvailableTables == 1)
            {
                return 6;
            }
            else return 0;
        }

        public Room getRoomWithMostSpace(Resturant resturant)
        {
            Room roomWithMostSpace = null;
            int maxNumTables = 0;
            foreach(Room room in resturant.Rooms)
            {
                if(room.GetCapasity() > maxNumTables)
                {
                    maxNumTables = room.GetCapasity();
                    roomWithMostSpace = room;
                }
            }
            if (roomWithMostSpace is null)
            {
                throw new ArgumentNullException("Couldn't find a room with any space");
            }
            else return roomWithMostSpace;
        }

        public void Run1(Resturant resturant)
        {
            State.Run1(resturant);
        }
    }
}
