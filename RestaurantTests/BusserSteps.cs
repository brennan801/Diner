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
        [Given(@"there are (.*) tables")]
        public void GivenThereAre___Tables(int numberOfTables)
        {
            List<JCsDiner.Table> tables = new List<JCsDiner.Table>();
            for(int i = 0; i < numberOfTables; i++)
            {
                tables.Add(new JCsDiner.Table());
            }
            context.Add("tables", tables);
        }

        [Given(@"there is a busser")]
        public void GivenThereIsABusser()
        {
            Busser busser = new Busser();
            context.Add("busser", busser);
        }


        [When(@"the busser puts them together")]
        public void WhenTheBusserPutsThemTogether()
        {
            var tables = context.Get<List<JCsDiner.Table>>("tables");
            var busser = context.Get<Busser>("busser");
            var actualTable = busser.CombineTables(tables);
            context.Add("actualTable", actualTable);
        }
        
        [Then(@"they should seat (.*) people")]
        public void ThenTheyShouldSeatPeople(int expectedNumberOfPeople)
        {
            var actualTable = context.Get<JCsDiner.Table>("actualTable");
            actualTable.numOfChairs.Should().Be(expectedNumberOfPeople);
        }

        [Then(@"should be made up of (.*) tables")]
        public void ThenShouldBeMadeUpOf(int expectedNuberOfTables)
        {
            var actualTable = context.Get<JCsDiner.Table>("actualTable");
            actualTable.numOfTables.Should().Be(expectedNuberOfTables);
        }

        [Given(@"there is a table made up of (.*) tables and (.*) chairs")]
        public void GivenThereIsATableMadeUpOf____TablesAnd____Chairs(int numberOfTables, int numberOfChairs)
        {
            JCsDiner.Table combinedTable = new JCsDiner.Table(numberOfTables, numberOfChairs);
            context.Add("combinedTable", combinedTable);
        }

        [When(@"The busser seperates the tables")]
        public void WhenTheBusserSeperatesTheTables()
        {
            var busser = context.Get<Busser>("busser");
            var combinedTable = context.Get<JCsDiner.Table>("combinedTable");
            var seperatedTables = busser.SeperateTables(combinedTable);
            context.Add("seperatedTables", seperatedTables);
        }

        [Then(@"There should be (.*) tables each with (.*) chairs")]
        public void ThenThereShouldBe____TablesEachWith____Chairs(int expecedNumberOfTables, int expectedNumberOfChairs)
        {
            var seperatedTables = context.Get<List<JCsDiner.Table>>("seperatedTables");
            seperatedTables.Count.Should().Be(expecedNumberOfTables);

            foreach (JCsDiner.Table table in seperatedTables)
            {
                table.numOfChairs.Should().Be(expectedNumberOfChairs);
            }
        }


    }
}
