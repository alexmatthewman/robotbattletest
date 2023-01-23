Feature: RobotBattle
#Simple BDD for the Robot Battle project

@positive
Scenario: Place a robot, move it and report
	Given I have placed a robot on the board
	When I move it a step ahead within bounds
	Then the report should show the correct location