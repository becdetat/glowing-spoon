using Elevator;
using NUnit.Framework;
using Shouldly;
using TestStack.BDDfy;

namespace ElevatorTests
{
    public class InitialDirectionOfTravelTests : TestBase
    {
        [Test]
        public void AscendingRequestResultsInUp()
        {
            Brains = new Brains(10);

            this
                .Given(x => x.PassengerSummonsElevatorOnFloor(15, DirectionOfTravel.Down))
                .Then(x => x.InitialDirectionOfTravelShouldBe(DirectionOfTravel.Up))
                .BDDfy();
        }

        [Test]
        public void DescendingRequestResultsInDown()
        {
            Brains = new Brains(10);

            this
                .Given(x => x.PassengerSummonsElevatorOnFloor(5, DirectionOfTravel.Down))
                .Then(x => x.InitialDirectionOfTravelShouldBe(DirectionOfTravel.Down))
                .BDDfy();
        }

        [Test]
        public void NoopRequestResultsInDown()
        {
            Brains = new Brains(10);

            this
                .Given(x => x.PassengerSummonsElevatorOnFloor(10, DirectionOfTravel.Down))
                .Then(x => x.InitialDirectionOfTravelShouldBe(DirectionOfTravel.Down))
                .BDDfy();
        }

        [Test]
        public void SecondRequestDoesNotChangeInitialDirection()
        {
            Brains = new Brains(10);

            this
                .Given(x => x.PassengerSummonsElevatorOnFloor(15, DirectionOfTravel.Down))
                .And(x => x.PassengerSummonsElevatorOnFloor(5, DirectionOfTravel.Down))
                .Then(x => x.InitialDirectionOfTravelShouldBe(DirectionOfTravel.Up))
                .BDDfy();
        }

        private void InitialDirectionOfTravelShouldBe(DirectionOfTravel directionOfTravel)
        {
            Brains.CurrentDirectionOfTravel.ShouldBe(directionOfTravel);
        }
    }
}