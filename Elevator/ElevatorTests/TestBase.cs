using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elevator;

namespace ElevatorTests
{
    public abstract class TestBase
    {
        protected Brains Brains;

        protected void PassengerSummonsElevatorOnFloor(int floor, DirectionOfTravel directionOfTravel)
        {
            Brains.EnqueueSummonRequest(floor, directionOfTravel);
        }

        protected void PassengerChoosesFloor(int floor)
        {
            Brains.EnqueueFloorRequest(floor);
        }

        protected void WaitUntilElevatorReachesFloor(int floor)
        {
            while (Brains.CurrentFloor != floor) Brains.MoveToNextFloor();
        }

        protected void WaitUntilElevatorStops()
        {
            while (Brains.MoveToNextFloor()) ;
        }
    }
}
