
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
        public string Name { get; set; }
        public int NumberOfWaiters { get; set; }
        public int NumberOfCooks { get; set; }
        public int Customers { get; set; }
        public int AveragePartySize { get; set; }
        public int AveragePartyEntryTime { get; set; }
        public int NumberOfTables { get; set; }
        public int AverageEatingTime { get; set; }
        public string RunSpeed { get; set; }
    }
    public class SimulatorResults
    {
        public int ID { get; set; }
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
        public int RunSpeed { get; set; }
        public bool IsRunning { get; set; }
        public string Name { get; set;}
        public int NumberOfWaiters { get; set; }
        public int NumberOfCooks { get; set; }
        public int Customers { get; private set; }
        public int AveragePartySize { get; set; }
        public int AveragePartyEntryTime { get; set; }
        public int NumberOfTables { get; set; }
        public int AverageEatingTime { get; set; }
        public Restaurant Restaurant { get; set; }
        public CookPCQ CookPCQ { get; private set; }
        public HostPCQ HostPCQ { get; private set; }
        public WaiterPCQ WaiterPCQ { get; private set; }
        public BusserPCQ BusserPCQ { get; private set; }
        //public List<Party> CurrentParties { get; set; }

        public event EventHandler StateChanged;
        public SimulatorResults Results { get; private set; }
        public int PartiesEntered { get; set; }
        public int PartiesServed { get; set; }
     

        public Simulator()
        {
            Restaurant = new Restaurant();
            this.CookPCQ = new CookPCQ();
            this.HostPCQ = new HostPCQ();
            this.WaiterPCQ = new WaiterPCQ();
            this.BusserPCQ = new BusserPCQ();
            this.Results = new();
            PartiesEntered = 0;
            PartiesServed = 0;
        }

        public void RaiseStateChanged()
        {
            StateChanged?.Invoke(this, EventArgs.Empty);
        }

        public void Run(SimulatorArguments simArgs)
        {
            this.RunSpeed = GetRunSpeed(simArgs.RunSpeed);
            this.Name = simArgs.Name;
            AveragePartySize = simArgs.AveragePartySize;
            this.Customers = simArgs.Customers;
            this.NumberOfWaiters = simArgs.NumberOfWaiters;
            NumberOfCooks = simArgs.NumberOfCooks;
            AveragePartyEntryTime = simArgs.AveragePartyEntryTime;
            NumberOfTables = simArgs.NumberOfTables;
            AverageEatingTime = simArgs.AverageEatingTime;
            int beatNumber = 0;
            PartiesServed = 0;
            this.Restaurant = new Restaurant(NumberOfTables);
            this.HostPCQ = new HostPCQ(RunSpeed);
            this.WaiterPCQ = new WaiterPCQ(NumberOfWaiters, RunSpeed);
            this.BusserPCQ = new BusserPCQ(RunSpeed);
            this.CookPCQ = new CookPCQ(NumberOfCooks, RunSpeed);
            PartiesEntered = 0;
            int customersEntered = 0;
            int timeSinceLastEnteredParty = 0;
            this.IsRunning = true;

            using (HostPCQ)
            using (WaiterPCQ)
            using (BusserPCQ)
            using (CookPCQ)
            {
                while (PartiesServed < Customers)
                {
                    if (PartiesServed + Restaurant.CurrentParties.Count() < Customers)
                    {
                        Party newParty = TryGenerateParty(PartiesEntered, timeSinceLastEnteredParty);
                        if (newParty is not null)
                        {
                            PartiesEntered++;
                            timeSinceLastEnteredParty = 0;
                            customersEntered += newParty.Customers;
                            Restaurant.CurrentParties.Add(newParty);
                            newParty.EnterLobbyTime = beatNumber;
                            HostPCQ.EnqueueTask(new HostTask(newParty, Restaurant));
                        }
                    }

                    var partiesToRemove = new List<Party>();
                    foreach (Party party in Restaurant.CurrentParties)
                    {
                        if (party.State.GetType() == typeof(PartyWaitingToOrder))
                        {
                            WaiterPCQ.EnqueueTask(new GetOrderTask(party, CookPCQ, Restaurant));
                        }
                        else if (party.State.GetType() == typeof(PartyWaitingForCheck))
                        {
                            WaiterPCQ.EnqueueTask(new GetCheckTask(party));
                        }
                        else if (party.State.GetType() == typeof(PartyLeft))
                        {
                            partiesToRemove.Add(party);
                            Restaurant.CurrentOrders.Remove(party.Order);
                            PartiesServed++;
                            Console.WriteLine($"The restraurant has served {PartiesServed} parties");
                        }
                    }
                    foreach (Order order in Restaurant.CurrentOrders)
                    {
                        if (order.State == "waiting to be returned")
                        {
                            WaiterPCQ.EnqueueTask(new ReturnOrderTask(order, AverageEatingTime));
                        }

                    }
                    foreach (Party party in Restaurant.CurrentParties)
                    {
                        party.Run1(Restaurant);
                    }
                    foreach (Party party in partiesToRemove)
                    {
                        Restaurant.CurrentParties.Remove(party);
                    }

                    foreach (Table table in Restaurant.Tables)
                    {
                        if (table.State == "dirty")
                        {
                            BusserPCQ.EnqueueTask(new BusserTask(table, Restaurant));
                        }
                    }
                    Thread.Sleep(RunSpeed);
                    beatNumber++;
                    timeSinceLastEnteredParty++;
                    Console.WriteLine($"PartiesServed: {PartiesServed} ");
                    RaiseStateChanged();
                }
            }
            Console.WriteLine($"All parties have been served. Total Run Time: {beatNumber}");
            Console.WriteLine($"Average Party Size: {customersEntered / PartiesEntered}");
            Console.WriteLine($"Restaurant ended with {Restaurant.Tables.Count} tables");
            foreach (Table table in Restaurant.Tables)
            {
                Console.Write($"{table.ID} ");
            }
            Results = new()
            {
                Name = Name,
                Runtime = beatNumber,
                NumberOfCustomers = Customers,
                NumberOfWaiters = NumberOfWaiters,
                NumberOfCooks = NumberOfCooks,
                NumberOfTables = NumberOfTables,
                AverageEntryTime = AveragePartyEntryTime,
                SetAveragePartySize = AveragePartySize,
                ActualAveragePartySize = customersEntered / PartiesEntered
            };
            Console.WriteLine("raising state changed");
            this.IsRunning = false;
            RaiseStateChanged();

        }

        private int GetRunSpeed(string runSpeed)
        {
            if(runSpeed == "Fast")
            {
                return 50;
            }
            else if(runSpeed == "Slow")
            {
                return 1000;
            }
            else
            {
                return 1000;
            }
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
