using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using NUnit.Framework;

namespace ShouldBe
{
    /// <summary>
    /// Extension test methids for IEnumerable
    /// </summary>
    [DebuggerStepThrough]
    [ShouldBeMethods]
    public static class ShouldBeEnumerableTestExtensions
    {
        /// <summary>
        /// Expect IEnumerable to be empty
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="actual"></param>
        /// <returns></returns>
        public static IEnumerable<T> ShouldBeEmpty<T>(this IEnumerable<T> actual)
        {
            ShouldBeMessage.FailActualIfNull(actual);
            if (actual.Any())
            {
                ShouldBeMessage.FailActual(actual);
            }
            return actual;
        }

        /// <summary>
        /// Expect IEnumerable to not be empty
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="actual"></param>
        /// <returns></returns>
        public static IEnumerable<T> ShouldNotBeEmpty<T>(this IEnumerable<T> actual)
        {
            ShouldBeMessage.FailActualIfNull(actual);
            if (!actual.Any())
            {
                ShouldBeMessage.FailActual(actual);
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
            ShouldBeMessage.FailActualIfNull(actual);

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
            ShouldBeMessage.FailActualIfNull(actual);

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
            // TODO: Needs a unit test.
 
            ShouldBeMessage.FailActualIfNull(actual);
            IGrouping<TKey, T>[] duplicates = actual.GroupBy(keyFunc).Where(g => g.Count() > 1).ToArray();

            if (duplicates.Count() > 1)
            {
                string msg = "  Duplicate Keys:\n" + duplicates.Select(d => d.Key.Inspect() + " -> " + d.Inspect()).DelimitWith("\n");
                Assert.Fail(ShouldBeMessage.GetMessageActual(actual) + "\n" + msg);
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
            ShouldBeMessage.FailActualIfNull(actual);
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
            ShouldBeMessage.FailActualIfNull(actual);
            return subset.AssertAwesomely(Is.SubsetOf(actual), actual, subset);
        }

        /// <summary>
        /// Asserts that an element is contained in the IEnumerable{T} sequence.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="actual"></param>
        /// <param name="expected"></param>
        /// <returns></returns>
        public static IEnumerable<T> ShouldContain<T>(this IEnumerable<T> actual, T expected)
        {
            ShouldBeMessage.FailActualIfNull(actual);
            if (!actual.Contains(expected))
            {
                ShouldBeMessage.Fail(actual, expected);
            }
            return actual;
        }

        /// <summary>
        /// Asserts that an element is not contained in the IEnumerable{T} sequence.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="actual"></param>
        /// <param name="expected"></param>
        /// <returns></returns>
        public static IEnumerable<T> ShouldNotContain<T>(this IEnumerable<T> actual, T expected)
        {
            ShouldBeMessage.FailActualIfNull(actual);

            if (actual.Contains(expected))
            {
                ShouldBeMessage.Fail(actual, expected);
            }
            return actual;
        }

        /// <summary>
        /// Asserts that at least one element in the IEnumerable{T} sequence fulfills the <paramref name="elementPredicate"/> assertion.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="actual"></param>
        /// <param name="elementPredicate"></param>
        /// <returns></returns>
        public static IEnumerable<T> ShouldContain<T>(this IEnumerable<T> actual, Expression<Func<T, bool>> elementPredicate)
        {
            ShouldBeMessage.FailActualIfNull(actual);

            var condition = elementPredicate.Compile();
            if (!actual.Any(condition))
            {
                ShouldBeMessage.FailExpectingElement(elementPredicate.Body);
            }
            return actual;
        }

        /// <summary>
        /// Asserts that none of the elements in the IEnumerable{T} sequence fulfills the <paramref name="elementPredicate"/> assertion.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="actual"></param>
        /// <param name="elementPredicate"></param>
        /// <returns></returns>
        public static IEnumerable<T> ShouldNotContain<T>(this IEnumerable<T> actual, Expression<Func<T, bool>> elementPredicate)
        {
            ShouldBeMessage.FailActualIfNull(actual);

            var condition = elementPredicate.Compile();
            if (actual.Any(condition))
            {
                ShouldBeMessage.FailExpectingElement(elementPredicate.Body);
            }
            return actual;
        }

        /// <summary>
        /// Asserts that all of the elements in the IEnumerable{T} sequence fulfills the <paramref name="elementPredicate"/> assertion.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="actual"></param>
        /// <param name="elementPredicate"></param>
        /// <returns></returns>
        public static IEnumerable<T> ShouldBeAll<T>(this IEnumerable<T> actual, Expression<Func<T, bool>> elementPredicate)
        {
            ShouldBeMessage.FailActualIfNull(actual);

            var condition = elementPredicate.Compile();
            if (!actual.All(condition))
            {
                ShouldBeMessage.FailExpectingElement(elementPredicate.Body);
            }
            return actual;
        }

        #region Ascending/ Descending

        /// <summary>
        /// Asserts that the elements in the IEnumerable{T} sequence are in ascending order.
        /// </summary>
        /// <typeparam name="T">Elements must implement <see cref="IComparable"/>.</typeparam>
        /// <param name="actual"></param>
        /// <returns></returns>
        public static IEnumerable<T> ShouldBeAscending<T>(this IEnumerable<T> actual)
            where T : IComparable
        {
            ShouldBeMessage.FailActualIfNull(actual);

            T[] actualArray = actual.ToArray();
            T[] expectedArray = actual.OrderBy(o => o).ToArray();

            for (int i = 0; i < actualArray.Length && i < expectedArray.Length; i++)
            {
                actualArray[i].AssertAwesomely(Is.EqualTo(expectedArray[i]), actualArray, expectedArray);
            }

            return actual;
        }

        /// <summary>
        /// Asserts that the keys selected by the <paramref name="keySelector"/> function from elements in IEnumerable{TElem} in are in ascending order.
        /// </summary>
        /// <typeparam name="TElem"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="actual"></param>
        /// <param name="keySelector">Maps an element to an ordering key</param>
        /// <returns></returns>
        public static IEnumerable<TElem> ShouldBeAscending<TElem, TKey>(this IEnumerable<TElem> actual, Func<TElem, TKey> keySelector)
            where TKey : IComparable
        {
            keySelector.ShouldNotBe(null);
            ShouldBeMessage.FailActualIfNull(actual);

            TElem[] actualArray = actual.ToArray();
            TElem[] expectedArray = actual.OrderBy(keySelector).ToArray();

            for (int i = 0; i < actualArray.Length && i < expectedArray.Length; i++)
            {
                actualArray[i].AssertAwesomely(Is.EqualTo(expectedArray[i]), actualArray, expectedArray);
            }

            return actual;
        }

        /// <summary>
        /// Asserts that the elements in the IEnumerable{T} sequence are in descending order.
        /// </summary>
        /// <typeparam name="T">Elements must implement <see cref="IComparable"/>.</typeparam>
        /// <param name="actual"></param>
        /// <returns></returns>
        public static IEnumerable<T> ShouldBeDescending<T>(this IEnumerable<T> actual)
            where T : IComparable
        {
            ShouldBeMessage.FailActualIfNull(actual);

            T[] actualArray = actual.ToArray();
            T[] expectedArray = actual.OrderByDescending(o => o).ToArray();

            for (int i = 0; i < actualArray.Length && i < expectedArray.Length; i++)
            {
                actualArray[i].AssertAwesomely(Is.EqualTo(expectedArray[i]), actualArray, expectedArray);
            }

            return actual;
        }

        /// <summary>
        /// Asserts that the keys selected by the <paramref name="keySelector"/> function from elements in IEnumerable{TElem} in are in descending order.
        /// </summary>
        /// <typeparam name="TElem">Element type</typeparam>
        /// <typeparam name="TKey">Key type. Must implement <see cref="IComparable"/></typeparam>
        /// <param name="actual"></param>
        /// <param name="keySelector">Maps an element to an ordering key</param>
        /// <returns></returns>
        public static IEnumerable<TElem> ShouldBeDescending<TElem, TKey>(this IEnumerable<TElem> actual, Func<TElem, TKey> keySelector)
            where TKey : IComparable
        {
            keySelector.ShouldNotBe(null);
            ShouldBeMessage.FailActualIfNull(actual);

            TElem[] actualArray = actual.ToArray();
            TElem[] expectedArray = actual.OrderByDescending(keySelector).ToArray();

            for (int i = 0; i < actualArray.Length && i < expectedArray.Length; i++)
            {
                actualArray[i].AssertAwesomely(Is.EqualTo(expectedArray[i]), actualArray, expectedArray);
            }

            return actual;
        }
        #endregion

        #region ShouldContain with tolerance (numeric overloads)

        /// <summary>
        /// Asserts that an floating point element exists in the IEnumerable{T} sequence that is equal to <paramref name="expected"/> within a given <paramref name="tolerance"/> value.
        /// </summary>
        /// <param name="actual"></param>
        /// <param name="expected"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static IEnumerable<float> ShouldContain(this IEnumerable<float> actual, float expected, float tolerance)
        {
            ShouldBeMessage.FailActualIfNull(actual);

            if (!actual.Any(a => Math.Abs(expected - a) < tolerance))
            {
                ShouldBeMessage.Fail(actual, expected, tolerance);
            }
            return actual;
        }

        /// <summary>
        /// Asserts that an double precision floating point element exists in the IEnumerable{T} sequence that is equal to <paramref name="expected"/> within a given <paramref name="tolerance"/> value.
        /// </summary>
        /// <param name="actual"></param>
        /// <param name="expected"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static IEnumerable<double> ShouldContain(this IEnumerable<double> actual, double expected, double tolerance)
        {
            ShouldBeMessage.FailActualIfNull(actual);

            if (!actual.Any(a => Math.Abs(expected - a) < tolerance))
            {
                ShouldBeMessage.Fail(actual, expected, tolerance);
            }
            return actual;
        }

        //public static IEnumerable<sbyte> ShouldContain(this IEnumerable<sbyte> actual, sbyte expected, sbyte tolerance)
        //{
        //    ShouldBeMessage.FailActualIfNull(actual);

        //    if (actual.Any(a => Math.Abs(expected - a) < tolerance))
        //    {
        //        ShouldBeMessage.Fail(actual, expected, tolerance);
        //    }
        //    return actual;
        //}

        //public static IEnumerable<short> ShouldContain(this IEnumerable<short> actual, short expected, short tolerance)
        //{
        //    ShouldBeMessage.FailActualIfNull(actual);

        //    if (actual.Any(a => Math.Abs(expected - a) < tolerance))
        //    {
        //        ShouldBeMessage.Fail(actual, expected, tolerance);
        //    }
        //    return actual;
        //}

        //public static IEnumerable<int> ShouldContain(this IEnumerable<int> actual, int expected, int tolerance)
        //{
        //    ShouldBeMessage.FailActualIfNull(actual);

        //    if (actual.Any(a => Math.Abs(expected - a) < tolerance))
        //    {
        //        ShouldBeMessage.Fail(actual, expected, tolerance);
        //    }
        //    return actual;
        //}

        //public static IEnumerable<long> ShouldContain(this IEnumerable<long> actual, long expected, long tolerance)
        //{
        //    ShouldBeMessage.FailActualIfNull(actual);

        //    if (actual.Any(a => Math.Abs(expected - a) < tolerance))
        //    {
        //        ShouldBeMessage.Fail(actual, expected, tolerance);
        //    }
        //    return actual;
        //}

        /// <summary>
        /// Asserts that an <see cref="Decimal"/> value exists in the IEnumerable{T} sequence that is equal to <paramref name="expected"/> within a given <paramref name="tolerance"/> value.
        /// </summary>
        /// <param name="actual"></param>
        /// <param name="expected"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static IEnumerable<Decimal> ShouldContain(this IEnumerable<Decimal> actual, Decimal expected, Decimal tolerance)
        {
            ShouldBeMessage.FailActualIfNull(actual);

            if (actual.Any(a => Math.Abs(expected - a) < tolerance))
            {
                ShouldBeMessage.Fail(actual, expected, tolerance);
            }
            return actual;
        }
        #endregion

        #region ShouldNotContain with tolerance (numeric overloads)
        /// <summary>
        /// 
        /// </summary>
        /// <param name="actual"></param>
        /// <param name="expected"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static IEnumerable<float> ShouldNotContain(this IEnumerable<float> actual, float expected, float tolerance)
        {
            return actual.ShouldNotContain(a => Math.Abs(expected - a) < tolerance);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actual"></param>
        /// <param name="expected"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static IEnumerable<double> ShouldNotContain(this IEnumerable<double> actual, double expected, double tolerance)
        {
            return actual.ShouldNotContain(a => Math.Abs(expected - a) < tolerance);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actual"></param>
        /// <param name="expected"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static IEnumerable<sbyte> ShouldNotContain(this IEnumerable<sbyte> actual, sbyte expected, sbyte tolerance)
        {
            return actual.ShouldNotContain(a => Math.Abs(expected - a) < tolerance);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actual"></param>
        /// <param name="expected"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static IEnumerable<short> ShouldNotContain(this IEnumerable<short> actual, short expected, short tolerance)
        {
            return actual.ShouldNotContain(a => Math.Abs(expected - a) < tolerance);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actual"></param>
        /// <param name="expected"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static IEnumerable<int> ShouldNotContain(this IEnumerable<int> actual, int expected, int tolerance)
        {
            return actual.ShouldNotContain(a => Math.Abs(expected - a) < tolerance);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actual"></param>
        /// <param name="expected"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static IEnumerable<long> ShouldNotContain(this IEnumerable<long> actual, long expected, long tolerance)
        {
            return actual.ShouldNotContain(a => Math.Abs(expected - a) < tolerance);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actual"></param>
        /// <param name="expected"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static IEnumerable<Decimal> ShouldNotContain(this IEnumerable<Decimal> actual, Decimal expected, Decimal tolerance)
        {
            return actual.ShouldNotContain(a => Math.Abs(expected - a) < tolerance);
        }
        #endregion
    }

}