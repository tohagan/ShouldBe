using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NUnit.Framework;

namespace ShouldBe.UnitTests
{
    /// <summary>
    /// Unit test the unit test's helper class method <see cref="TestHelper.ShouldFailWithError"/>.
    /// </summary>
    [TestFixture]
    public class ShouldFailWithErrorTests
    {
        [Test]
        public void ShouldFailWithError_ShouldFail_WhenActionSucceeds()
        {
            string expectedMessage = "blah de blah";
            try
            {
                TestHelper.ShouldFailWithError(() =>
                             "xyz".ShouldBe("xyz"),
                             expectedMessage);
            }
            catch (AssertionException ex)
            {
                string expected = string.Format("Should fail with error\n{0}\n    but it succeeded.", expectedMessage);
                Assert.That(ex.Message, Is.EqualTo(expected));
                return;
            }

            Assert.Fail("TestHelper.ShouldFailWithError() should fail when action succeeds.");
        }

        [Test]
        public void ShouldFailWithError_ShouldFail_WhenExpectedMessageDoesNotMatch()
        {
            try
            {
                TestHelper.ShouldFailWithError(() =>
                             "xyz".ShouldBe("pqr"),
                             @"WILL NOT MATCH");
            }
            catch (AssertionException)
            {
                return;
            }

            Assert.Fail("TestHelper.ShouldFailWithError() should fail when expected message does not match.");
        }


        [Test]
        public void ShouldFailWithError_ShouldSucceed_WhenExpectedMessageMatchesClosely()
        {
            TestHelper.ShouldFailWithError(() =>
                         "xyz".ShouldBe("pqr"),
                         @"   ""xyz""   should   be   ""pqr""   but   was ""xyz""  ");
        }

        [Test]
        public void ShouldFailWithError_ShouldSucceed_WhenExpectedMessageMatchesExactly()
        {
            TestHelper.ShouldFailWithError(() =>
                         "xyz".ShouldBe("pqr"),
                         @"""xyz"" should be ""pqr"" but was ""xyz""");
        }
    }
}