
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JCsDiner
{
    public class SimulatorArguments
    {
        public int NumberOfWaiters { get; set; }
        public int NumberOfCooks { get; set; }
        public int Customers { get; set; }
        public int AveragePartySize { get; set; }
        public int AveragePartyEntryTime { get; set; }
        public int NumberOfTables { get; set; }
    }
    public class SimulatorResults
    {
        public string Name { get; set; }
        public int Runtime { get; set; }
        public int NumberOfCustomers { get; set; }
        public int NumberOfWaiters { get; set; }
        public int NumberOfCooks { get; set; }
        public int NumberOfTables { get; set; }
        public int AverageEntryTime { get; set; }
        public int SetAveragePartySize { get; set; }
        public int ActualAveragePartySize { get; set; }
    }
    public class Simulator
    {
        public int NumberOfWaiters { get; set; }
        public int NumberOfCooks { get; set; }
        public int Customers { get; private set; }
        public int AveragePartySize { get; set; }
        public int AveragePartyEntryTime { get; set; }
        public int NumberOfTables { get; set; }
        public SimulatorResults Run(SimulatorArguments simArgs)
        {
            AveragePartySize = simArgs.AveragePartySize;
            this.Customers = simArgs.Customers;
            this.NumberOfWaiters = simArgs.NumberOfWaiters;
            NumberOfCooks = simArgs.NumberOfCooks;
            AveragePartyEntryTime = simArgs.AveragePartyEntryTime;
            NumberOfTables = simArgs.NumberOfTables;
            int beatNumber = 0;
            int customersServed = 0;
            var restaurant = new Restaurant(NumberOfTables);
            var hostPCQ = new HostPCQ();
            var waiterPCQ = new WaiterPCQ(NumberOfWaiters);
            var busserPCQ = new BusserPCQ();
            var cookPCQ = new CookPCQ(NumberOfCooks);
            int partiesEntered = 0;
            int customersEntered = 0;
            int timeSinceLastEnteredParty = 0;

            using (hostPCQ)
            using (waiterPCQ)
            using (busserPCQ)
            using (cookPCQ)
            {
                while (customersServed < Customers)
                {
                    if (customersServed + restaurant.CurrentParties.Count() < Customers)
                    {
                        Party newParty = TryGenerateParty(partiesEntered, timeSinceLastEnteredParty);
                        if (newParty is not null)
                        {
                            partiesEntered++;
                            timeSinceLastEnteredParty = 0;
                            customersEntered += newParty.Customers;
                            restaurant.CurrentParties.Add(newParty);
                            newParty.EnterLobbyTime = beatNumber;
                            hostPCQ.EnqueueTask(new HostTask(newParty, restaurant));
                        }
                    }

                    var partiesToRemove = new List<Party>();
                    foreach (Party party in restaurant.CurrentParties)
                    {
                        if (party.State.GetType() == typeof(PartyWaitingToOrder))
                        {
                            waiterPCQ.EnqueueTask(new GetOrderTask(party, cookPCQ, restaurant));
                        }
                        else if(party.State.GetType() == typeof(PartyWaitingForCheck))
                        {
                            waiterPCQ.EnqueueTask(new GetCheckTask(party));
                        }
                        else if(party.State.GetType() == typeof(PartyLeft))
                        {
                            partiesToRemove.Add(party);
                            restaurant.CurrentOrders.Remove(party.Order);
                            customersServed++;
                            Console.WriteLine($"The restraurant has served {customersServed} parties");
                        }
                    }
                    foreach(Order order in restaurant.CurrentOrders)
                    {
                        if (order.State == "waiting to be returned")
                        {
                            waiterPCQ.EnqueueTask(new ReturnOrderTask(order));
                        }

                    }
                    foreach (Party party in restaurant.CurrentParties)
                    {
                        party.Run1(restaurant);
                    }
                    foreach (Party party in partiesToRemove)
                    {
                        restaurant.CurrentParties.Remove(party);
                    }

                    foreach (Table table in restaurant.Tables)
                    {
                        if (table.State == "dirty")
                        {
                            busserPCQ.EnqueueTask(new BusserTask(table, restaurant));
                        }
                    }
                    Thread.Sleep(1000);
                    beatNumber++;
                    timeSinceLastEnteredParty++;
                    Console.WriteLine();
                }
            }
            Console.WriteLine($"All parties have been served. Total Run Time: {beatNumber}");
            Console.WriteLine($"Average Party Size: {customersEntered / partiesEntered}");
            Console.WriteLine($"Restaurant ended with {restaurant.Tables.Count} tables");
            foreach(Table table in restaurant.Tables)
            {
                Console.Write($"{table.ID} ");
            }
            SimulatorResults results = new()
            {
                Runtime = beatNumber,
                NumberOfCustomers = Customers,
                NumberOfWaiters = NumberOfWaiters,
                NumberOfCooks = NumberOfCooks,
                NumberOfTables = NumberOfTables,
                AverageEntryTime = AveragePartyEntryTime,
                SetAveragePartySize = AveragePartySize,
                ActualAveragePartySize = customersEntered / partiesEntered
            };
            return results;
        }

        public Party TryGenerateParty(int id, int timeSinceLastEnteredParty)
        {
            var rand = new Random();
            var randNum = rand.Next(100);
            var range = AveragePartyEntryTime * .3;
            if(timeSinceLastEnteredParty < AveragePartyEntryTime - range)
            {
                return randNum < 5 ? new Party(id, AveragePartySize) : null;
            }
            else if (timeSinceLastEnteredParty <= AveragePartyEntryTime + range)
            {
                return randNum < 50 ? new Party(id, AveragePartySize) : null;
            }
            else return new Party(id, AveragePartySize); 
        }
    }
}
