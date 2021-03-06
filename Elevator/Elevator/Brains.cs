﻿using System;
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
        public Queue<int> ExecutedFloors { get; } = new Queue<int>();

        public void EnqueueFloorRequest(int floor)
        {
            _requests.Add(new FloorRequest(floor));
        }

        public void EnqueueSummonRequest(int floor, DirectionOfTravel directionOfTravel)
        {
            _requests.Add(new SummonRequest(floor, directionOfTravel));
        }

        /// <summary>
        /// </summary>
        /// <returns>True if a command was executed, false otherwise</returns>
        public bool MoveToNextFloor()
        {
            if (_requests.Count == 0) return false;

            var nextRequest = _requests
                .Permute()
                .OrderBy(x =>
                {
                    var permutation = x.ToList();
                    var distanceTraveled = Math.Abs(CurrentFloor - permutation[0].Floor);

                    for (var i = 0; i < permutation.Count - 1; i++)
                    {
                        distanceTraveled += Math.Abs(permutation[i].Floor - permutation[i + 1].Floor);
                    }

                    return distanceTraveled;
                })
                .First()
                .First();
            
            _requests.Remove(nextRequest);

            if (nextRequest.Floor == CurrentFloor) return true;

            ExecutedFloors.Enqueue(nextRequest.Floor);
            CurrentFloor = nextRequest.Floor;

            return true;
        }
    }
}