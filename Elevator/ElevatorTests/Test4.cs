using Elevator;
using NUnit.Framework;
using Shouldly;
using TestStack.BDDfy;

namespace ElevatorTests
{
    public class Test4 : TestBase
    {
        [Test(Description =
            "Passenger 1 summons lift to go up from Ground. They choose L5. Passenger 2 summons lift to go down from L4. Passenger 3 summons lift to go down from L10. Passengers 2 and 3 choose to travel to Ground.")]
        public void Execute()
        {
            Brains = new Brains(0);

            this
                .Given(x => x.PassengerSummonsElevatorOnFloor(0, DirectionOfTravel.Up))
                .And(x => x.PassengerChoosesFloor(6))
                .And(x => x.PassengerSummonsElevatorOnFloor(4, DirectionOfTravel.Down))
                .And(x => x.PassengerSummonsElevatorOnFloor(10, DirectionOfTravel.Down))
                .And(x => x.Brains.MoveToNextFloor())
                .And(x => x.Brains.MoveToNextFloor())
                .And(x => x.Brains.MoveToNextFloor())
                .And(x => x.PassengerChoosesFloor(0))
                .And(x => x.Brains.MoveToNextFloor())
                .And(x => x.PassengerChoosesFloor(0))
                .When(x => x.WaitUntilElevatorStops())
                .Then(x => x.ElevatorShouldHavePerformedStepsInOrder())
                .BDDfy();
        }

        private void ElevatorShouldHavePerformedStepsInOrder()
        {
            Brains.ExecutedFloors.Dequeue().ShouldBe(4);
            Brains.ExecutedFloors.Dequeue().ShouldBe(6);
            Brains.ExecutedFloors.Dequeue().ShouldBe(10);
            Brains.ExecutedFloors.Dequeue().ShouldBe(0);
            Brains.ExecutedFloors.ShouldBeEmpty();
        }
    }
}