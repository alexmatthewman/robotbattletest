using System;
using System.Collections.Generic;

namespace RobotBattleNS
{
    public class RobotBattle
    {
        // Used as indentity - only ever incremented when robot added
        public static int robotCount = 0;

        // Robots are stored in the below list. Not much point in maintaining the whole board
        public static List<Robot> robots = new List<Robot>();

        private static string commandGuide = "Valid commands are in format: \nPLACE <x coord>,<y coord>,<[NORTH, SOUTH, EAST, WEST]> \nMOVE\nLEFT\nRIGHT\nREPORT\nROBOT <id to activate>";

        public static void Main(string[] args)
        {
            Console.WriteLine($"Welcome to ROBOT BATTLE! {commandGuide}");
            bool quitNow = false;
            string command;

            while (!quitNow)
            {
                command = Console.ReadLine();
                robotBattleMain(command.Trim().ToUpper());
                if (command.Equals("QUIT")) quitNow = true; // Exit option
            }
        }

        public static void robotBattleMain(string commandOriginal)
        {
            if (!string.IsNullOrWhiteSpace(commandOriginal))
            {
                string command = commandOriginal;
                if (commandOriginal.StartsWith("PLACE")) command = "PLACE"; // Simplify commands with args for easier switch
                if (commandOriginal.StartsWith("ROBOT")) command = "ROBOT";

                switch (command)
                {
                    case "PLACE":
                        placeCommand(commandOriginal);
                        break;

                    case "MOVE":
                        move();
                        break;

                    case "LEFT":
                    case "RIGHT":
                        turn(command);
                        break;

                    case "ROBOT":
                        robotCommand(commandOriginal);
                        break;

                    case "REPORT":
                        report();
                        break;

                    default:
                        Console.WriteLine($"Error: You can't control your robots with {command}. Valid ones are: {commandGuide}.");
                        break;
                }
            }
        }

        /// <summary>
        /// Handle placing a robot
        /// </summary>
        /// <param name="placeArgs">Format expected is PLACE X,Y,F (eg PLACE 0,1,NORTH or PLACE 5,4,WEST etc)</param>
        public static void placeCommand(string placeArgs)
        {
            // Turn input like 'PLACE 1,2,EAST' into variables
            int x = -1;
            int y = -1;
            string orientation = "";
            string xS = "";
            string yS = "";

            // Validate and parse
            // Many ways to bug the PLACE command input so just catch any issues and give correct format. Some specific error messages.
            try
            {
                orientation = placeArgs.Substring(placeArgs.LastIndexOf(',') + 1);

                // Try to turn it just to see if the orientation is valid, leave if error
                if (newOrientation(true, orientation).Equals("")) return;

                xS = placeArgs.Substring(placeArgs.IndexOf(' ') + 1, 1);
                yS = placeArgs.Substring(placeArgs.IndexOf(',') + 1, 1);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: Place command is bugged. Format expected is PLACE X,Y,F (eg PLACE 0,1,NORTH or PLACE 5,4,WEST etc)\n {e}");
                return;
            }

            if (!int.TryParse(xS, out x) || !int.TryParse(yS, out y)) { Console.WriteLine($"Error: Location is not a valid x,y integer co-ordinate"); return; }

            // If valid placement add the robot and set any others inactive
            if (!isValidLocation(x, y) || robotIsAlreadyAtLocation(x, y)) { Console.WriteLine($"Error: location chosen is not valid ({x},{y})"); return; }

            // Everything should be ok so place the robot
            Robot contender = new Robot();
            contender.id = robotCount++;
            contender.x = x;
            contender.y = y;
            contender.orientation = orientation;
            contender.active = true;
            robots.Add(contender);
            setOtherRobotsInactive(contender.id);
        }

        /// <summary>
        /// Set all other robots to inactive
        /// </summary>
        /// <param name="activeRobotID">ID of active robot</param>
        private static void setOtherRobotsInactive(int activeRobotID)
        {
            foreach (Robot robot in robots)
            {
                if (!robot.id.Equals(activeRobotID)) robot.active = false;
            }
        }

        /// <summary>
        /// Returns true if valid location, false if not
        /// </summary>
        /// <param name="xString">Valid between 1 and 5 inclusive</param>
        /// <param name="yString">Valid between 1 and 5 inclusive</param>
        /// <returns>True if valid location, false if not</returns>
        private static bool isValidLocation(int x, int y)
        {
            bool isOK = true;

            // Check here that the args are valid ints in range
            if ((x < 0 || x > 4) || (y < 0 || y > 4))
            {
                Console.WriteLine($"Error: Location is not a valid x,y integer co-ordinate in on the board.");
                isOK = false;
            }

            return isOK;
        }

