using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCsDiner
{
    public class Lobby
    {
        private readonly int capacity;
        public Queue<Party> PartyQueue { get; internal set; }
        public Lobby(int capacity)
        {
            this.capacity = capacity;
            PartyQueue = new Queue<Party>();
        }
        public int GetSpaceLeft()
        {
            int spaceTaken = 0;
            foreach(Party party in PartyQueue)
            {
                spaceTaken += party.Customers.Count();
            }
            return capacity - spaceTaken;
        }
    }
}
