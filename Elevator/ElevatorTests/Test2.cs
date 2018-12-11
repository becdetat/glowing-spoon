using Elevator;
using NUnit.Framework;
using Shouldly;
using TestStack.BDDfy;

namespace ElevatorTests
{
    public class Test2
    {
        private readonly Brains _brains = new Brains(0);

        [Test]
        public void Execute()
        {
            this
                .Given(_ => this.PassengerSummonsElevatorOnFloor(6))
                .And(_ => this.PassengerSummonsElevatorOnFloor(4))
                .And(_ => this.WaitUntilElevatorReachesFloor(6))
                .And(_ => this.PassengerChoosesFloor(1))
                .And(_ => this.WaitUntilElevatorReachesFloor(4))
                .And(_ => this.PassengerChoosesFloor(1))
                .When(_ => this.WaitUntilElevatorStops())
                .Then(_ => this.ElevatorShouldHavePerformedStepsInOrder())
                .BDDfy();
        }

        private void PassengerSummonsElevatorOnFloor(int floor)
        {
            _brains.EnqueueFloorRequest(floor);
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

        private void ElevatorShouldHavePerformedStepsInOrder()
        {
            _brains.ExecutedFloors.Dequeue().ShouldBe(6);
            _brains.ExecutedFloors.Dequeue().ShouldBe(4);
            _brains.ExecutedFloors.Dequeue().ShouldBe(1);
            _brains.ExecutedFloors.ShouldBeEmpty();
        }
    }
}