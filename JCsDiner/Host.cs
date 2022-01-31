﻿using System;
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
        public List<int> PartyLobbyTimes { get; set; }   

        public Host()
        {
            State = new HostFree(this);
            FreeTimeCounter = 0;
            PartyLobbyTimes = new List<int>();
        }

        public (Party, Table) Seat(Party party, Table table)
        {
            party.Table = table;
            party.State = new PartyDecidingOrder(party);
            PartyLobbyTimes = new List<int>();
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

        public void Run1(Restaurant resturant, int beatNumber)
        {
            State.Run1(resturant, beatNumber);
        }

        public void PrintStats()
        {
            Console.WriteLine(
                $"Host" +
                $"\n\tAverage Party Waiting in Lobby Time: {PartyLobbyTimes.Average()}" +
                $"\n\tWastedTime: {FreeTimeCounter}"
                );
        }
    }
}
