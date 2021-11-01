Feature: Busser
	employee that cleans tables and combines tables when neeeded

@mytag
Scenario Outline: 2,3, and 4 tables need to be put together
	Given there are <numberOfTablesInRoom> tables in a room
	And there is a busser
	And there is a party of <partySize>
	When the busser puts tables together for a party
	Then a table should be created that should seat <expectedNumberOfChairs> people
	And the table should be made up of <expectedNumberOfTables> tables
	And the party should have state <partyState>
Examples: 
| numberOfTablesInRoom | partySize | expectedNumberOfChairs | expectedNumberOfTables | partyState |
| 2                    | 8         | 10                     | 2                      | "seated"   |
| 3                    | 12        | 13                     | 3                      | "seated"   |
| 4                    | 15        | 16                     | 4                      | "seated"   |

Scenario Outline: Tables of different sizes need to be seperated
	Given there is a table made up of <numberOfTables> tables and <numberOfChairs> chairs in a room
	And there is a busser
	When The busser seperates the tables
	Then There should be <numberOfExpectedTables> tables each with <numberOfExpectedChairs> chairs
Examples: 
| numberOfTables | numberOfChairs | numberOfExpectedTables | numberOfExpectedChairs |
| 2              | 10             | 2                      | 6                      |
| 3              | 13             | 3                      | 6                      |
| 4              | 16             | 4                      | 6                      |