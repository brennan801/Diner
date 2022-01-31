
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCsDiner
{
    public class Simulator
    {
        public int NumberOfWaiters { get; set; }
        public int NumberOfCooks { get; set; }
        public int Customers { get; private set; }
        public Simulator(int customers, int nuberOfWaiters, int numberOfCooks)
        {
            this.Customers = customers;
            this.NumberOfWaiters = nuberOfWaiters;
            NumberOfCooks = numberOfCooks;
        }
        public Simulator()
        {
            this.Customers = 10;
            this.NumberOfWaiters = 3;
        }
        public int Run()
        {
            int beatNumber = 0;
            int customersServed = 0;
            var restaurant = new Restaurant();
            var host = new Host();
            var waiters = new List<Waiter>();
            for (int i = 0; i < NumberOfWaiters; i++)
            {
                waiters.Add(new Waiter(i));
            }
            restaurant.Waiters = waiters;
            var runables = new List<IRunable>();
            runables.AddRange(waiters);
            for (int i = 0; i < NumberOfCooks; i++)
            {
                runables.Add(new Cook(i));
            }
            runables.Add(new Busser());

            while (customersServed < Customers)
            {
                if (customersServed + restaurant.CurrentParties.Count() < Customers)
                {
                    Party newParty = TryGenerateParty(restaurant.CurrentOrders.Count() + 1);
                    if (newParty is not null)
                    {
                        restaurant.CurrentParties.Add(newParty);
                        newParty.EnterLobbyTime = beatNumber;
                    }
                }
                    host.Run1(restaurant, beatNumber);


                foreach (IRunable runable in runables){
                    runable.Run1(restaurant, beatNumber);
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
                beatNumber++;
                Console.WriteLine();
            }
            Console.WriteLine($"All parties have been served. Total Run Time: {beatNumber}");

            host.PrintStats();
            foreach(IRunable employee in runables)
            {
                employee.PrintStats();
            }
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
