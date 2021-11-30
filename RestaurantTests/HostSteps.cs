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

    }
}
