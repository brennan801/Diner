using FluentAssertions;
using JCsDiner;
using System;
using TechTalk.SpecFlow;

namespace RestaurantTests
{
    [Binding]
    public class PartySteps
    {
        private readonly ScenarioContext context;

        public PartySteps(ScenarioContext context)
        {
            this.context = context;
        }

        [Given(@"a party is in the Entered state")]
        public void GivenAPartyIsInTheEnteredState()
        {
            var party = new Party();
            context.Add("party", party);
        }

        [When(@"the party is run")]
        public void WhenThePartyIsRun()
        {
            var restaurant = context.Get<Resturant>("restaurant");
            var party = context.Get<Party>("party");
            party.Run1(restaurant);
            context.Set<Party>(party, "party");
        }

        [Then(@"the parties new state should be the Entered state")]
        public void ThenThePartiesNewStateShouldBeTheEnteredState()
        {
            var party = context.Get<Party>("party");
            party.State.Should().BeOfType(typeof(WaitingInLobby));
        }

        [Given(@"a party is in the RecievedCheck state")]
        public void GivenAPartyIsInTheRecievedCheckState()
        {
            var party = new Party();
            party.State = new RecievedCheck(party);
            context.Add("party", party);
        }

        [Then(@"the parties new state should be the Left state")]
        public void ThenThePartiesNewStateShouldBeTheLeftState()
        {
            var party = context.Get<Party>("party");
            party.State.Should().BeOfType(typeof(Left));
        }

        [Given(@"there is a party of size (.*)")]
        public void GivenThereISAPartyOfSize___(int partySize)
        {
            var party = new Party(partySize);
            context.Add("party", party);
        }

        [Given(@"the party is deciding what to order")]
        public void GivenThePartyIsDecidingWhatToOrder()
        {
            var party = context.Get<Party>("party");
            party.State = new DecidingOrder(party);
            context.Set<Party>(party, "party");
        }


        [When(@"the party is ran (.*) times")]
        [When(@"the party is ran (.*) more times")]
        public void WhenThePartyIsRan___Times(int timeToRun)
        {
            var restaurant = context.Get<Resturant>("restaurant");
            var party = context.Get<Party>("party");
            for(int i = 0; i < timeToRun; i++)
            {
                party.Run1(restaurant);
            }
            context.Set<Party>(party, "party");
        }

        [Then(@"the parties new state should be the DecidingOrder state")]
        public void ThenThePartiesNewStateShouldBeTheDecidingOrderState()
        {
            var party = context.Get<Party>("party");
            party.State.Should().BeOfType(typeof(DecidingOrder));
        }

        [Then(@"the parties new state should be the WaitingToOrder state")]
        public void ThenThePartiesNewStateShouldBeTheWaitingToOrderState()
        {
            var party = context.Get<Party>("party");
            party.State.Should().BeOfType(typeof(WaitingToOrder));
        }

        [Then(@"the parties new state should be the Ordering state")]
        public void ThenThePartiesNewStateShouldBeTheOrderingState()
        {
            var party = context.Get<Party>("party");
            party.State.Should().BeOfType(typeof(Ordering));
        }

        [Then(@"the parties new state should be the WaitingForFood state")]
        public void ThenThePartiesNewStateShouldBeTheWaitingForFoodState()
        {
            var party = context.Get<Party>("party");
            party.State.Should().BeOfType(typeof(WaitingForFood));
        }

        [Then(@"the parties new state should be the Eating state")]
        public void ThenThePartiesNewStateShouldBeTheEatingState()
        {
            var party = context.Get<Party>("party");
            party.State.Should().BeOfType(typeof(Eating));
        }

        [Then(@"the parties new state should be WaitingForCheck")]
        public void ThenThePartiesNewStateShouldBeWaitingForCheck()
        {
            var party = context.Get<Party>("party");
            party.State.Should().BeOfType(typeof(WaitingForCheck));
        }


        [Given(@"the party is ordering")]
        public void GivenThePartyIsOrdering()
        {
            var party = context.Get<Party>("party");
            party.State = new Ordering(party);
            context.Set<Party>(party, "party");
        }

        [Given(@"the party is eating an order with (.*) platters and (.*) appitizers")]
        public void GivenThePartyIsEatingAnOrderWith___PlattersAnd____Appitizers(int numPlatters, int numAppitizers)
        {
            var party = context.Get<Party>("party");
            var order = new Order();
            order.Platers = numPlatters;
            order.Appetizers = numAppitizers;
            party.Order = order;
            party.State = new Eating(party);
            context.Set<Party>(party, "party");
        }

        

    }
}
