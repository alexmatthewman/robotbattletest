using Microsoft.VisualStudio.TestTools.UnitTesting;
using RobotBattleNS;

namespace RobotBattleTests
{
    [TestClass]
    public class RobotBattlUnitTests
    {
        [TestMethod]
        public void Main_IncorrectCommandHandling()
        {
            // Arrange
            string neg_Args = "";
            string neg_Args1 = "BOB";

            string negPlace_badCoords = "PLACE -1,0,NORTH";
            string negPlace_badCoords1 = "PLACE 0,5,NORTH";

            string negPlace_badFacing = "PLACE 1,1,BOB";

            string negPlace_badArgs = "PLACE 1,1";
            string negPlace_badArgs1 = "PLACE 1";
            string negPlace_badArgs2 = "PLACE";
            string negPlace_badArgs3 = "PLACE S,T,NORTH";
            string negPlace_badArgs4 = "PLACE 1,1,NORTH,1";

            string posMove = "MOVE";
            string posLeft = "LEFT";
            string posRight = "RIGHT";
            string posReport = "REPORT";
            string posRobot = "ROBOT 0";

            // Act
            RobotBattle.robotBattleMain(neg_Args);
            RobotBattle.robotBattleMain(neg_Args1);

            // Shouldn't work before a robot is placed correctly
            RobotBattle.robotBattleMain(posMove);
            RobotBattle.robotBattleMain(posLeft);
            RobotBattle.robotBattleMain(posRight);
            RobotBattle.robotBattleMain(posReport);
            RobotBattle.robotBattleMain(posRobot);

            RobotBattle.robotBattleMain(negPlace_badCoords);
            RobotBattle.robotBattleMain(negPlace_badCoords1);
            RobotBattle.robotBattleMain(negPlace_badFacing);
            RobotBattle.robotBattleMain(negPlace_badArgs);
            RobotBattle.robotBattleMain(negPlace_badArgs1);
            RobotBattle.robotBattleMain(negPlace_badArgs2);
            RobotBattle.robotBattleMain(negPlace_badArgs3);
            RobotBattle.robotBattleMain(negPlace_badArgs4);

            // Assert
            System.Console.WriteLine($"RobotBattle.robotCount: {RobotBattle.robotCount}");
            Assert.IsTrue(RobotBattle.robotCount.Equals(0), "Robot count should be zero still {RobotBattle.robotCount}");
        }

        [TestMethod]
        public void placeCommand()
        {
            // Arrange
            string posPlace = "PLACE 0,0,NORTH";
            string posPlace2 = "PLACE 0,1,NORTH";
            string posPlace3 = "PLACE 0,2,SOUTH";

            // Act
            RobotBattle.robotBattleMain(posPlace);
            RobotBattle.robotBattleMain(posPlace2);
            RobotBattle.robotBattleMain(posPlace3);
            RobotBattle.robotBattleMain(posPlace3);

            // Assert
            Assert.IsTrue(RobotBattle.robotCount == 3);

            // Clean up
            RobotBattle.robots.Clear();
            RobotBattle.robotCount = 0;
        }
    }
        
        // That's enough unit tests for a mock up - normally 100% coverage if reasonable and maybe depending on BDD coverage
}