Feature: Host
	Employee that welcomes customers and either adds them to a table, or adds them to the lobby queue

@mytag
Scenario: A party is turned away
	Given a resturant exists
	And a party of 6 walks in
	And the rooms are full
	And The lobby is full
	When the host deals with the customer
	Then the custommer will be sent away

Scenario: A Party is put into the queue
	Given a resturant exists
	And a party of 3 walks in
	And the lobby is empty
	And the rooms are full
	When the host deals with the customer
	Then the customer will be put into the lobby queue

Scenario: A party is assinged a table
	Given a resturant exists
	And a party of 3 walks in
	And the lobby is empty
	And there is available seating in a room
	When the host deals with the customer
	Then the party should be seated at the table

Scenario Outline: Party walks into a resturant and is delt with
	Given a resturant exists
	And a party of <partySize> walks in
	And the lobby has <emptySpots> empty spots 
	And there are <availabeTables> tables available
	When the host deals with the customer
	Then the party should have state <partyState>
Examples: 
| partySize | emptySpots | availabeTables | partyState          |
| 4         | 10         | 0              | "waiting in lobby"  |
| 10        | 20         | 6              | "waiting for table" |
| 5         | 2          | 0              | "turned away"       |
| 5         | 10         | 2              | "seated"            |
| 2         | 0          | 0              | "turned away"       |
| 12        | 20         | 2              | "waiting in lobby"  |


Scenario: A table is open but the lobby queue is empty
	Given a resturant exists
	And the lobby is empty
	And there are 1 tables available
	When the host is told to seat the next customer
	Then the host should return a null party and null table

Scenario Outline: different parties in the queue are assinged tables
	Given a resturant exists
	And the lobby has <numWaitingParties> parties with <numCustomersPerParty> customers
	And there are <numAvaliableTables> tables available
	When the host is told to seat the next customer
	Then There should be <numPartiesLeft> parties left in the queue
	And the party should have state <partyState>
Examples: 
| numWaitingParties | numCustomersPerParty | numAvaliableTables | numPartiesLeft | partyState          |
| 2                 | 3                    | 1                  | 1              | "seated"            |
| 4                 | 8                    | 1                  | 4              | "waiting in lobby"  |
| 4                 | 8                    | 2                  | 3              | "waiting for table" |