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

        [Given(@"a resturant exists")]
        public void GivenAResturantExists()
        {
            Resturant resturant = new();
            context.Add("resturant", resturant);
        }


        [Given(@"a party of (.*) walks in")]
        public void GivenAPartyOf___WalksIn(int partySize)
        {
            Party party = new(partySize, "walked in");
            context.Add("party", party);
        }
        
        [Given(@"The lobby is full")]
        public void GivenTheLobbyIsFull()
        {
            var resturant = context.Get<Resturant>("resturant");
            Lobby lobby = new(4);
            lobby.PartyQueue.Enqueue(new Party(4, "waiting in lobby"));
            resturant.Lobby = lobby;
            Host host = new();
            context.Add("host", host);
            context.Set<Resturant>(resturant, "resturant");
        }
        
        [When(@"the host deals with the customer")]
        public void WhenTheHostDealsWithTheCustomer()
        {
            var host = context.Get<Host>("host");
            var party = context.Get<Party>("party");
            var resturant = context.Get<Resturant>("resturant");
            (var actualParty, var assignedTable) = host.DealWithNewParty(party, resturant);
            context.Add("actualParty", actualParty);
            context.Add("assignedTable", assignedTable);
        }
        
        [Then(@"the custommer will be sent away")]
        public void ThenTheCustommerWillBeSentAway()
        {
            var actualParty = context.Get<Party>("actualParty");
            actualParty.State.Should().Be("turned away");
        }

        [Given(@"the lobby is empty")]
        public void GivenTheLobbyIsEmpty()
        {
            var resturant = context.Get<Resturant>("resturant");
            Lobby lobby = new(50);
            resturant.Lobby = lobby;
            Host host = new();
            context.Add("host", host);
            context.Set<Resturant>(resturant, "resturant");
        }

        [Given(@"the rooms are full")]
        public void GivenTheRoomsAreFull()
        {
            var resturant = context.Get<Resturant>("resturant");
            foreach(Room room in resturant.Rooms)
            {
                foreach(JCsDiner.Table table in room.Tables)
                {
                    table.isOccupied = true;
                }
            }
            context.Set<Resturant>(resturant, "resturant");
        }

        [Then(@"the customer will be put into the lobby queue")]
        public void ThenTheCustomerWillBePutIntoTheLobbyQueue()
        {
            var resturant = context.Get<Resturant>("resturant");
            var actualParty = context.Get<Party>("actualParty");
            actualParty.State.Should().Be("waiting in lobby");
            resturant.Lobby.PartyQueue.Should().HaveCount(1);
            resturant.Lobby.PartyQueue.Should().Contain(actualParty);
        }

        [Given(@"there is available seating in a room")]
        public void GivenThereIsAvailableSeatingInARoom()
        {
            var resturant = context.Get<Resturant>("resturant");
            foreach (Room room in resturant.Rooms)
            {
                foreach (JCsDiner.Table table in room.Tables)
                {
                    table.isOccupied = false;
                }
            }
            context.Set<Resturant>(resturant, "resturant");
        }

        [Then(@"the party should be seated at the table")]
        public void ThenThePartyShouldBeSeatedAtTheTable()
        {
            var actualParty = context.Get<Party>("actualParty");
            var assignedTable = context.Get<JCsDiner.Table>("assignedTable");

            actualParty.State.Should().Be("seated");
            assignedTable.Party.Should().Be(actualParty);
            
        }

        [Given(@"the lobby has (.*) empty spots")]
        public void GivenTheLobbyHas___EmptySpots(int lobbyEmptySpots)
        {
            var resturant = context.Get<Resturant>("resturant");
            Lobby lobby = new(50);
            int partySize = 50 - lobbyEmptySpots;
            Party party = new(partySize, "waiting in lobby");
            lobby.PartyQueue.Enqueue(party);
            resturant.Lobby = lobby;
            Host host = new();
            context.Add("host", host);
            context.Set<Resturant>(resturant, "resturant");
        }

        [Given(@"there are (.*) tables available")]
        public void GivenThereAre___TablesAvailable(int emptyTables)
        {
            var resturant = context.Get<Resturant>("resturant");
            var emptyTableCount = 0;
            foreach (Room room in resturant.Rooms)
            {
                foreach (JCsDiner.Table table in room.Tables)
                {
                    if(emptyTableCount < emptyTables)
                    {
                        table.isOccupied = false;
                        emptyTableCount++;
                    }
                    else
                    {
                        table.isOccupied = true;
                    }
                }
            }
            context.Set<Resturant>(resturant, "resturant");
        }

        [Then(@"the party should have state ""(.*)""")]
        public void ThenThePartyShouldHaveState(string partyState)
        {
            var actualParty = context.Get<Party>("actualParty");

            actualParty.State.Should().Be(partyState);
        }

        [Given(@"the lobby has (.*) parties with (.*) customers")]
        public void GivenTheLobbyHas___PartiesWith____Customers(int numberOfPartys, int numberOfCustomersPerParty)
        {
            var resturant = context.Get<Resturant>("resturant");
            Lobby lobby = new(50);
            
            for(int i = 0; i < numberOfPartys; i++)
            {
                lobby.PartyQueue.Enqueue(new Party(numberOfCustomersPerParty, "waiting in lobby"));
            }

            resturant.Lobby = lobby;
            Host host = new();
            context.Add("host", host);
            context.Set<Resturant>(resturant, "resturant");
        }

        [When(@"the host is told to seat the next customer")]
        public void WhenTheHostIsToldToSeatTheNextCustomer()
        {
            var host = context.Get<Host>("host");
            var resturant = context.Get<Resturant>("resturant");
            var availableRoom = host.getRoomWithMostSpace(resturant);
            Party actualParty;
            JCsDiner.Table assignedTable;
            try
            {
                (actualParty, assignedTable) = host.TrySeatNextCustomer(resturant, availableRoom);
            }
            catch(Exception)
            {
                actualParty = null;
                assignedTable = null;
            }
            context.Add("actualParty", actualParty);
            context.Add("assignedTable", assignedTable);
        }

        [Then(@"the first party to enter the queue should be seated")]
        public void ThenTheFirstPartyToEnterTheQueueShouldBeSeated()
        {
            var actualParty = context.Get<Party>("actualParty");
            var assignedTable = context.Get<JCsDiner.Table>("assignedTable");

            actualParty.State.Should().Be("seated");
            assignedTable.Party.Should().Be(actualParty);

        }

        [Then(@"There should be (.*) parties left in the queue")]
        public void ThenThereShouldBe___PartiesLeftInTheQueue(int numWaitingParties)
        {
            var resturant = context.Get<Resturant>("resturant");
            resturant.Lobby.PartyQueue.Should().HaveCount(numWaitingParties);
        }

        [Then(@"the host should return a null party and null table")]
        public void ThenTheHostShouldReturnANullPartyAndNullTable()
        {
            var actualParty = context.Get<Party>("actualParty");
            var assignedTable = context.Get<JCsDiner.Table>("assignedTable");

            actualParty.Should().BeNull();
            assignedTable.Should().BeNull();
        }


    }
}
