using FluentAssertions;
using JCsDiner;
using System;
using System.Collections.Generic;
using TechTalk.SpecFlow;

namespace RestaurantTests
{
    [Binding]
    public class BusserSteps
    {
        private readonly ScenarioContext context;

        public BusserSteps(ScenarioContext context)
        {
            this.context = context;
        }
        [Given(@"a busser has an empty queue")]
        public void GivenABusserHasAnEmptyQueue()
        {
            var busser = new Busser();
            context.Add("busser", busser);
        }

        [Given(@"a table is added to the queue")]
        public void GivenATableIsAddedToTheQueue()
        {
            var busser = context.Get<Busser>("busser");
            var restaurant = context.Get<Restaurant>("restaurant");
            restaurant.Rooms[0].Tables[0].State = "dirty";
        }

        [When(@"the busser is ran (.*) times")]
        [When(@"the busser is ran (.*) more times")]
        public void WhenTheBusserIsRan___Times(int timeRun)
        {
            var restaurant = context.Get<Restaurant>("restaurant");
            var busser = context.Get<Busser>("busser");
            for (int i = 0; i < timeRun; i++)
            {
                busser.Run1(restaurant, i);
            }
        }

        [Then(@"the busser should have a current table")]
        public void ThenTheBusserShouldHaveACurrentTable()
        {
            var busser = context.Get<Busser>("busser");
            busser.CurrentTable.Should().NotBeNull();
        }

        [Then(@"the busser should not have a current table")]
        public void ThenTheBusserShouldNotHaveACurrentTable()
        {
            var busser = context.Get<Busser>("busser");
            busser.CurrentTable.Should().BeNull();
        }


        [Then(@"the time left for the table should be (.*)")]
        public void ThenTheTimeLeftForTheTableShouldBe(int expectedTimeLeft)
        {
            var busser = context.Get<Busser>("busser");
            busser.CurrentTableTimeLeft.Should().Be(expectedTimeLeft);
        }

    }
}
