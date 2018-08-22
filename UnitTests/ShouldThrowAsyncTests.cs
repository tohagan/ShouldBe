using System;
using System.Threading.Tasks;
using NUnit.Framework;
using ShouldBe;

namespace ShouldBe.UnitTests
{
#if NET_45_OR_GREATER
    /// <summary>
    /// Identical to <see cref="ShouldThrowTests"/> but tests the methods that support async Actions.
    /// </summary>
    [TestFixture]
    public class ShouldThrowAsyncTests
    {
        private async Task Throw(Exception ex)
        {
            await Task.Yield();
            throw ex;
        }

        [Test]
        public async Task ShouldThrowExceptionAsync_WithMatchingExceptionType_ShouldSucceed()
        {
            await Should.ShouldThrowExceptionAsync<NotImplementedException>(
                async () => { await Throw(new NotImplementedException()); }
            );
        }

        [Test]
        public async Task ShouldThrowExceptionAsync_WhenItThrowsIncorrectExceptionType_ShouldFail()
        {
            Func<Task> shouldThrowAction =
                () => Should.ShouldThrowExceptionAsync<NotImplementedException>(
                    async () => { await Throw(new RankException()); }
                );

            await TestHelper.ShouldFailWithErrorAsync(shouldThrowAction, "Should throw exception System.NotImplementedException but was System.RankException");
        }

        [Test]
        public async void ShouldThrowExceptionAsync_WhenItDoesntThrow_ShouldFail()
        {
            Func<Task> shouldThrowAction = () => 
                Should.ShouldThrowExceptionAsync<NotImplementedException>(
                    async () => { await Task.Run(() => { }); }
                );

            await TestHelper.ShouldFailWithErrorAsync(shouldThrowAction, "Should throw System.NotImplementedException but failed to throw an exception.");
        }

        [Test]
        public async void ShouldThrowExceptionContainingAsync_WhenItThrowsIncorrectExceptionMessage_ShouldFail()
        {
            Func<Task> shouldThrowAction = () =>
                Should.ShouldThrowExceptionContainingAsync<RankException>(
                    async () => { await Throw(new RankException("actual message")); },
                    "expect message"
                );

            await TestHelper.ShouldFailWithErrorAsync(shouldThrowAction, "Should throw System.RankException containing \"expect message\" but was \"actual message\"");
        }

        [Test]
        public async Task ShouldThrowExceptionContainingAsync_WithMatchingExceptionTypeAndMessage_ShouldSucceed()
        {
            await Should.ShouldThrowExceptionContainingAsync<NotImplementedException>(
                async () => { await Throw(new NotImplementedException("I'm not implemented yet")); },
                "I'm not implemented yet"
             );
        }
    }
#endif
}