using NUnit.Framework;

namespace ShouldBe.UnitTests
{
    [TestFixture]
    public class ShouldBeStringTests
    {
        [Test]
        public void ShouldBeCloseTo_WithNullAsExpected()
        {
            TestHelper.ShouldFailWithError(
               () => 
                   "test".ShouldBeCloseTo(null),
               @"""test"" should be close to null but was ""test""");
        }

        [Test]
        public void ShouldBeCloseTo_IsPrettyLoose()
        {
            "Fun   with space   and \"quotes\""
                .ShouldBeCloseTo("Fun with space and 'quotes'");
        }

        [Test]
        public void ShouldBeCloseTo_ShowsYouWhereTheStringsDiffer()
        {
            const string testMessage = "muhst eat braiiinnzzzz";
            TestHelper.ShouldFailWithError(() =>
                testMessage.ShouldBeCloseTo("must eat brains"),
                @"testMessage should be close to ""must eat brains"" but was ""muhst eat braiiinnzzzz""");
        }

        [Test]
        public void ShouldStartWith_Should_Succeed_For_Ch_In_Cheese()
        {
            "Cheese".ShouldStartWith("Ch");
        }

        // Should not be case insensitive

        [Test]
        [ExpectedException(typeof(AssertionException))]
        public void ShouldStartWith_Should_Fail_For_CH_In_Cheese()
        {
            "Cheese".ShouldStartWith("CH");
        }

        [Test]
        public void ShouldEndWith_Should_Succeed_For_ez_In_Cheez()
        {
            "Cheez".ShouldEndWith("ez");
        }

        [Test]
        [ExpectedException(typeof(AssertionException))]
        public void ShouldEndWith_Should_Succeed_For_EZ_In_Cheez()
        {
            "Cheez".ShouldEndWith("EZ");
        }

        [Test]
        public void ShouldMatch_Should_Match_Based_On_RegEx_Pattern()
        {
            "Cheese".ShouldMatch(@"C.e{2}s[e]");
        }

        [Test]
        [ExpectedException(typeof(AssertionException))]
        public void ShouldMatch_Should_Fail_Based_On_RegEx_Pattern()
        {
            "Cheese".ShouldMatch(@"C.e{2}s[f]");
        }

        [Test]
        public void ShouldNotMatch_Should_Not_Match_Based_On_RegEx_Pattern()
        {
            "Cheese".ShouldNotMatch(@"C.e{2}s[f]");
        }

        [Test]
        [ExpectedException(typeof(AssertionException))]
        public void ShouldNotMatch_Should_Fail_Based_On_RegEx_Pattern()
        {
            "Cheese".ShouldNotMatch(@"C.e{2}s[e]");
        }

        [Test]
        [ExpectedException(typeof(AssertionException))]
        public void ShouldContain_Should_Fail_For_hEE_In_Cheez()
        {
            ("Che" + "ez").ShouldContain("hEE");
        }

        [Test]
        public void ShouldContain_Should_Succeed_For_hee_In_Cheez()
        {
            ("Che" + "ez").ShouldContain("hee");
        }
    }
}