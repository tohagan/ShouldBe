using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using NUnit.Framework;

namespace ShouldBe
{
    [DebuggerStepThrough]
    [ShouldBeMethods]
    public static class ShouldBeEnumerableTestExtensions
    {
        public static IEnumerable<T> ShouldBeEmpty<T>(this IEnumerable<T> actual)
        {
            if (actual == null) ShouldBeMessage.Fail(actual);
            if (actual.Count() > 0)
            {
                ShouldBeMessage.Fail(actual);
            }
            return actual;
        }

        public static IEnumerable<T> ShouldNotBeEmpty<T>(this IEnumerable<T> actual)
        {
            if (actual == null) ShouldBeMessage.Fail(actual);
            if (actual.Count() == 0)
            {
                ShouldBeMessage.Fail(actual);
            }
            return actual;
        }

        ///<summary>
        /// Assert that the sequence of values in <paramref cref="IEnumerable{actual}"/>
        /// is equivalent to the <paramref cref="IEnumerable{expected}"/>.
        ///</summary>
        /// <remarks>
        /// Reports the first missing or unexpected element.
        /// </remarks>
        ///<param name="actual"></param>
        ///<param name="expected"></param>
        ///<typeparam name="T"></typeparam>
        public static IEnumerable<T> ShouldBeTheSequence<T>(this IEnumerable<T> actual, IEnumerable<T> expected)
        {
            T[] actualArray = actual.ToArray();
            T[] expectedArray = expected.ToArray();

            for (int i = 0; i < actualArray.Length && i < expectedArray.Length; i++)
            {
                actualArray[i].AssertAwesomely(Is.EqualTo(expectedArray[i]), actualArray, expectedArray);
            }

            actualArray.Length.AssertAwesomely(Is.EqualTo(expectedArray.Length), actualArray, expectedArray);

            //if (actualArray.Length < expectedArray.Length)
            //{
            //    Assert.Fail("Sequence is shorter than expected. Expected:\n{0}", expectedArray[actualArray.Length - 1].Inspect());
            //}
            //else if (actualArray.Length > expectedArray.Length)
            //{
            //    Assert.Fail("Sequence is longer than expected. Actual:\n{0}", actualArray[expectedArray.Length - 1].Inspect());
            //}
            return actual;
        }

        ///<summary>
        /// Assert that the set of values in <paramref cref="IEnumerable{actual}"/>
        /// is equivalent to the <paramref cref="IEnumerable{expected}"/> collection.
        ///</summary>
        /// <remarks>
        /// Reports missing and unexpected elements.
        /// </remarks>
        ///<param name="actual"></param>
        ///<param name="expected"></param>
        ///<typeparam name="T"></typeparam>
        public static IEnumerable<T> ShouldBeTheSet<T>(this IEnumerable<T> actual, IEnumerable<T> expected)
        {
            T[] actual1 = actual.ToArray();
            T[] expected1 = expected.ToArray();

            Array.Sort(actual1);
            Array.Sort(expected1);

            T[] onlyInActual = actual1.Except(expected1).ToArray();
            T[] onlyInExpected = expected1.Except(actual1).ToArray();

            string msg = "";
            if (onlyInExpected.Length > 0)
            {
                msg += string.Format("        missing\n    {0}\n", onlyInExpected.Inspect());
            }

            if (onlyInActual.Length > 0)
            {
                msg += string.Format("        not expected\n    {0}\n", onlyInActual.Inspect());
            }

            if (onlyInExpected.Length > 0 || onlyInActual.Length > 0)
            {
                Assert.Fail(ShouldBeMessage.GetMessage(actual1, expected1) + "\n" + msg);
            }
            return actual;
        }

        ///<summary>
        /// Assert that the set of values does not contain duplicate keys.
        /// TODO: Needs a unit test. 
        ///</summary>
        /// <remarks>
        /// Reports duplicate keys and their element values.
        /// </remarks>
        ///<param name="actual"></param>
        ///<param name="keyFunc">Function that maps an element to a key.</param>
        ///<typeparam name="T">Enumeration element type</typeparam>
        ///<typeparam name="TKey">Key type that implements <see cref="IComparable"/>.</typeparam>
        public static IEnumerable<T> ShouldHaveUniqueKeys<T, TKey>(this IEnumerable<T> actual, Func<T, TKey> keyFunc)
            where TKey : IComparable
        {
            IGrouping<TKey, T>[] duplicates = actual.GroupBy(keyFunc).Where(g => g.Count() > 1).ToArray();

            if (duplicates.Count() > 1)
            {
                string msg = "  Duplicate Keys:\n" + duplicates.Select(d => d.Key.Inspect() + " -> " + d.Inspect()).DelimitWith("\n");
                Assert.Fail(ShouldBeMessage.GetMessage(actual) + "\n" + msg);
            }

            return actual;
        }

        // TODO: Review http://www.nunit.org/index.php?p=collectionAssert&r=2.5.9
        // TODO: Implement: IEnumerable<T> ShouldHaveUniqueValues<T>(this IEnumerable<T> actual) - NUnit: AllItemsAreUnique
        // TODO: Implement: IEnumerable<T> ShouldHaveOrderedValues<T>(this IEnumerable<T> actual)  - NUnit: IsOrdered

