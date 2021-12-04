Feature: Host
	Employee that welcomes customers and either adds them to a table, or adds them to the lobby queue


Scenario: A Host is running and a party enters
	Given there is a restaurant 
	And a host is in the free state
	When the host is ran 1 times
	Then the host should be in the free state
	When there are 1 parties waiting in the lobby
	And the host is ran 1 times
	Then the host should be in the Seating Party State

Scenario: A Host seats two customers waiting in the lobby
	Given there is a restaurant
	And there are 2 parties waiting in the lobby
	And a host is in the free state
	When the host is ran 3 times
	Then the host should be in the free state
	And there should be 1 parties waiting in the lobby
	When the host is ran 2 more times
	Then the host should be in the Seating Party State
	When the host is ran 1 more times
	Then the host should be in the free state
	And there should be 0 parties waiting in the lobby