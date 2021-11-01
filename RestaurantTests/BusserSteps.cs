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
        [Given(@"there are (.*) tables in a room")]
        public void GivenThereAre___TablesInARom(int numberOfTables)
        {
            Room room = new Room(numberOfTables);
            context.Add("room", room);
        }

        [Given(@"there is a busser")]
        public void GivenThereIsABusser()
        {
            Busser busser = new Busser();
            context.Add("busser", busser);
        }


        [When(@"the busser puts tables together for a party")]
        public void WhenTheBusserPutsTablesTogetherForAParty()
        {
            var busser = context.Get<Busser>("busser");
            var room = context.Get<Room>("room");
            var party = context.Get<Party>("party");
            (var actualParty, var actaulTable) = busser.CombineTablesForParty(party, room);
            context.Add("actualTable", actaulTable);
            context.Add("actualParty", actualParty);
        }


        [Then(@"a table should be created that should seat (.*) people")]
        public void ThenATableShouldBeCreatedThatShouldSeat____People(int expectedNumberOfPeople)
        {
            var actualTable = context.Get<JCsDiner.Table>("actualTable");
            actualTable.numOfChairs.Should().Be(expectedNumberOfPeople);
        }

        [Then(@"the table should be made up of (.*) tables")]
        public void ThenTheTableShouldBeMadeUpOf___Tables(int expectedNuberOfTables)
        {
            var actualTable = context.Get<JCsDiner.Table>("actualTable");
            actualTable.numOfTables.Should().Be(expectedNuberOfTables);
        }

        [Given(@"there is a table made up of (.*) tables and (.*) chairs in a room")]
        public void GivenThereIsATableMadeUpOf____TablesAnd____ChairsInARoom(int numberOfTables, int numberOfChairs)
        {
            JCsDiner.Table combinedTable = new JCsDiner.Table(numberOfTables, numberOfChairs);
            context.Add("combinedTable", combinedTable);
            List<JCsDiner.Table> tablesInRoom = new();
            tablesInRoom.Add(combinedTable);
            Room room = new(tablesInRoom);
            context.Add("room", room);
        }

        [When(@"The busser seperates the tables")]
        public void WhenTheBusserSeperatesTheTables()
        {
            var busser = context.Get<Busser>("busser");
            var combinedTable = context.Get<JCsDiner.Table>("combinedTable");
            var room = context.Get<Room>("room");
            var seperatedTables = busser.SeperateTables(room, combinedTable);
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
