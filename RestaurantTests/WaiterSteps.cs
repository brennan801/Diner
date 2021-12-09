using FluentAssertions;
using JCsDiner;
using System;
using TechTalk.SpecFlow;

namespace RestaurantTests
{
    [Binding]
    public class WaiterSteps
    {
        private readonly ScenarioContext context;

        public WaiterSteps(ScenarioContext context)
        {
            this.context = context;
        }

        [Given(@"there is a waiter for room (.*)")]
        public void GivenThereIsAWaiterForRoom(int waiterRoomNumber)
        {
            var restaurant = context.Get<Restaurant>("restaurant");
            var waiterRoom = restaurant.Rooms[waiterRoomNumber];
            var waiter = new Waiter(waiterRoom, "jim");
            context.Add("waiter", waiter);
        }


        [Given(@"each party has an order in the ToBeReturned State")]
        public void GivenEachPartyHasAnOrderInTheToBeReturnedState()
        {
            var restaurant = context.Get<Restaurant>("restaurant");
            var waiter = context.Get<Waiter>("waiter");

            foreach(Party party in restaurant.CurrentParties)
            {
                party.Order = new Order { State = "ToBeReturned", Table = party.Table };
                restaurant.CurrentOrders.Add(party.Order);
            }
        }


        [Given(@"there are (.*) parties in the waiting to order state")]
        public void GivenThereAre___PartiesInTheWaitingToOrderState(int numParties)
        {
            var restaurant = context.Get<Restaurant>("restaurant");
            var waiter = context.Get<Waiter>("waiter");
            for (int i = 0; i < numParties; i++)
            {
                var newParty = new Party(0);
                newParty.State = new PartyWaitingToOrder(newParty);
                newParty.Table = waiter.AssignedRoom.Tables[i];
                restaurant.CurrentParties.Add(newParty);
            }
        }

        [Given(@"there are (.*) parties in the waiting for check state")]
        public void GivenThereAre___PartiesInTheWaitingForCheckState(int numParties)
        {
            var restaurant = context.Get<Restaurant>("restaurant");
            var waiter = context.Get<Waiter>("waiter");
            for (int i = 0; i < numParties; i++)
            {
                var newParty = new Party(0);
                newParty.State = new PartyWaitingForCheck(newParty);
                newParty.Table = waiter.AssignedRoom.Tables[i];
                restaurant.CurrentParties.Add(newParty);
            }
        }

        [When(@"the waiter is run (.*) times")]
        public void WhenTheWaiterIsRun___Times(int runTime)
        {
            var restaurant = context.Get<Restaurant>("restaurant");
            var waiter = context.Get<Waiter>("waiter");

            for (int i = 0; i < runTime; i++)
            {
                waiter.Run1(restaurant);
            }
        }
        [Then(@"the waiter should be in the returning order state")]
        public void ThenTheWaiterShouldBeInTheReturningOrderState()
        {
            var waiter = context.Get<Waiter>("waiter");
            waiter.State.Should().BeOfType(typeof(WaiterReturningOrder));
        }

        [Then(@"the waiter should be in the getting order state")]
        public void ThenTheWaiterShouldBeInTheGettingOrderState()
        {
            var waiter = context.Get<Waiter>("waiter");
            waiter.State.Should().BeOfType(typeof(WaiterGettingOrder));
        }

        [Then(@"the waiter should be in the providing check state")]
        public void ThenTheWaiterShouldBeInTheProvidingCheckState()
        {
            var waiter = context.Get<Waiter>("waiter");
            waiter.State.Should().BeOfType(typeof(WaiterProvidingCheck));
        }

        [Then(@"the waiter should be in the Free state")]
        public void ThenTheWaiterShouldBeInTheFreeState()
        {
            var waiter = context.Get<Waiter>("waiter");
            waiter.State.Should().BeOfType(typeof(WaiterFree));
        }

        [Then(@"the waiter should have a free counter of (.*)")]
        public void ThenTheWaiterShouldHaveAFreeCounterOf___(int expectedWaitCounter)
        {
            var waiter = context.Get<Waiter>("waiter");
            waiter.FreeCounter.Should().Be(expectedWaitCounter);
        }

    }
}
