using System.Collections.Generic;

namespace Elevator
{
    public class Brains
    {
        private readonly Queue<int> _commands = new Queue<int>();

        public int CurrentFloor { get; private set; }
        public Queue<int> ExecutedFloors { get; } = new Queue<int>();

        public Brains(int startingFloor)
        {
            CurrentFloor = startingFloor;
        }

        public void EnqueueFloorRequest(int floor)
        {
            _commands.Enqueue(floor);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>True if a command was executed, false otherwise</returns>
        public bool MoveToNextFloor()
        {
            if (_commands.Count == 0) return false;

            // naive implementation
            var nextFloor = _commands.Dequeue();

            if (nextFloor == CurrentFloor) return true;

            ExecutedFloors.Enqueue(nextFloor);
            CurrentFloor = nextFloor;

            return true;
        }
    }
}