        /// <summary>
        /// Is a robot already at that location
        /// </summary>
        /// <param name="x">x coordinate to check</param>
        /// <param name="y">y coordinate to check</param>
        /// <returns>True if a robot is at the x,y coordinates, false if not</returns>
        private static bool robotIsAlreadyAtLocation(int x, int y)
        {
            // Check robot not already at those coords
            foreach (Robot robot in robots)
            {
                if (robot.x.Equals(x) && robot.y.Equals(y))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Turn active robot left or right (left string in case of expansion to more complex behaviour).
        /// </summary>
        /// <param name="direction">Should be 'left' or 'right'</param>
        private static void turn(string direction)
        {
            Robot activeRobot = robots.Find(m => m.active.Equals(true));
            if (activeRobot == null) { Console.WriteLine($"Error: No active robot on board."); return; }

            switch (direction)
            {
                case "LEFT":
                    activeRobot.orientation = newOrientation(true, activeRobot.orientation);
                    break;

                case "RIGHT":
                    activeRobot.orientation = newOrientation(false, activeRobot.orientation);
                    break;

                default:
                    // Not really possible to get here but just in case
                    Console.WriteLine($"Error: Valid responses are LEFT and RIGHT.");
                    break;
            }
            robots[activeRobot.id] = activeRobot;
        }

        /// <summary>
        /// Given your turn and current orientation will tell you the direction now facing
        /// </summary>
        /// <param name="left">True if turning left, false for right</param>
        /// <param name="currentOrientation">Should be NORTH,SOUTH,EAST,WEST</param>
        /// <returns>The new direction the robot is facing</returns>
        private static string newOrientation(bool left, string currentOrientation)
        {
            string newDirection = "";
            switch (currentOrientation)
            {
                case "NORTH":
                    newDirection = (left ? "WEST" : "EAST");
                    break;

                case "EAST":
                    newDirection = (left ? "NORTH" : "SOUTH");
                    break;

                case "SOUTH":
                    newDirection = (left ? "EAST" : "WEST");
                    break;

                case "WEST":
                    newDirection = (left ? "SOUTH" : "NORTH");
                    break;

                default:
                    Console.WriteLine($"Error: Robot orientation {currentOrientation} is not valid (should be in [NORTH,SOUTH,EAST,WEST])");
                    break;
            }
            return newDirection;
        }

        /// <summary>
        /// Move the active robot forward one space in the direction it is facing if possible
        /// </summary>
        private static void move()
        {
            Robot activeRobot = robots.Find(m => m.active.Equals(true));
            if (activeRobot == null) { Console.WriteLine($"Error: No active robot on board."); return; }
            int proposedX = activeRobot.x;
            int proposedY = activeRobot.y;

            // Find the proposed new location
            switch (activeRobot.orientation)
            {
                case "NORTH":
                    proposedY++;
                    break;

                case "EAST":
                    proposedX++;
                    break;

                case "SOUTH":
                    proposedY--;
                    break;

                case "WEST":
                    proposedX--;
                    break;
            }

            if (robotIsAlreadyAtLocation(proposedX, proposedY))
            {
                // Robot battle
                Console.WriteLine($"\n***ROBOT BATTLE!!!***");
            }
            else if (isValidLocation(proposedX, proposedY))
            {
                activeRobot.x = proposedX;
                activeRobot.y = proposedY;
            }
            robots[activeRobot.id] = activeRobot;
        }

        /// <summary>
        /// Activate a specific Robot
        /// </summary>
        /// <param name="command">Should be in the form ROBOT ID (eg ROBOT 1 or ROBOT 15 etc)</param>
        private static void robotCommand(string command)
        {
            int robotToActivate = -1;
            // Validate and parse
            try
            {
                string robotS = command.Substring(command.IndexOf(' ') + 1);
                if (!int.TryParse(robotS, out robotToActivate)) { Console.WriteLine($"Error: Robot ID is not valid"); return; }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: Should be in the form ROBOT ID (eg ROBOT 1 or ROBOT 15 etc)\n {e}");
                return;
            }
            Robot activeRobot = robots.Find(m => m.id.Equals(robotToActivate));
            if (null == activeRobot) { Console.WriteLine($"Error: Can't find robot with ID: {robotToActivate}"); return; }
            activeRobot.active = true;
            robots[activeRobot.id] = activeRobot;
            setOtherRobotsInactive(robotToActivate);
        }

        /// <summary>
        /// Output the details of the robots on the board like below
        /// Report:
        /// ID: 0 Location:1,1,WEST, Active=False
        /// ID: 1 Location:5,5,NORTH, Active=False
        /// ID: 2 Location:4,4,NORTH, Active=False
        /// ID: 3 Location:2,1,EAST, Active=True
        /// </summary>
        private static void report()
        {
            Robot activeRobot = robots.Find(m => m.active.Equals(true));
            if (activeRobot == null) { Console.WriteLine($"Error: No active robot on board."); return; }

            Console.WriteLine($"Report:");
            foreach (Robot robot in robots)
            {
                Console.WriteLine($"ID: {robot.id} Location:{robot.x},{robot.y},{robot.orientation}, Active={robot.active}");
            }
        }
    }
}