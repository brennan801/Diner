
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var resturant = new Resturant();
            var host = new Host();
            var runables = new List<IRunable>
            {
                new Waiter(resturant.Rooms[0]),
                new Waiter(resturant.Rooms[1]),
                new Waiter(resturant.Rooms[2]),
                new Busser(),
                new Cook()
            };

            while (customersServed < Cutormers)
            {
                if (customersServed + resturant.CurrentParties.Count() < Cutormers)
                {
                    Party newParty = TryGenerateParty();
                    if (newParty is not null)
                    {
                        resturant.CurrentParties.Add(newParty);
                        Console.WriteLine("New Party Entered");
                        host.DealWithNewParty(newParty, resturant);
                    }
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

                foreach(Party party in resturant.CurrentParties)
                {
                    party.Run1(resturant);
                    if(party.State.GetType() == typeof(Left))
                    {
                        resturant.CurrentParties.Remove(party);
                        customersServed++;
                        var spaciousRoom = host.getRoomWithMostSpace(resturant);
                        try
                        {
                            host.TrySeatNextCustomer(resturant, spaciousRoom);
                        }
                        catch(ArgumentNullException e)
                        {
                            Console.WriteLine(e);
                        }
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
