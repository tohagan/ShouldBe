﻿using NUnit.Framework;
using ShouldBe;

namespace ShouldBe.UnitTests
{
    [TestFixture]
    public class ShouldBeRhinoTests
    {
        public enum Direction
        {
            North, East, South, West
        }

        public interface IZombie
        {
            void EatHuman(Direction direction);
        }

        [Test]
        public void OnShouldHaveBeenCalled_WhenCalled_ShouldPass()
        {
            var steve = MockMe.Mock<IZombie>();

            steve.EatHuman(Direction.North);

            steve.ShouldHaveBeenCalled(s => s.EatHuman(Direction.North));
        }

        [Test]
        public void OnShouldHaveBeenCalled_WhenNotCalled_ShouldFailWithOtherCallsShown()
        {
            var steve = MockMe.Mock<IZombie>();

            steve.EatHuman(Direction.East);
            steve.EatHuman(Direction.South);
            steve.EatHuman(Direction.West);

            TestHelper.ShouldFailWithError(() =>
                steve.ShouldHaveBeenCalled(s => s.EatHuman(Direction.North)),
                @"*Expecting*
                      EatHuman(Direction.North)
                  *Recorded*
                   0: EatHuman(Direction.East)
                   1: EatHuman(Direction.South)
                   2: EatHuman(Direction.West)"); 
        }

        [Test]
        public void OnShouldHaveBeenCalledWithVaraible_WhenNotCalled_ShouldFailWithOtherCallsShown()
        {
            var steve = MockMe.Mock<IZombie>();

            steve.EatHuman(Direction.East);
            steve.EatHuman(Direction.South);
            steve.EatHuman(Direction.West);

            var expectedDirection = Direction.North;

            TestHelper.ShouldFailWithError(() =>
                steve.ShouldHaveBeenCalled(s => s.EatHuman(expectedDirection)),
                @"*Expecting*
                      EatHuman(Direction.North)
                  *Recorded*
                   0: EatHuman(Direction.East)
                   1: EatHuman(Direction.South)
                   2: EatHuman(Direction.West)"); 
        }
    }
}