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
        [Given(@"there is a party of (.*)")]
        public void GivenThereIsAPartyOf(int numOfCustomers)
        {
            var party = new Party(numOfCustomers, "walking in");
            context.Add("party", party);
        }
        
        [When(@"the party orders food")]
        public void WhenThePartyOrdersFood()
        {
            var party = context.Get<Party>("party");
            var partyOrder = party.Order();
            context.Add("partyOrder", partyOrder);
        }
        
        [Then(@"the order should have at inbetween (.*) and (.*) platters")]
        public void ThenTheOrderShouldHaveAtInbetween___And____Platters(int expectedLow, int expectedHigh)
        {
            var partyOrder = context.Get<Order>("partyOrder");
            partyOrder.Platers.Should().BeGreaterOrEqualTo(expectedLow).And.BeLessOrEqualTo(expectedHigh);
        }
    }
}
