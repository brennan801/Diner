
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCsDiner.Host;
using JCsDiner.Party;
using JCsDiner.Waiter;

namespace JCsDiner
{
    public class Simulator
    {
        public int Cutormers { get; private set; }
        public Simulator(int customers)
        {
            this.Cutormers = customers;
        }
        public int Run()
        {
            int beatNumber = 0;
            int customersServed = 0;
            var resturant = new Restaurant();
            var host = new Host.Host();
            var runables = new List<IRunable>
            {
                new Waiter.Waiter(resturant.Rooms[0], "Jim"),
                new Waiter.Waiter(resturant.Rooms[1], "Sal"),
                new Waiter.Waiter(resturant.Rooms[2], "Bob"),
                new Busser(),
                new Cook()
            };

            while (customersServed < Cutormers)
            {
                if (customersServed + resturant.CurrentParties.Count() < Cutormers)
                {
                    Party.Party newParty = TryGenerateParty();
                }
                
                foreach(IRunable runable in runables){
                    try
                    {
                        runable.Run1(resturant);
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }

                foreach(Party.Party party in resturant.CurrentParties)
                {
                    party.Run1(resturant);
                    if(party.State.GetType() == typeof(PartyLeft))
                    {
                        resturant.CurrentParties.Remove(party);
                        customersServed++;
                    }
                }
            }
            return beatNumber;
        }

        public Party TryGenerateParty()
        {
            var rand = new Random();
            var randNum = rand.Next(100);
            return randNum < 20 ? new Party() : null; 
        }
    }
}
