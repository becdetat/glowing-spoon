using System.Collections.Generic;

namespace Elevator
{
    public class Brains
    {
        private readonly Queue<string> _commands = new Queue<string>();

        public string CurrentFloor { get; private set; }

        public void TakeCommand(string command)
        {
            _commands.Enqueue(command);
        }

        public void MoveToNextFloor()
        {
        }
    }
}