
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCsDiner
{
    public class Simulator
    {
        public int Customers { get; private set; }
        public Simulator(int customers)
        {
            this.Customers = customers;
        }
        public int Run()
        {
            int beatNumber = 0;
            int customersServed = 0;
            var restaurant = new Restaurant();
            var host = new Host();
            var runables = new List<IRunable>
            {
                new Waiter(restaurant.Rooms[0], "Jim"),
                new Waiter(restaurant.Rooms[1], "Sal"),
                new Waiter(restaurant.Rooms[2], "Bob"),
                new Busser(),
                new Cook()
            };

            while (customersServed < Customers)
            {
                if (customersServed + restaurant.CurrentParties.Count() < Customers)
                {
                    Party newParty = TryGenerateParty(restaurant.CurrentOrders.Count() + 1);
                    if (newParty is not null) restaurant.CurrentParties.Add(newParty);
                }
                    host.Run1(restaurant);


                foreach (IRunable runable in runables){
                    runable.Run1(restaurant);
                }
                var partiesToRemove = new List<Party>();
                foreach(Party party in restaurant.CurrentParties)
                {
                    party.Run1(restaurant);
                    if(party.State.GetType() == typeof(PartyLeft))
                    {
                        partiesToRemove.Add(party);
                        restaurant.CurrentOrders.Remove(party.Order);
                        customersServed++;
                        Console.WriteLine($"The restraurant has served {customersServed} parties");
                    }
                }
                foreach(Party party in partiesToRemove)
                {
                    restaurant.CurrentParties.Remove(party);
                }
                Console.WriteLine();
            }
            Console.WriteLine("done.");
            return beatNumber;
        }

        public Party TryGenerateParty(int id)
        {
            var rand = new Random();
            var randNum = rand.Next(100);
            return randNum < 20 ? new Party(id) : null; 
        }
    }
}
