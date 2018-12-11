using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elevator;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Shouldly;
using TestStack.BDDfy;

namespace ElevatorTests
{
    public class Test1
    {
        readonly Brains _brains = new Brains(10);

        [Test]
        public void Execute()
        {
            this
                .Given(_ => this.PassengerSummonsElevatorOnGroundFloor())
                .And(_ => this.WaitUntilElevatorReachesFloor(0))
                .And(_ => this.PassengerChoosesFloor5())
                .When(_ => this.WaitUntilElevatorReachesFloor(5))
                .Then(_ => this.ElevatorShouldHavePerformedStepsInOrder())
                .BDDfy();
        }

        private void ElevatorShouldHavePerformedStepsInOrder()
        {
            _brains.ExecutedFloors.Dequeue().ShouldBe(0);
            _brains.ExecutedFloors.Dequeue().ShouldBe(5);
            _brains.ExecutedFloors.ShouldBeEmpty();
        }

        private void PassengerChoosesFloor5()
        {
            _brains.EnqueueFloorRequest(5);
        }

        private void WaitUntilElevatorReachesFloor(int floor)
        {
            while (_brains.CurrentFloor != floor)
            {
                _brains.MoveToNextFloor();
            }
        }

        private void PassengerSummonsElevatorOnGroundFloor()
        {
            _brains.EnqueueFloorRequest(0);
        }
    }
}
