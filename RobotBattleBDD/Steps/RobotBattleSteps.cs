using System;
using TechTalk.SpecFlow;

namespace RobotBattleBDD.Steps
{
    [Binding]
    public class RobotBattleSteps
    {
        [Given(@"I have placed a robot on the board")]
        public void GivenIHavePlacedARobotOnTheBoard()
        {
            RobotBattleTest.Program battle = new RobotBattleTest.Program();            
            battle.
        }
        
        [When(@"I move it a step ahead within bounds")]
        public void WhenIMoveItAStepAheadWithinBounds()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"the report should show the correct location")]
        public void ThenTheReportShouldShowTheCorrectLocation()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
