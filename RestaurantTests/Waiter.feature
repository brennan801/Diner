Feature: Waiter
	An employee who tends to multiple parties

Scenario: Waiter gets an order from a party
	Given there is an available waiter
	And there is a party of 4
	When the waiter gets their order
	Then the waiter should recieve an order with a number of appetizers between 0 and 12
	And the waiter should recive an order with a number of platers between 4 and 8
	And the order should be assigned to the correct party

#Scenario: Waiter delivers an order to a party
#	Given there is an available waiter
#	And there is a party of 2
#	When the waiter delivers their order
#	Then 