Feature: Customer
	a single customer inside a party of people


Scenario: a customer orders food
	Given a customer exists
	When the customer is asked to order
	Then the customer should return a number of Appitizers in-between 0 and 3
	And the customer should return a number of Platers that is 1 or 2