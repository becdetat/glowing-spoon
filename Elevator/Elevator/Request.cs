namespace Elevator
{
    public abstract class Request
    {
        protected Request(int floor)
        {
            Floor = floor;
        }

        public int Floor { get; }
    }
}