using Elevator;
using NUnit.Framework;
using Shouldly;
using TestStack.BDDfy;

namespace ElevatorTests
{
    public class Test1 : TestBase
    {
        [Test(Description = "Passenger summons lift on the ground floor. Once in chooses to go to level 5.")]
        public void Execute()
        {
            Brains = new Brains(10);
            this
                .Given(x => x.PassengerSummonsElevatorOnFloor(0, DirectionOfTravel.Up))
                .And(x => x.WaitUntilElevatorReachesFloor(0))
                .And(x => x.PassengerChoosesFloor(5))
                .When(x => x.WaitUntilElevatorReachesFloor(5))
                .Then(x => x.ElevatorShouldHavePerformedStepsInOrder())
                .BDDfy();
        }

        private void ElevatorShouldHavePerformedStepsInOrder()
        {
            Brains.ExecutedFloors.Dequeue().ShouldBe(0);
            Brains.ExecutedFloors.Dequeue().ShouldBe(5);
            Brains.ExecutedFloors.ShouldBeEmpty();
        }
    }
}