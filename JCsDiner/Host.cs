using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCsDiner
{
    public class Host 
    {
        private readonly int id;
        public string State { get; private set; }

        public (Party, Room, Table) DealWithNewParty(Party party, Resturant resturant)
        {
            Room largestCapacityRoom = null;

            try
            {
                largestCapacityRoom = getRoomWithMostSpace(resturant);
            }
            catch(ArgumentNullException)
            {
                if (party.Customers.Count() > resturant.Lobby.GetSpaceLeft())
                {
                    party.State = new Left(party);
                    //Throw Turn Party Away Event 
                    return (party, null, null);
                }
                else
                {
                    resturant.Lobby.PartyQueue.Enqueue(party);
                    party.State = new WaitingInLobby(party);
                    return (party, null, null);
                }
            }

            int largestCapacity = getLargestCapacity(largestCapacityRoom.GetCapasity());


            if (party.Customers.Count() > largestCapacity)
            {
                resturant.Lobby.PartyQueue.Enqueue(party);
                party.State = new WaitingInLobby(party);
                return (party, null, null);
            }

            else if (party.Customers.Count() > 6)
            {
                //throw combine tables action
                return (party, largestCapacityRoom, null );
            }
            else
            {
                return seatPartyAtIndividualTable(party, largestCapacityRoom);

            }
            throw new Exception("Host doesn't know what to do with new party!");
        }

        public (Party, Room, Table) TrySeatNextCustomer(Resturant resturant, Room room)
        {
            if(resturant.Lobby.PartyQueue.Count() < 1)
            {
                throw new NullReferenceException("The party queue is empty");
            }

            var numAvailableTables = room.GetCapasity();
            var nextParty = resturant.Lobby.PartyQueue.Peek();
            var capacity = getLargestCapacity(numAvailableTables);
            
            if(nextParty.Customers.Count() <= capacity)
            {
                resturant.Lobby.PartyQueue.Dequeue();
                if (nextParty.Customers.Count() > 6)
                {
                    //throw combine tables action
                    return (nextParty, room, null);
                }
                else
                {
                    return seatPartyAtIndividualTable(nextParty, room);
                }
                
            }
            return (nextParty, room, null);

        }

        private int getLargestCapacity(int numAvailableTables)
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

        private (Party, Room, Table) seatPartyAtIndividualTable(Party waitingParty, Room room)
        {
            Table emptyTable = null;
            foreach (Table table in room.Tables)
            {
                if (!table.isOccupied)
                {
                    emptyTable = table;
                }
            }
            if (emptyTable is not null)
            {
                emptyTable.SetParty(waitingParty);
                waitingParty.State = new WaitingToOrder(waitingParty);
                //throw party seated action
                return (waitingParty, room, emptyTable);
            }
            else
            {
                throw new Exception("Host could not find a table for the party");
            }
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
    }
}
