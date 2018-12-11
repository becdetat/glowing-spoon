using Elevator;
using NUnit.Framework;
using Shouldly;
using TestStack.BDDfy;

namespace ElevatorTests
{
    public class MoveToNextFloorTests : TestBase
    {
        [Test]
        public void NoopRequestResultsInNoMovement()
        {
            Brains = new Brains(0);

            this
                .Given(x => x.PassengerSummonsElevatorOnFloor(0, DirectionOfTravel.Up))
                .Then(x => x.ElevatorDidNotMove())
                .BDDfy();
        }

        [Test]
        public void RequestGoingDownIsSkippedWhenElevatorIsGoingUp()
        {
            Brains = new Brains(10);

            this
                .Given(x => x.PassengerChoosesFloor(12))
                .And(x => x.PassengerChoosesFloor(5))
                .And(x => x.PassengerChoosesFloor(15))
                .And(x => x.MoveToNextFloor())
                .And(x => x.MoveToNextFloor())
                .Then(x => x.CurrentFloorIs(15))
                .BDDfy();
        }

        [Test]
        public void RequestGoingUpIsSkippedWhenElevatorIsGoingDown()
        {
            Brains = new Brains(10);

            this
                .Given(x => x.PassengerChoosesFloor(8))
                .And(x => x.PassengerChoosesFloor(15))
                .And(x => x.PassengerChoosesFloor(5))
                .And(x => x.MoveToNextFloor())
                .And(x => x.MoveToNextFloor())
                .Then(x => x.CurrentFloorIs(5))
                .BDDfy();
        }

        [Test]
        public void SummonRequestGoingInOtherDirectionIsSkipped()
        {
            Brains = new Brains(10);

            this
                .Given(x => x.PassengerChoosesFloor(12))
                .And(x => x.PassengerSummonsElevatorOnFloor(5, DirectionOfTravel.Up))
                .And(x => x.PassengerChoosesFloor(15))
                .And(x => x.MoveToNextFloor())
                .And(x => x.MoveToNextFloor())
                .Then(x => x.CurrentFloorIs(15))
                .BDDfy();
        }

        [Test]
        public void ClosestRequestedFloorIsSelected()
        {
            Brains = new Brains(10);

            this
                .Given(x => x.PassengerChoosesFloor(12))
                .And(x => x.PassengerChoosesFloor(18))
                .And(x => x.PassengerChoosesFloor(14))
                .And(x => x.MoveToNextFloor())
                .And(x => x.MoveToNextFloor())
                .Then(x => x.CurrentFloorIs(14))
                .BDDfy();
        }

        private void CurrentFloorIs(int floor)
        {
            Brains.CurrentFloor.ShouldBe(floor);
        }

        private void ElevatorDidNotMove()
        {
            Brains.ExecutedFloors.ShouldBeEmpty();
        }

        private void MoveToNextFloor()
        {
            Brains.MoveToNextFloor();
        }


    }
}