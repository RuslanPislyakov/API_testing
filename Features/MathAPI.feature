Feature: Math API evaluations

Scenario Outline: Extraction of the square root
	Given the number is <number>
	When I extract the square root
	Then the result should be <result>

	Examples: 
        |number|result|
        |25|5|
		|36|6|
		|16|4|

Scenario Outline: Checking arithmetic calculations
	When I send <expression> on API
	Then the result of expression should be <result>

	Examples: 
        |expression|result|
        |4*5|20|
		|12/2|6|
		|2+3|5|
		|10-3|7|