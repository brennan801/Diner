using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCsDiner
{
    public abstract class HostState
    {
        public Host Host { get; protected set; }
        public HostState(Host host)
        {
            this.Host = host;
        }
        public abstract void Run1(Restaurant resturant, int beatNumber);
    }
    public class HostFree : HostState
    {
        public HostFree(Host host) : base(host) { }

        public override void Run1(Restaurant resturant, int beatNumber)
        {
            Party nextParty;
            Room largestCapacityRoom;
            int capacity;
            
            try
            {
                nextParty = getNextParty(resturant);
                largestCapacityRoom = Host.getRoomWithMostSpace(resturant);
                capacity = Host.getLargestCapacity(largestCapacityRoom.GetCapasity());
            }
            catch(ArgumentNullException e)
            {
                Host.FreeTimeCounter++;
                return;
            }
            if(nextParty.Customers.Count() <= capacity)
            {
                var table = getTableForParty(nextParty.Customers.Count(), largestCapacityRoom);
                nextParty.State = new PartyBeingSeated(nextParty);
                nextParty.ExitLobbyTime = beatNumber;
                Host.State = new HostSeatingParty(Host, nextParty, table);
                Console.WriteLine($"Host started seating a party with {nextParty.Customers.Count()} customers");
            }
        }

        private Table getTableForParty(int partySize, Room largestCapacityRoom)
        {
            Table perfectTable;
            var tableQuery =
                    from table in largestCapacityRoom.Tables
                    where table.State == "clean"
                    select table;
            if (partySize <= 6) perfectTable = tableQuery.First();
            else if (partySize <= 10)
            {
                perfectTable = largestCapacityRoom.CombineTables(2);
                Console.WriteLine("Host combined 2 tables");
            }
            else if (partySize <= 13)
            {
                perfectTable = largestCapacityRoom.CombineTables(3);
                Console.WriteLine("Host combined 3 tables");
            }
            else
            {
                perfectTable = largestCapacityRoom.CombineTables(4);
                Console.WriteLine("Host combined 4 tables");
            }
            return perfectTable;
        }

        public Party getNextParty(Restaurant resturant)
        {
            if (resturant.CurrentParties.Count() > 0)
            {
                var partyQuery =
                                from party in resturant.CurrentParties
                                where party.State.GetType() == typeof(PartyWaitingInLobby)
                                orderby party.State.WaitCounter
                                select party;
                if(partyQuery.Count() > 0)
                {
                    return partyQuery.First();
                }
                else
                {
                    throw new ArgumentNullException("No parties waiting in lobby");
                }

            }
            else
            {
                throw new ArgumentNullException("No parties waiting in lobby");
            }
        }
    }
    public class HostSeatingParty : HostState
    {
        public Party Party { get; private set; }
        public Table Table { get; private set; }
        public int TimeSpent { get; private set; }
        public HostSeatingParty(Host host, Party party, Table table) : base(host)
        {
            this.Party = party;
            this.Table = table;
            this.TimeSpent = 0;
            Party.State = new PartyBeingSeated(party);
            Table.State = "occupied";
        }
        public override void Run1(Restaurant resturant, int beatNumber)
        {
            TimeSpent++;
            if(TimeSpent >= 2)
            {
                Host.Seat(Party, Table);
                Host.State = new HostFree(Host);
                Party.State = new PartyDecidingOrder(Party);
                Host.PartyLobbyTimes.Add(Party.ExitLobbyTime - Party.EnterLobbyTime);
                Console.WriteLine("Host seated a party");
            }
        }
    }
}
