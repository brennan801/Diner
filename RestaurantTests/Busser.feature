Feature: Busser
	employee that cleans tables and combines tables when neeeded

Scenario: A busser has to clean a table
	Given there is a restaurant
	Given a busser has an empty queue
	And a table is added to the queue
	When the busser is ran 1 times
	Then the busser should have a current table
	And the time left for the table should be 5
	When the busser is ran 3 more times
	Then the time left for the table should be 2
	When the busser is ran 2 more times
	Then the busser should not have a current table