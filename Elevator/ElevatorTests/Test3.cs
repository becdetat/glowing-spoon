using Elevator;
using NUnit.Framework;
using Shouldly;
using TestStack.BDDfy;

namespace ElevatorTests
{
    public class Test3 : TestBase
    {
        [Test(Description =
            "Passenger 1 summons lift to go up from L2. Passenger 2 summons lift to go down from L4. Passenger 1 chooses to go to L6. Passenger 2 chooses to go to Ground Floor")]
        public void Execute()
        {
            Brains = new Brains(0);

            this
                .Given(x => x.PassengerSummonsElevatorOnFloor(2, DirectionOfTravel.Up))
                .And(x => x.PassengerSummonsElevatorOnFloor(4, DirectionOfTravel.Down))
                .And(x => x.WaitUntilElevatorReachesFloor(2))
                .And(x => x.PassengerChoosesFloor(6))
                .And(x => x.WaitUntilElevatorReachesFloor(4))
                .And(x => x.PassengerChoosesFloor(0))
                .When(x => x.WaitUntilElevatorStops())
                .Then(x => x.ElevatorShouldHavePerformedStepsInOrder())
                .BDDfy();
        }

        private void ElevatorShouldHavePerformedStepsInOrder()
        {
            Brains.ExecutedFloors.Dequeue().ShouldBe(2);
            Brains.ExecutedFloors.Dequeue().ShouldBe(4);
            Brains.ExecutedFloors.Dequeue().ShouldBe(6);
            Brains.ExecutedFloors.Dequeue().ShouldBe(0);
            Brains.ExecutedFloors.ShouldBeEmpty();
        }
    }
}