using NUnit.Framework;

namespace ShouldBe.UnitTests
{
    [TestFixture]
    [ShouldBeMethods]
    public class ShouldBeMessageTests
    {
        [Test]
        public void ComparingStringExpressions_ShouldGenerateExpressionMessage()
        {
            TestHelper.ShouldFailWithError(
                () => ("expe"+ "cted").ShouldBe("actual"),
                "(\"expe\"+ \"cted\") should be \"actual\" but was \"expected\""
            );
        }

        [Test]
        public void ComparingIntExpressions_ShouldGenerateMessage()
        {
            TestHelper.ShouldFailWithError(
                () => (1+1).ShouldBe(1),
                "(1+1) should be 1 but was 2"
            );
        }

        [Test]
        public void EnumerationsWithComparableObjects_ShouldShowDifferences() 
        {
            TestHelper.ShouldFailWithError(
                () => (new[] { 1, 2, 3 }).ShouldBe(new[] { 2, 2, 3 }),
                "(new[] { 1, 2, 3 }) should be [2, 2, 3] but was [1, 2, 3] difference [*1*, 2, 3]"
            );
        }

        [Test]
        public void UncomparableClasses_ShouldNotShowDifferences() 
        {
            TestHelper.ShouldFailWithError(
                () => new UncomparableClass("ted").ShouldBe(new UncomparableClass("bob")),
                "new UncomparableClass(\"ted\") should be bob but was ted"
            );
        }

        private class UncomparableClass {
            private readonly string _description;

            public UncomparableClass(string description) {
                _description = description;
            }

            public override string ToString() {
                return _description;
            }
        }
    }
}