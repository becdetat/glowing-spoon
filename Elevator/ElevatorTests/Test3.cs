using Elevator;
using NUnit.Framework;
using Shouldly;
using TestStack.BDDfy;

namespace ElevatorTests
{
    public class Test3
    {
        private readonly Brains _brains = new Brains(0);

        [Test(Description =
            "Passenger 1 summons lift to go up from L2. Passenger 2 summons lift to go down from L4. Passenger 1 chooses to go to L6. Passenger 2 chooses to go to Ground Floor")]
        public void Execute()
        {
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
            _brains.ExecutedFloors.Dequeue().ShouldBe(2);
            _brains.ExecutedFloors.Dequeue().ShouldBe(6);
            _brains.ExecutedFloors.Dequeue().ShouldBe(4);
            _brains.ExecutedFloors.Dequeue().ShouldBe(0);
            _brains.ExecutedFloors.ShouldBeEmpty();
        }

        private void PassengerSummonsElevatorOnFloor(int floor, DirectionOfTravel directionOfTravel)
        {
            _brains.EnqueueSummonRequest(floor, directionOfTravel);
        }

        private void WaitUntilElevatorReachesFloor(int floor)
        {
            while (_brains.CurrentFloor != floor && _brains.MoveToNextFloor()) ;
        }

        private void PassengerChoosesFloor(int floor)
        {
            _brains.EnqueueFloorRequest(floor);
        }

        private void WaitUntilElevatorStops()
        {
            while (_brains.MoveToNextFloor()) ;
        }
    }
}