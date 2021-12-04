using FluentAssertions;
using JCsDiner;
using System;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;

namespace RestaurantTests
{
    [Binding]
    public class HostSteps
    {
        private readonly ScenarioContext context;

        public HostSteps(ScenarioContext context)
        {
            this.context = context;
        }

        [Given(@"there is a restaurant")]
        public void GivenThereIsARestaurant()
        {
            var restaurant = new Restaurant();
            context.Add("restaurant", restaurant);
        }

        [Given(@"a host is in the free state")]
        public void GivenAHostIsInTheFreeState()
        {
            var host = new Host();
            context.Add("host", host);
        }

        [When(@"the host is ran (.*) times")]
        [When(@"the host is ran (.*) more times")]
        public void WhenTheHostIsRan___Times(int runTime)
        {
            var restaurant = context.Get<Restaurant>("restaurant");
            var host = context.Get<Host>("host");
            for (int i = 0; i < runTime; i++)
            {
                host.Run1(restaurant);
            }
        }

        [Then(@"the host should be in the free state")]
        public void ThenTheHostShouldBeInTheFreeState()
        {
            var host = context.Get<Host>("host");
            host.State.Should().BeOfType(typeof(Free));
        }

        [Given(@"there are (.*) parties waiting in the lobby")]
        [When(@"there are (.*) parties waiting in the lobby")]
        public void GivenThereAre___PartiesWaitingInTheLobby(int numOfParties)
        {
            var restaurant = context.Get<Restaurant>("restaurant");
            for (int i = 0; i < numOfParties; i++)
            {
                restaurant.CurrentParties.Add(new Party());
            }
            context.Set<Restaurant>(restaurant, "restaurant");
        }

        [Then(@"the host should be in the Seating Party State")]
        public void ThenTheHostShouldBeInTheSeatingPartyState()
        {
            var host = context.Get<Host>("host");
            host.State.Should().BeOfType(typeof(HostSeatingParty));
        }

        [Then(@"there should be (.*) parties waiting in the lobby")]
        public void ThenThereShouldBe___PartiesWaitingInTheLobby(int numParties)
        {
            var restaurant = context.Get<Restaurant>("restaurant");
            var lobbyQuery =
                from party in restaurant.CurrentParties
                where party.State.GetType() == typeof(PartyWaitingInLobby)
                orderby party.State.WaitCounter
                select party;
            lobbyQuery.Should().HaveCount(numParties);
        }

    }
}
