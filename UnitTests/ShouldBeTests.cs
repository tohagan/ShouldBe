using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NUnit.Framework;
using ShouldBe;

namespace ShouldBe.UnitTests
{
    [TestFixture]
    public class ShouldBeTests
    {
        [Test]
        public void ShouldBe_WhenTrue_ShouldSucceed()
        {
            true.ShouldBe(true);
        }

        [Test]
        public void ShouldBe_WhenFalse_ShouldThrow()
        {
            "this string".ShouldNotBe("some other string");
        }

        [Test]
        public void Should_WithNumbers_ShouldAllowTolerance()
        {
            Math.PI.ShouldNotBe(3.14);
            Math.PI.ShouldBe(3.14, 0.01);
            ((float)Math.PI).ShouldBe(3.14f, 0.01f);
        }

        [Test]
        public void ShouldBe_GreaterThan()
        {
            7.ShouldBeGreaterThan(1);
            1.ShouldBeLessThan(7);
            Should.ShouldThrowException<AssertionException>(() => 7.ShouldBeLessThan(0));
            Should.ShouldThrowException<AssertionException>(() => 0.ShouldBeGreaterThan(7));
        }

        [Test]
        public void ShouldBe_GreaterThanOrEqualTo()
        {
            7.ShouldBeGreaterThanOrEqualTo(1);
            1.ShouldBeGreaterThanOrEqualTo(1);
            Should.ShouldThrowException<AssertionException>(() => 0.ShouldBeGreaterThanOrEqualTo(1));
        }

        [Test]
        public void ShouldBeInstanceOf_ShouldSucceedForStrings()
        {
            "Sup yo".ShouldBeInstanceOf(typeof(string));
        }

        [Test]
        public void ShouldBeInstanceOfWithGenericParameter_ShouldSucceedForStrings()
        {
            "Sup yo".ShouldBeInstanceOf<string>();
        }

        [Test]
        public void ShouldNotBeTypeOf_ShouldSucceedForNonMatchingType()
        {
            "Sup yo".ShouldNotBeInstanceOf(typeof(int));
        }

        [Test]
        public void ShouldNotBeTypeOfWithGenericParameter_ShouldSucceedForNonMatchingTypes()
        {
            "Sup yo".ShouldNotBeInstanceOf<int>();
        }

        [Test]
        public void ShouldBe_ShouldSucceedWhenCalledOnANullEnumerableReference()
        {
            IEnumerable<int> something = null;
            TestHelper.ShouldFailWithError(
                () => something.ShouldBe(new[] { 1, 2, 3 }),
                "something should be [1, 2, 3] but was null");
        }

        class MyBase { }
        class MyThing : MyBase { }

        [Test]
        public void ShouldBeInstanceOf_ShouldSucceedForInheritance()
        {
            new MyThing().ShouldBeInstanceOf<MyBase>();
        }

        [Test]
        public void ShouldBe_Action()
        {
            Action a = () => 1.ShouldBe(2);

            TestHelper.ShouldFailWithError(a,
                "Action a = () => 1 should be 2 but was 1");
        }

        [Test]
        public void ShouldBe_Expression()
        {
            Expression<Action> lambda = () => 1.ShouldBe(2);

            TestHelper.ShouldFailWithError(lambda.Compile(),
            "The provided expression should be 2 but was 1");
        }
    }
}
