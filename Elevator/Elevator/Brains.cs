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

        public void MoveToNextFloor()
        {
            if (_commands.Count == 0) return;

            // naive implementation
            var nextFloor = _commands.Dequeue();

            ExecutedFloors.Enqueue(nextFloor);
            CurrentFloor = nextFloor;
        }
    }
}