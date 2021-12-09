Feature: Waiter
	An employee who tends to multiple parties

Scenario: A free waiter is run when there are orders to return
	Given there is a restaurant
	And there is a waiter for room 2
	And there are 2 parties in the waiting to order state
	And there are 2 parties in the waiting for check state
	And each party has an order in the ToBeReturned State
	When the waiter is run 1 times
	Then the waiter should be in the returning order state

Scenario: A free waiter is run when there are parties waiting to order
	Given there is a restaurant
	And there is a waiter for room 2
	And there are 2 parties in the waiting to order state
	And there are 2 parties in the waiting for check state
	When the waiter is run 1 times
	Then the waiter should be in the getting order state

Scenario: A free waiter is run when there are parties waiting for a check
	Given there is a restaurant
	And there is a waiter for room 2
	And there are 2 parties in the waiting for check state
	When the waiter is run 1 times
	Then the waiter should be in the providing check state

Scenario: A free waiter is run when there is nothing for the waiter to do
	Given there is a restaurant
	And there is a waiter for room 2
	When the waiter is run 1 times
	Then the waiter should be in the Free state
	And the waiter should have a free counter of 1



