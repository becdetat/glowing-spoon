namespace Elevator
{
    public class SummonRequest : Request
    {
        public SummonRequest(int floor, DirectionOfTravel directionOfTravel)
            : base(floor)
        {
            DirectionOfTravel = directionOfTravel;
        }

        public DirectionOfTravel DirectionOfTravel { get; }
    }
}