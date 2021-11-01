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
        [Given(@"there is an available waiter")]
        public void GivenThereIsAnAvailableWaiter()
        {
            var waiter = new Waiter();
            context.Add("waiter", waiter);
        }

        [When(@"the waiter gets their order")]
        public void WhenTheWaiterGetsTheirOrder()
        {
            var party = context.Get<Party>("party");
            var waiter = context.Get<Waiter>("waiter");
            var order = waiter.GetAndSendOrder(party);
            context.Add("order", order);
        }
        
        [Then(@"the waiter should recieve an order with a number of appetizers between (.*) and (.*)")]
        public void ThenTheWaiterShouldRecieveAnOrderWithANumberOfAppetizersBetween____And____(int expectedLow, int expectedHigh)
        {
            var order = context.Get<Order>("order");
            order.Appetizers.Should().BeGreaterOrEqualTo(expectedLow).And.BeLessOrEqualTo(expectedHigh);
        }
        
        [Then(@"the waiter should recive an order with a number of platers between (.*) and (.*)")]
        public void ThenTheWaiterShouldReciveAnOrderWithANumberOfPlatersBetween____And____(int expectedLow, int expectedHigh)
        {
            var order = context.Get<Order>("order");
            order.Platers.Should().BeGreaterOrEqualTo(expectedLow).And.BeLessOrEqualTo(expectedHigh);
        }
        [Then(@"the order should be assigned to the correct party")]
        public void ThenTheOrderShouldBeAssignedToTheCorrectParty()
        {
            var order = context.Get<Order>("order");
            var party = context.Get<Party>("party");
            order.Party.Should().BeSameAs(party);
        }

    }
}
