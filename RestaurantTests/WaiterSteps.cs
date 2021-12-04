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

    }
}
