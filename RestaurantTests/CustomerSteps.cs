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
        [Given(@"a customer exists")]
        public void GivenACustomerExists()
        {
            var customer = new Customer();
            context.Add("customer", customer);
        }
        
        [When(@"the customer is asked to order")]
        public void WhenTheCustomerIsAskedToOrder()
        {
            var customer = context.Get<Customer>("customer");
            var customerOrder = customer.Order();
            context.Add("customerOrder", customerOrder);
        }
        
        [Then(@"the customer should return a number of Appitizers in-between (.*) and (.*)")]
        public void ThenTheCustomerShouldReturnANumberOfAppitizersInBetween____and____(int expectedLow, int expectedHigh)
        {
            (int actualAppitizers, _) = context.Get<(int, int)>("customerOrder");
            actualAppitizers.Should().BeGreaterOrEqualTo(expectedLow).And.BeLessOrEqualTo(expectedHigh);

        }
        
        [Then(@"the customer should return a number of Platers that is (.*) or (.*)")]
        public void ThenTheCustomerShouldReturnANumberOfPlatersThatIs____or____(int expectedLow, int expectedHigh)
        {
            (_, int actualPlaters) = context.Get<(int, int)>("customerOrder");
            actualPlaters.Should().BeGreaterOrEqualTo(expectedLow).And.BeLessOrEqualTo(expectedHigh);
        }
    }
}
