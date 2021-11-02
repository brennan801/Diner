using System;
using System.Collections.Generic;

namespace JCsDiner
{
    class Program
    {
        static void Main(string[] args)
        {
            Resturant resturant = new();
            Lobby lobby = new(20);
            resturant.Lobby = lobby;
            Host host = new();
            Waiter waiter0 = new(resturant.Rooms[0]);
            Waiter waiter1 = new(resturant.Rooms[1]);
            Waiter waiter2 = new(resturant.Rooms[2]);
            Busser busser = new();
            Cook cook = new();

            List<Party> parties = new List<Party>();
            for (int i = 0; i < 15; i++)
            {
                parties.Add(new Party());
            }
            foreach(Party party in parties)
            {
                party.Enter();
                (_, Room assignedRoom, Table assignedTable) = host.DealWithNewParty(party, resturant);
                if(party.State == "waiting for table")
                {
                    busser.CombineTablesForParty(party, assignedRoom);
                }
            }
            var thisParty = waiter0.AssignedRoom.Tables[0].Party;
            var order = waiter0.GetAndSendOrder(thisParty);
            order = cook.CookOrder(order);
            waiter0.DeliverOrder(order);
            thisParty.Eat();
            thisParty.PayAndLeave();
            busser.cleanTable(waiter0.AssignedRoom.Tables[0]);


        }
    }
}
