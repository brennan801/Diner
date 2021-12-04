using FluentAssertions;
using JCsDiner;
using System;
using System.Linq;
using TechTalk.SpecFlow;

namespace RestaurantTests
{
    [Binding]
    public class CookSteps
    {
        private readonly ScenarioContext context;

        public CookSteps(ScenarioContext context)
        {
            this.context = context;
        }
        [Given(@"there is a cook")]
        public void GivenThereIsACook()
        {
            var cook = new Cook();
            context.Add("cook", cook);
        }
        
        [Given(@"There are (.*) orders that are ready to be cooked with (.*) platers and (.*) appitizers")]
        public void GivenThereAre___OrdersThatAreReadyToBeCooked(int numOfOrders, int numPlaters, int numAppitizers)
        {
            var restaurant = context.Get<Restaurant>("restaurant");
            for (int i = 0; i < numOfOrders; i++)
            {
                var order = new Order();
                order.State = "ToBeCooked";
                order.Platers = numPlaters;
                order.Appetizers = numAppitizers;
                restaurant.CurrentOrders.Add(order);
            }
            context.Set<Restaurant>(restaurant, "restaurant");
        }
        
        [When(@"the cook is run (.*) times")]
        [When(@"the cook is run (.*) more times")]
        public void WhenTheCookIsRun___Times(int runTime)
        {
            var cook = context.Get<Cook>("cook");
            var restaurant = context.Get<Restaurant>("restaurant");

            for (int i = 0; i < runTime; i++)
            {
                cook.Run1(restaurant);
            }
        }
        
        [Then(@"there should be (.*) orders in the ready to be cooked state")]
        public void ThenThereShouldBe___OrdersInTheReadyToBeCookedState(int numOrders)
        {
            var restaurant = context.Get<Restaurant>("restaurant");
            var orderQuery =
                from order in restaurant.CurrentOrders
                where order.State == "ToBeCooked"
                orderby order.WaitCounter
                select order;
            orderQuery.Should().HaveCount(numOrders);
        }
        
        [Then(@"there should be (.*) orders in the being cooked state")]
        public void ThenThereShouldBe___OrdersInTheBeingCookedState(int numOrders)
        {
            var restaurant = context.Get<Restaurant>("restaurant");
            var orderQuery =
                from order in restaurant.CurrentOrders
                where order.State == "BeingCooked"
                orderby order.WaitCounter
                select order;
            orderQuery.Should().HaveCount(numOrders);
        }
    }
}
