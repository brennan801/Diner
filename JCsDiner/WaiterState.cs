using System;
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
            var toBeReturnedOrders = findOrders(restaurant, "ToBeReturned");
            var waitingToOrderParties = findWaitingToOrderParties(restaurant);
            var waitingForCheckParties = findWaitingForCheckParties(restaurant);
            if (toBeReturnedOrders.Count() > 0)
            {
                var order = toBeReturnedOrders.First();
                var party = findPartyForOrder(restaurant, order);
                order.State = "BeingReturned";
                Waiter.State = new WaiterReturningOrder(Waiter, party);
                return;
            }
            else if (waitingToOrderParties.Count() > 0)
            {
                var party = waitingToOrderParties.First();
                party.State = new PartyOrdering(party);
                Waiter.State = new WaiterGettingOrder(Waiter, party);
                return;
            }
            else if (waitingForCheckParties.Count() > 0)
            {
                var party = waitingForCheckParties.First();
                Waiter.State = new WaiterProvidingCheck(Waiter, party);
                return;
            }
            else { Waiter.FreeCounter++; }
        }
        private IOrderedEnumerable<Order> findOrders(Restaurant restaurant, string state)
        {
            var orderQuery =
                from order in restaurant.CurrentOrders
                where order.State == state
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
        private IOrderedEnumerable<Party> findWaitingToOrderParties(Restaurant restaurant)
        {
            var partyQuery =
                from party in restaurant.CurrentParties
                where party.State.GetType() == typeof(PartyWaitingToOrder)
                orderby party.State.WaitCounter descending
                select party;
            return partyQuery;
        }
        private IOrderedEnumerable<Party> findWaitingForCheckParties(Restaurant restaurant)
        {
            var partyQuery =
                from party in restaurant.CurrentParties
                where party.State.GetType() == typeof(PartyWaitingForCheck)
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
                order.State = "BeingSent";
                Party.State = new PartyWaitingForFood(Party);
                Waiter.State = new WaiterSendingOrder(Waiter, order);
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
            Waiter.State = new WaiterFree(Waiter);
        }
    }
}
