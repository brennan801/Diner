Feature: Party
	A party that comes into a resturaunt 

Scenario: the party orders food
	Given there is a party of 2
	When the party orders food
	Then the order should have at inbetween 2 and 4 platters
