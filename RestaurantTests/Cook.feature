Feature: Cook
	An employee that cooks ready to be cooked orders and sets them out to delivery

Scenario: Two orders get cooked
	Given there is a restaurant
	And there is a cook
	And There are 2 orders that are ready to be cooked with 2 platers and 1 appitizers
	When the cook is run 1 times
	Then there should be 1 orders in the ready to be cooked state
	And there should be 1 orders in the being cooked state