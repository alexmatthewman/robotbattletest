# Robot Battle Test
This is a simple command line app based on the challenge here: https://github.com/ioof-holdings/recruitment/wiki/Robot-Challenge

## Startup
You can simply run the exe file if you want to test functionality or it can be compiled etc in VS or VS Code.

## Example function
The application is a simulation of a toy robot moving on a square tabletop, of dimensions 5 units x 5 units.

There are no other obstructions on the table surface.

The robot is free to roam around the surface of the table, and is prevented from falling to destruction. 

It reads in commands of the following form:

PLACE X,Y,F
MOVE
LEFT
RIGHT
REPORT

### PLACE 
Will put the toy robot on the table in position X,Y and facing NORTH, SOUTH, EAST or WEST. The origin (0,0) can be considered to be the SOUTH WEST most corner.

It is required that the first command to the robot is a PLACE command, after that, any sequence of commands may be issued, in any order, including another PLACE command. 

The application discards all commands in the sequence until a valid PLACE command has been executed.

### MOVE
This will move the toy robot one unit forward in the direction it is currently facing.

### LEFT/RIGHT
These will rotate the robot 90 degrees in the specified direction without changing the position of the robot.

### ROBOT <ID number>
This changes the active robot to the ID number passed. It can then be moved etc.

### REPORT 
This will announce the details of the robots. Format is:

Report:

ID: 0 Location:1,1,WEST, Active=False

ID: 1 Location:5,5,NORTH, Active=False

ID: 2 Location:4,4,NORTH, Active=False

ID: 3 Location:2,1,EAST, Active=True

## Test Input

MOVE
LEFT
RIGHT
REPORT
PLACE 0,0,NORTH
PLACE 5,5,SOUTH
PLACE 0,1,SOUTH
REPORT
MOVE
LEFT
MOVE
LEFT
LEFT
LEFT
MOVE
MOVE
MOVE
REPORT
ROBOT 1
MOVE
REPORT