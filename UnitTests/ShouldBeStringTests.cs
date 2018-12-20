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
        public void ShouldStartWith_Should_Fail_For_CH_In_Cheese()
        {
            Assert.Throws<AssertionException>(() => "Cheese".ShouldStartWith("CH"));
        }

        [Test]
        public void ShouldEndWith_Should_Succeed_For_ez_In_Cheez()
        {
            "Cheez".ShouldEndWith("ez");
        }

        [Test]
        public void ShouldEndWith_Should_Succeed_For_EZ_In_Cheez()
        {
            Assert.Throws<AssertionException>(() => "Cheez".ShouldEndWith("EZ"));
        }

        [Test]
        public void ShouldMatch_Should_Match_Based_On_RegEx_Pattern()
        {
            "Cheese".ShouldMatch(@"C.e{2}s[e]");
        }

        [Test]
        public void ShouldMatch_Should_Fail_Based_On_RegEx_Pattern()
        {
            Assert.Throws<AssertionException>(() => "Cheese".ShouldMatch(@"C.e{2}s[f]"));
        }

        [Test]
        public void ShouldNotMatch_Should_Not_Match_Based_On_RegEx_Pattern()
        {
            "Cheese".ShouldNotMatch(@"C.e{2}s[f]");
        }

        [Test]
        public void ShouldNotMatch_Should_Fail_Based_On_RegEx_Pattern()
        {
            Assert.Throws<AssertionException>(() => "Cheese".ShouldNotMatch(@"C.e{2}s[e]"));
        }

        [Test]
        public void ShouldContain_Should_Fail_For_hEE_In_Cheez()
        {
            Assert.Throws<AssertionException>(() => ("Che" + "ez").ShouldContain("hEE"));
        }

        [Test]
        public void ShouldContain_Should_Succeed_For_hee_In_Cheez()
        {
            ("Che" + "ez").ShouldContain("hee");
        }

        [Test]
        public void ShouldBeNullOrEmptyTests()
        {
            string x = null;
            "".ShouldBeNullOrEmpty();
            x.ShouldBeNullOrEmpty();
            Assert.Throws<AssertionException>(() => "X".ShouldBeNullOrEmpty());
        }

        [Test]
        public void ShouldNotBeNullOrEmptyTests()
        {
            "X".ShouldNotBeNullOrEmpty();
            string x = null;
            Assert.Throws<AssertionException>(() => "".ShouldNotBeNullOrEmpty());
            Assert.Throws<AssertionException>(() => x.ShouldNotBeNullOrEmpty());
        }

        [Test]
        public void ShouldBeNullOrWhiteSpaceTests()
        {
            string x = null;
            "".ShouldBeNullOrWhiteSpace();
            "  ".ShouldBeNullOrWhiteSpace();
            x.ShouldBeNullOrWhiteSpace();
            Assert.Throws<AssertionException>(() => "X".ShouldBeNullOrWhiteSpace());
        }

        [Test]
        public void ShouldNotNullOrWhiteSpaceTests()
        {
            "X".ShouldNotBeNullOrWhiteSpace();
            string x = null;
            Assert.Throws<AssertionException>(() => "".ShouldNotBeNullOrWhiteSpace());
            Assert.Throws<AssertionException>(() => "  ".ShouldNotBeNullOrWhiteSpace());
            Assert.Throws<AssertionException>(() => x.ShouldNotBeNullOrWhiteSpace());
        }
    }
}