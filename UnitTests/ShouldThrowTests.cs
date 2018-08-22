using System;
using NUnit.Framework;
using ShouldBe;

namespace ShouldBe.UnitTests
{
    [TestFixture]
    public class ShouldThrowTests
    {
        [Test]
        public void ShouldThrowException_WithMatchingExceptionType_ShouldSucceed()
        {
            Should.ShouldThrowException<NotImplementedException>(
                () => { throw new NotImplementedException(); }
            );
        }

        [Test]
        public void ShouldThrowExceptionContaining_WithMatchingExceptionTypeAndMessage_ShouldSucceed()
        {
            Should.ShouldThrowExceptionContaining<NotImplementedException>(
                () => { throw new NotImplementedException("I'm not implemented yet"); }, 
                "I'm not implemented yet"
             );
        }

        [Test]
        public void ShouldThrowExceptionContaining_WhenItThrowsIncorrectExceptionMessage_ShouldFail()
        {
            Action shouldThrowAction = () =>
                Should.ShouldThrowExceptionContaining<RankException>(
                    () => { throw new RankException("actual message"); },
                    "expect message"
                );

            TestHelper.ShouldFailWithError(shouldThrowAction, "should throw exception containing \"expect message\" but was \"actual message\"");
        }

        [Test]
        public void ShouldThrowException_WhenItThrowsIncorrectExceptionType_ShouldFail()
        {
            Action shouldThrowAction =
                () => Should.ShouldThrowException<NotImplementedException>(
                    () => { throw new RankException(); }
                );

            TestHelper.ShouldFailWithError(shouldThrowAction, "should throw exception System.NotImplementedException but was System.RankException");
        }

        [Test]
        public void ShouldThrowException_WhenItDoesntThrow_ShouldFail()
        {
            Action shouldThrowAction = () => 
                Should.ShouldThrowException<NotImplementedException>(
                    () => { /* no exception thrown */ }
                );

            TestHelper.ShouldFailWithError(shouldThrowAction, "Should throw System.NotImplementedException but failed to throw an exception.");
        }
    }
}