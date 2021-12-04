Feature: Party
	A party that comes into a resturaunt 


Scenario: A party enters and is run
	Given there is a restaurant
	And a party is in the Entered state
	When the party is run
	Then the parties new state should be the Entered state

Scenario: A party pays and leaves after reciving the check
	Given there is a restaurant
	And a party is in the RecievedCheck state
	When the party is run
	Then the parties new state should be the Left state

Scenario: A party decides what to order
	Given there is a restaurant
	And there is a party of size 4 
	And the party is deciding what to order
	When the party is ran 4 times
	Then the parties new state should be the DecidingOrder state
	When the party is ran 5 more times
	Then the parties new state should be the WaitingToOrder state

Scenario: A party orders
	Given there is a restaurant
	And there is a party of size 3
	And the party is ordering
	When the party is ran 2 times
	Then the parties new state should be the Ordering state
	When the party is ran 2 more times
	Then the parties new state should be the WaitingForFood state

Scenario: A party eats
	Given there is a restaurant
	And there is a party of size 1
	And the party is eating an order with 2 platters and 1 appitizers
	When the party is ran 10 times
	Then the parties new state should be the Eating state
	When the party is ran 3 more times
	Then the parties new state should be WaitingForCheck