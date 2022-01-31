﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCsDiner
{
    public abstract class WaiterState
    {
        public Waiter Waiter { get; private set; }
        public WaiterState(Waiter waiter)
        {
            this.Waiter = waiter;
        }
        public abstract void Run1(Restaurant restaurant);
    }
    public class WaiterFree : WaiterState
    {
        public WaiterFree(Waiter waiter) : base(waiter) { }
        public override void Run1(Restaurant restaurant)
        {
            var toBeReturnedOrders = findOrders(restaurant, Waiter, "ToBeReturned");
            var waitingToOrderParties = findWaitingToOrderParties(restaurant, Waiter);
            var waitingForCheckParties = findWaitingForCheckParties(restaurant, Waiter);
            if (toBeReturnedOrders.Count() > 0)
            {
                var order = toBeReturnedOrders.First();
                var party = findPartyForOrder(restaurant, order);
                Waiter.WaitingForFoodTimes.Add(party.State.WaitCounter);
                order.State = "BeingReturned";
                Waiter.State = new WaiterReturningOrder(Waiter, party);
                Console.WriteLine($"\t\t {Waiter.ID} is returning an order");
                return;
            }
            else if (waitingToOrderParties.Count() > 0)
            {
                var party = waitingToOrderParties.First();
                Waiter.WaitingToOrderTimes.Add(party.State.WaitCounter);
                party.State = new PartyOrdering(party);
                Waiter.State = new WaiterGettingOrder(Waiter, party);
                Console.WriteLine($"\t\t{Waiter.ID} is getting an order");
                return;
            }
            else if (waitingForCheckParties.Count() > 0)
            {
                var party = waitingForCheckParties.First();
                Waiter.WaitingCheckTimes.Add(party.State.WaitCounter);
                Waiter.State = new WaiterProvidingCheck(Waiter, party);
                Console.WriteLine($"\t\t{Waiter.ID} is Providing the check");
                return;
            }
            else { Waiter.FreeCounter++; }
        }
        private IOrderedEnumerable<Order> findOrders(Restaurant restaurant, Waiter waiter, string state)
        {
            var orderQuery =
                from order in restaurant.CurrentOrders
                where order.State == state && waiter.AssignedTables.Contains(order.Table)
                orderby order.WaitCounter descending
                select order;
            return orderQuery;
        }
        private Party findPartyForOrder(Restaurant restaurant, Order order)
        {
            var partyQuery =
                from party in restaurant.CurrentParties
                where party.Order == order
                select party;
            return partyQuery.First();
        }
        private IOrderedEnumerable<Party> findWaitingToOrderParties(Restaurant restaurant, Waiter waiter)
        {
            var partyQuery =
                from party in restaurant.CurrentParties
                where party.State.GetType() == typeof(PartyWaitingToOrder) && waiter.AssignedTables.Contains(party.Table)
                orderby party.State.WaitCounter descending
                select party;
            return partyQuery;
        }
        private IOrderedEnumerable<Party> findWaitingForCheckParties(Restaurant restaurant, Waiter waiter)
        {
            var partyQuery =
                from party in restaurant.CurrentParties
                where party.State.GetType() == typeof(PartyWaitingForCheck) && waiter.AssignedTables.Contains(party.Table)
                orderby party.State.WaitCounter descending
                select party;
            return partyQuery;
        }
    }
    public class WaiterGettingOrder : WaiterState
    {
        public Party Party { get; set; }
        private int orderTime;
        public WaiterGettingOrder(Waiter waiter, Party party) : base(waiter)
        {
            this.Party = party;
            orderTime = party.Customers.Count();
        }
        public override void Run1(Restaurant restaurant)
        {
            if (orderTime > 0)
            {
                orderTime--;
                return;
            }
            else
            {
                var order = Party.CreateOrder();
                restaurant.CurrentOrders.Add(order);
                order.State = "BeingSent";
                Party.State = new PartyWaitingForFood(Party);
                Waiter.State = new WaiterSendingOrder(Waiter, order);
                Console.WriteLine($"\t\t{Waiter.ID} is sending an order to the cook");
            }
        }
    }
    public class WaiterSendingOrder : WaiterState
    {
        public Order Order { get; set; }
        public WaiterSendingOrder(Waiter waiter, Order order) : base(waiter)
        {
            this.Order = order;
        }
        public override void Run1(Restaurant restaurant)
        {
            Order.State = "ToBeCooked";
            Waiter.State = new WaiterFree(Waiter);
            Console.WriteLine($"\t\t{Waiter.ID} sent an order to the cooks");
        }
    }
    public class WaiterReturningOrder : WaiterState
    {
        public Party Party { get; set; }
        public WaiterReturningOrder(Waiter waiter, Party party) : base(waiter) 
        {
            this.Party = party;
        }
        public override void Run1(Restaurant restaurant)
        {
            Party.State = new PartyEating(Party);
            Waiter.State = new WaiterFree(Waiter);
            Console.WriteLine($"\t\t{Waiter.ID} returned an order");
        }
    }
    public class WaiterProvidingCheck : WaiterState
    {
        public WaiterProvidingCheck(Waiter waiter, Party party) : base(waiter)
        {
            Party = party;
        }

        public Party Party { get; }

        public override void Run1(Restaurant restaurant)
        {
            Party.State = new PartyRecievedCheck(Party);
            Waiter.CustomersServed++;
            Waiter.State = new WaiterFree(Waiter);
            Waiter.AssignedTables.Remove(Party.Table);
            Console.WriteLine($"\t\t{Waiter.ID} provided a check");
        }
    }
}