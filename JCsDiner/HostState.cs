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
        public abstract void Run1(Resturant resturant);
    }
    public class Free : HostState
    {
        public Free(Host host) : base(host) { }

        public override void Run1(Resturant resturant)
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
                nextParty.State = new BeingSeated(nextParty);
                Host.State = new SeatingParty(Host, nextParty, table);
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
            else if (partySize <= 10) perfectTable = largestCapacityRoom.CombineTables(2);
            else if (partySize <= 13) perfectTable = largestCapacityRoom.CombineTables(3);
            else perfectTable = largestCapacityRoom.CombineTables(4);
            return perfectTable;
        }

        public Party getNextParty(Resturant resturant)
        {
            if (resturant.CurrentParties.Count() > 0)
            {
                var partyQuery =
                                from party in resturant.CurrentParties
                                where party.State.GetType() == typeof(WaitingInLobby)
                                orderby party.State.WaitCounter
                                select party;
                return partyQuery.First();
            }
            else
            {
                throw new ArgumentNullException("No parties waiting in lobby");
            }
        }
    }
    public class SeatingParty : HostState
    {
        public Party Party { get; private set; }
        public Table Table { get; private set; }
        public int TimeSpent { get; private set; }
        public SeatingParty(Host host, Party party, Table table) : base(host)
        {
            this.Party = party;
            this.Table = table;
            this.TimeSpent = 0;
            Party.State = new BeingSeated(party);
            Table.State = "occupied";
        }
        public override void Run1(Resturant resturant)
        {
            TimeSpent++;
            if(TimeSpent >= 2)
            {
                Host.Seat(Party, Table);
                Host.State = new Free(Host);
            }
        }
    }
}
