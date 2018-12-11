using Elevator;
using NUnit.Framework;
using Shouldly;
using TestStack.BDDfy;

namespace ElevatorTests
{
    public class Test2 : TestBase
    {
        [Test(Description =
            "Passenger summons lift on level 6 to go down. Passenger on level 4 summons the lift to go down. They both choose L1.")]
        public void Execute()
        {
            Brains = new Brains(0);

            this
                .Given(x => x.PassengerSummonsElevatorOnFloor(6, DirectionOfTravel.Down))
                .And(x => x.PassengerSummonsElevatorOnFloor(4, DirectionOfTravel.Down))
                .And(x => x.PassengerChoosesFloor(1))
                .And(x => x.PassengerChoosesFloor(1))
                .When(x => x.WaitUntilElevatorStops())
                .Then(x => x.ElevatorShouldHavePerformedStepsInOrder())
                .BDDfy();
        }

        private void ElevatorShouldHavePerformedStepsInOrder()
        {
            Brains.ExecutedFloors.Dequeue().ShouldBe(1);
            Brains.ExecutedFloors.Dequeue().ShouldBe(4);
            Brains.ExecutedFloors.Dequeue().ShouldBe(6);
            Brains.ExecutedFloors.ShouldBeEmpty();
        }
    }
}