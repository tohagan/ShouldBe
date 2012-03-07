using System;
using NUnit.Framework;

namespace ShouldBe.UnitTests
{
    [TestFixture]
    public class ShouldBeEnumerableTests
    {
        [Test]
        public void TestShouldBeEmpty()
        {
            new int[] { }.ShouldBeEmpty();

            TestHelper.ShouldFailWithError(() =>
                new[] { 1, 2, 3 }.ShouldBeEmpty(),
                "new[] {1,2,3} should be empty but was {1, 2, 3}");

            int[] NullArray = null;
            TestHelper.ShouldFailWithError(() =>
                NullArray.ShouldBeEmpty(),
                "NullArray should not be null");
        }

        [Test]
        public void TestShouldNotBeEmpty()
        {
            new int[] { 1, 2, 3 }.ShouldNotBeEmpty();

            TestHelper.ShouldFailWithError(() =>
                new int[] { }.ShouldNotBeEmpty(),
                "new int[] { } should not be empty but was {}");

            int[] NullArray = null;
            TestHelper.ShouldFailWithError(() =>
                NullArray.ShouldNotBeEmpty(),
                "NullArray should not be null");
        }

        [Test]
        public void TestShouldContain()
        {
            new[] { 1, 2, 3 }.ShouldContain(2);

            TestHelper.ShouldFailWithError(() =>
                new[] { 1, 2, 3 }.ShouldContain(5),
                "new[] {1,2,3} should contain 5 but was {1, 2, 3}");

            int[] NullArray = null;
            TestHelper.ShouldFailWithError(() =>
                NullArray.ShouldContain(5),
                "NullArray should not be null");
        }

        [Test]
        public void TestShouldNotContain()
        {
            new[]{1,2,3}.ShouldNotContain(5);

            TestHelper.ShouldFailWithError(() =>
                new[] { 1, 2, 3 }.ShouldNotContain(3),
                "new[] {1,2,3} should not contain 3 but was {1, 2, 3}");

            int[] NullArray = null;
            TestHelper.ShouldFailWithError(() =>
                NullArray.ShouldNotContain(5),
                "NullArray should not be null");
        }

        public class Vampire
        {
            public int BitesTaken { get; set; }
        }

        [Test]
        public void ShouldContain_WithPredicate_UsingObjectsShouldDisplayMeaningfulMessage()
        {
            var vampires = new[]
                               {
                                   new Vampire {BitesTaken = 1},
                                   new Vampire {BitesTaken = 2},
                                   new Vampire {BitesTaken = 3},
                                   new Vampire {BitesTaken = 4},
                                   new Vampire {BitesTaken = 5},
                                   new Vampire {BitesTaken = 6},
                               };

            TestHelper.ShouldFailWithError(() =>
                vampires.ShouldContain(x => x.BitesTaken > 7),
                "vampires should contain an element satisfying the condition (x.BitesTaken > 7)");
        }

        [Test]
        public void TestShouldContain_WithPredicate()
        {
            new[]{1,2,3}.ShouldContain(x => x % 3 == 0);

            TestHelper.ShouldFailWithError(() =>
                new[] { 1, 2, 3 }.ShouldContain(x => x % 4 == 0),
                "new[]{1,2,3} should contain an element satisfying the condition ((x % 4) = 0)");

            int[] NullArray = null;
            TestHelper.ShouldFailWithError(() =>
                NullArray.ShouldContain(x => x % 3 == 0),
                "NullArray should not be null");
        }

        [Test]
        public void TestShouldNotContain_WithPredicate()
        {
            new[] { 1, 2, 3 }.ShouldNotContain(x => x % 4 == 0);

            TestHelper.ShouldFailWithError(() =>
                new[] { 1, 2, 3 }.ShouldNotContain(x => x % 3 == 0),
                "new[] {1,2,3} should not contain an element satisfying the condition ((x % 3) = 0)");

            int[] NullArray = null;
            TestHelper.ShouldFailWithError(() =>
                NullArray.ShouldNotContain(x => x % 3 == 0),
                "NullArray should not be null");
        }

        [Test]
        public void TestShouldContain_WithTolerance()
        {
            new[] { 1.0, 2.1, Math.PI, 4.321, 5.4321 }.ShouldNotContain(3.14);
            new[] { 1.0, 2.1, Math.PI, 4.321, 5.4321 }.ShouldContain(3.14, 0.01);
            new[] { 1.0f, 2.1f, (float)Math.PI, 4.321f, 5.4321f }.ShouldContain(3.14f, 0.02f);

            TestHelper.ShouldFailWithError(() =>
                new[] { 1.0, 2.1, Math.PI, 4.321, 5.4321 }.ShouldContain(2.14, 0.02),
                @"new[] { 1.0, 2.1, Math.PI, 4.321, 5.4321 } should contain 2.14 (+/- 0.02) but was {1, 2.1, 3.14159265358979, 4.321, 5.4321}");

            TestHelper.ShouldFailWithError(() =>
                new[] { 1.0f, 2.1f, (float)Math.PI, 4.321f, 5.4321f }.ShouldContain(2.14f, 0.002f),
                @"new[] { 1.0f, 2.1f, (float)Math.PI, 4.321f, 5.4321f } should contain 2.14 (+/- 0.002) but was {1, 2.1, 3.141593, 4.321, 5.4321}");

            float[] NullArray = null;
            TestHelper.ShouldFailWithError(() =>
                NullArray.ShouldContain(2.14f, 0.001f),
                "NullArray should not be null");
        }

        [Test]
        public void ShouldBeTheSet_WithMatchingElementsInSameOrder_ShouldSucceed()
        {
            var actual   = new[] { "ABC", "DEF", "GHI", "JKI", "LMN", "OPQ" };
            var expected = new[] { "ABC", "DEF", "GHI", "JKI", "LMN", "OPQ" };
            actual.ShouldBeTheSet(expected);

            string[] NullArray = null;
            TestHelper.ShouldFailWithError(() =>
                NullArray.ShouldBeTheSet(expected),
                "NullArray should not be null");
        }

        [Test]
        public void ShouldBeTheSet_WithMatchingElementsInMixedOrder_ShouldSucceed()
        {
            var actual = new[] { "ABC", "DEF", "GHI", "JKI", "LMN", "OPQ" };
            var expected = new[] { "LMN", "GHI", "OPQ", "ABC", "DEF", "JKI" };
            actual.ShouldBeTheSet(expected);
        }

        [Test]
        public void ShouldBeTheSet_WithAddedAndRemovedElements_ShouldFailWithMessage()
        {
            TestHelper.ShouldFailWithError(() => 
                new[] { 2, 7, 10, 9 }.ShouldBeTheSet(new[] { 2, 3, 10, 15, 9 }),
                @"new[] { 2, 7, 10, 9 } should be the set {2, 3, 9, 10, 15} but was {2, 7, 9, 10} difference {2, *7*, 9, 10, *} missing {3, 15} not expected {7}");
        }

        public void TestShouldHaveUniqueKeys()
        {
            // TODO: Unit test for ShouldHaveUniqueKeys()
        }

        public void TestShouldBeASubsetOf()
        {
            // TODO: Unit test for ShouldBeASubsetOf()
        }

        public void TestShouldBeASupersetOf()
        {
            // TODO: Unit test for ShouldBeASupersetOf()
        }
    }
}