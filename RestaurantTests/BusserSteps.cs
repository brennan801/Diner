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
        


    }
}
