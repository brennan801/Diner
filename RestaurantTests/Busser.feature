Feature: Busser
	employee that cleans tables and combines tables when neeeded

@mytag
Scenario Outline: 2,3, and 4 tables need to be put together
	Given there are <numberOfTables> tables
	And there is a busser
	When the busser puts them together
	Then they should seat <expectedNumberOfChairs> people
	And should be made up of <expectedNumberOfTables> tables
Examples: 
| numberOfTables | expectedNumberOfChairs | expectedNumberOfTables |
| 2              | 10                     | 2                      |
| 3              | 13                     | 3                      |
| 4              | 16                     | 4                      |

Scenario Outline: Tables of different sizes need to be seperated
	Given there is a table made up of <numberOfTables> tables and <numberOfChairs> chairs
	And there is a busser
	When The busser seperates the tables
	Then There should be <numberOfExpectedTables> tables each with <numberOfExpectedChairs> chairs
Examples: 
| numberOfTables | numberOfChairs | numberOfExpectedTables | numberOfExpectedChairs |
| 2              | 10             | 2                      | 6                      |
| 3              | 13             | 3                      | 6                      |
| 4              | 16             | 4                      | 6                      |