        ///<summary>
        /// Assert that the collection <paramref cref="IEnumerable{actual}"/> 
        /// is a subset of the <paramref cref="IEnumerable{superset}"/> collection.
        ///</summary>
        ///<param name="actual"></param>
        ///<param name="superset"></param>
        ///<typeparam name="T"></typeparam>
        public static IEnumerable<T> ShouldBeASubsetOf<T>(this IEnumerable<T> actual, IEnumerable<T> superset)
        {
            return actual.AssertAwesomely(Is.SubsetOf(superset), actual, superset);
        }

        ///<summary>
        /// Assert that the collection <paramref cref="IEnumerable{actual}"/> 
        /// is a superset of the <paramref cref="IEnumerable{subset}"/> collection.
        ///</summary>
        ///<param name="actual"></param>
        ///<param name="subset"></param>
        ///<typeparam name="T"></typeparam>
        public static IEnumerable<T> ShouldContainTheSubset<T>(this IEnumerable<T> actual, IEnumerable<T> subset)
        {
            return subset.AssertAwesomely(Is.SubsetOf(actual), actual, subset);
        }

        public static IEnumerable<T> ShouldContain<T>(this IEnumerable<T> actual, T expected)
        {
            if (actual == null) ShouldBeMessage.Fail(actual, expected);

            if (!actual.Contains(expected))
            {
                ShouldBeMessage.Fail(actual, expected);
            }
            return actual;
        }

        public static IEnumerable<T> ShouldNotContain<T>(this IEnumerable<T> actual, T expected)
        {
            if (actual == null) ShouldBeMessage.Fail(actual, expected);
            if (actual.Contains(expected))
            {
                ShouldBeMessage.Fail(actual, expected);
            }
            return actual;
        }

        public static IEnumerable<T> ShouldContain<T>(this IEnumerable<T> actual, Expression<Func<T, bool>> elementPredicate)
        {
            if (actual == null) ShouldBeMessage.Fail(actual);

            var condition = elementPredicate.Compile();
            if (!actual.Any(condition))
            {
                ShouldBeMessage.Fail(elementPredicate.Body);
            }
            return actual;
        }

        public static IEnumerable<T> ShouldNotContain<T>(this IEnumerable<T> actual, Expression<Func<T, bool>> elementPredicate)
        {
            if (actual == null) ShouldBeMessage.Fail(actual);

            var condition = elementPredicate.Compile();
            if (actual.Any(condition))
            {
                ShouldBeMessage.Fail(elementPredicate.Body);
            }
            return actual;
        }

        #region ShouldContain with tolerance (numeric overloads)
        public static IEnumerable<float> ShouldContain(this IEnumerable<float> actual, float expected, float tolerance)
        {
            return actual.ShouldContain(a => Math.Abs(expected - a) < tolerance);
        }

        public static IEnumerable<double> ShouldContain(this IEnumerable<double> actual, double expected, double tolerance)
        {
            return actual.ShouldContain(a => Math.Abs(expected - a) < tolerance);
        }

        public static IEnumerable<sbyte> ShouldContain(this IEnumerable<sbyte> actual, sbyte expected, sbyte tolerance)
        {
            return actual.ShouldContain(a => Math.Abs(expected - a) < tolerance);
        }

        public static IEnumerable<short> ShouldContain(this IEnumerable<short> actual, short expected, short tolerance)
        {
            return actual.ShouldContain(a => Math.Abs(expected - a) < tolerance);
        }

        public static IEnumerable<int> ShouldContain(this IEnumerable<int> actual, int expected, int tolerance)
        {
            return actual.ShouldContain(a => Math.Abs(expected - a) < tolerance);
        }

        public static IEnumerable<long> ShouldContain(this IEnumerable<long> actual, long expected, long tolerance)
        {
            return actual.ShouldContain(a => Math.Abs(expected - a) < tolerance);
        }

        public static IEnumerable<Decimal> ShouldContain(this IEnumerable<Decimal> actual, Decimal expected, Decimal tolerance)
        {
            return actual.ShouldContain(a => Math.Abs(expected - a) < tolerance);
        }
        #endregion

        #region ShouldNotContain with tolerance (numeric overloads)
        public static IEnumerable<float> ShouldNotContain(this IEnumerable<float> actual, float expected, float tolerance)
        {
            return actual.ShouldNotContain(a => Math.Abs(expected - a) < tolerance);
        }

        public static IEnumerable<double> ShouldNotContain(this IEnumerable<double> actual, double expected, double tolerance)
        {
            return actual.ShouldNotContain(a => Math.Abs(expected - a) < tolerance);
        }

        public static IEnumerable<sbyte> ShouldNotContain(this IEnumerable<sbyte> actual, sbyte expected, sbyte tolerance)
        {
            return actual.ShouldNotContain(a => Math.Abs(expected - a) < tolerance);
        }

        public static IEnumerable<short> ShouldNotContain(this IEnumerable<short> actual, short expected, short tolerance)
        {
            return actual.ShouldNotContain(a => Math.Abs(expected - a) < tolerance);
        }

        public static IEnumerable<int> ShouldNotContain(this IEnumerable<int> actual, int expected, int tolerance)
        {
            return actual.ShouldNotContain(a => Math.Abs(expected - a) < tolerance);
        }

        public static IEnumerable<long> ShouldNotContain(this IEnumerable<long> actual, long expected, long tolerance)
        {
            return actual.ShouldNotContain(a => Math.Abs(expected - a) < tolerance);
        }

        public static IEnumerable<Decimal> ShouldNotContain(this IEnumerable<Decimal> actual, Decimal expected, Decimal tolerance)
        {
            return actual.ShouldNotContain(a => Math.Abs(expected - a) < tolerance);
        }
        #endregion
    }

}