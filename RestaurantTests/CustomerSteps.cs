using FluentAssertions;
using JCsDiner;
using System;
using TechTalk.SpecFlow;

namespace RestaurantTests
{
    [Binding]
    public class CustomerSteps
    {
        private readonly ScenarioContext context;

        public CustomerSteps(ScenarioContext context)
        {
            this.context = context;
        }
        
    }
}
