using System;
using System.Collections.Generic;
using System.Linq;

namespace Elevator
{
    public class Brains
    {
        private readonly IList<Request> _requests = new List<Request>();

        public Brains(int startingFloor)
        {
            CurrentFloor = startingFloor;
        }

        public int CurrentFloor { get; private set; }
        public DirectionOfTravel CurrentDirectionOfTravel { get; private set; }
        public Queue<int> ExecutedFloors { get; } = new Queue<int>();

        public void EnqueueFloorRequest(int floor)
        {
            _requests.Add(new FloorRequest(floor));

            if (_requests.Count == 1) this.SetInitialDirectionOfTravel();
        }

        public void EnqueueSummonRequest(int floor, DirectionOfTravel directionOfTravel)
        {
            _requests.Add(new SummonRequest(floor, directionOfTravel));

            if (_requests.Count == 1) this.SetInitialDirectionOfTravel();
        }

        private void SetInitialDirectionOfTravel()
        {
            if (_requests.Count != 1) return;

            CurrentDirectionOfTravel = _requests.First().Floor > CurrentFloor
                ? DirectionOfTravel.Up
                : DirectionOfTravel.Down;
        }

        /// <summary>
        /// </summary>
        /// <returns>True if a command was executed, false otherwise</returns>
        public bool MoveToNextFloor()
        {
            if (_requests.Count == 0) return false;

            var orderedRequests = _requests
                .OrderBy(x => Math.Abs(x.Floor - CurrentFloor))
                .ToList();

            var nextRequest = orderedRequests
                .FirstOrDefault(x =>
                {
                    if (x.Floor == CurrentFloor) return true;
                    if (x is SummonRequest && ((SummonRequest) x).DirectionOfTravel != CurrentDirectionOfTravel) return false;
                    if (CurrentDirectionOfTravel == DirectionOfTravel.Down && x.Floor < CurrentFloor) return true;
                    if (CurrentDirectionOfTravel == DirectionOfTravel.Up && x.Floor > CurrentFloor) return true;

                    return false;
                })
                ?? orderedRequests.First();
            
            _requests.Remove(nextRequest);

            if (nextRequest.Floor == CurrentFloor) return true;

            ExecutedFloors.Enqueue(nextRequest.Floor);
            CurrentFloor = nextRequest.Floor;

            return true;
        }
    }
}