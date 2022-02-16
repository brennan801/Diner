﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        public int Run()
        {
            int beatNumber = 0;
            int customersServed = 0;
            var restaurant = new Restaurant();
            var hostPCQ = new HostPCQ();
            var waiterPCQ = new WaiterPCQ(NumberOfWaiters);
            var busserPCQ = new BusserPCQ();
            var cookPCQ = new CookPCQ(NumberOfCooks);

            using (hostPCQ)
            using (waiterPCQ)
            using (busserPCQ)
            using (cookPCQ)
            {
                while (customersServed < Customers)
                {
                    if (customersServed + restaurant.CurrentParties.Count() < Customers)
                    {
                        Party newParty = TryGenerateParty(restaurant.CurrentOrders.Count() + 1);
                        if (newParty is not null)
                        {
                            restaurant.CurrentParties.Add(newParty);
                            newParty.EnterLobbyTime = beatNumber;
                            hostPCQ.EnqueueTask(new HostTask(newParty));
                        }
                    }

                    var partiesToRemove = new List<Party>();
                    foreach (Party party in restaurant.CurrentParties)
                    {
                        if (party.State.GetType() == typeof(PartyWaitingToOrder))
                        {
                            waiterPCQ.EnqueueTask(new GetOrderTask(party));
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
                        if (order.State == "WaitingToBeReturned")
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
                    Thread.Sleep(1000);
                    beatNumber++;
                    Console.WriteLine();
                }
            }
            Console.WriteLine($"All parties have been served. Total Run Time: {beatNumber}");
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
