using System;
using System.Diagnostics;
using NUnit.Framework;

namespace ShouldBe
{
    /// <summary>
    /// Wrapper extentions methods for <see cref="NUnit.Framework.Is"/> constraints.
    /// </summary>
    [DebuggerStepThrough]
    [ShouldBeMethods]
    public static class ShouldBeTestExtensions
    {
        #region ShouldBe
        public static T ShouldBe<T>(this T actual, T expected)
        {
            return actual.AssertAwesomely(Is.EqualTo(expected), actual, expected);
        }

        public static T ShouldNotBe<T>(this T actual, T expected)
        {
            return actual.AssertAwesomely(Is.Not.EqualTo(expected), actual, expected);
        }

        public static T ShouldBe<T>(this T actual, T expected, T tolerance)
        {
            return actual.AssertAwesomely(Is.EqualTo(expected).Within(tolerance), actual, expected);
        }
        #endregion

        #region TypeOf
        public static T ShouldBeTypeOf<T>(this object actual)
        {
            return (T) ShouldBeTypeOf(actual, typeof(T));
        }

        public static object ShouldBeTypeOf(this object actual, Type expected)
        {
            return actual.AssertAwesomely(Is.TypeOf(expected), actual, expected);
        }

        public static object ShouldNotBeTypeOf<T>(this object actual)
        {
            return ShouldNotBeTypeOf(actual, typeof(T));
        }

        public static object ShouldNotBeTypeOf(this object actual, Type expected)
        {
            return actual.AssertAwesomely(Is.Not.TypeOf(expected), actual, expected);
        }
        #endregion

        #region InstanceOf
        public static T ShouldBeInstanceOf<T>(this object actual)
        {
            ShouldBeInstanceOf(actual, typeof(T));
            return (T)actual;
        }

        public static object ShouldBeInstanceOf(this object actual, Type expected)
        {
            actual.AssertAwesomely(Is.InstanceOf(expected), actual, expected);
            return actual;
        }

        public static object ShouldNotBeInstanceOf<T>(this object actual)
        {
            ShouldNotBeInstanceOf(actual, typeof(T));
            return actual;
        }

        public static object ShouldNotBeInstanceOf(this object actual, Type expected)
        {
            actual.AssertAwesomely(Is.Not.InstanceOf(expected), actual, expected);
            return actual;
        }
        #endregion

        #region AssignableFrom
        public static object ShouldBeAssignableFrom<T>(this object actual)
        {
            return ShouldBeAssignableFrom(actual, typeof(T));
        }

        public static object ShouldBeAssignableFrom(this object actual, Type expected)
        {
            return actual.AssertAwesomely(Is.AssignableFrom(expected), actual, expected);
        }

        public static object ShouldNotBeAssignableFrom<T>(this object actual)
        {
            return ShouldNotBeAssignableFrom(actual, typeof(T));
        }

        public static object ShouldNotBeAssignableFrom(this object actual, Type expected)
        {
            return actual.AssertAwesomely(Is.Not.AssignableFrom(expected), actual, expected);
        }
        #endregion

        #region AssignableTo
        public static T ShouldBeAssignableTo<T>(this object actual)
        {
            return (T)ShouldBeAssignableTo(actual, typeof(T));
        }

        public static object ShouldBeAssignableTo(this object actual, Type expected)
        {
            return actual.AssertAwesomely(Is.AssignableTo(expected), actual, expected);
        }

        public static object ShouldNotBeAssignableTo<T>(this object actual)
        {
            return ShouldNotBeAssignableTo(actual, typeof(T));
        }

        public static object ShouldNotBeAssignableTo(this object actual, Type expected)
        {
            return actual.AssertAwesomely(Is.Not.AssignableTo(expected), actual, expected);
        }
        #endregion

        #region GreaterThan / AtLeast
        public static T ShouldBeGreaterThan<T>(this T actual, T expected)
        {
            return actual.AssertAwesomely(Is.GreaterThan(expected), actual, expected);
        }

        public static T ShouldBeGreaterThanOrEqualTo<T>(this T actual, T expected)
        {
            return actual.AssertAwesomely(Is.GreaterThanOrEqualTo(expected), actual, expected);
        }

        public static T ShouldBeAtLeast<T>(this T actual, T expected)
        {
            return actual.AssertAwesomely(Is.GreaterThanOrEqualTo(expected), actual, expected);
        }

        #endregion

        #region LessThan / AtMost
        public static T ShouldBeLessThan<T>(this T actual, T expected)
        {
            return actual.AssertAwesomely(Is.LessThan(expected), actual, expected);
        }

        public static T ShouldBeLessThanOrEqualTo<T>(this T actual, T expected)
        {
            return actual.AssertAwesomely(Is.LessThanOrEqualTo(expected), actual, expected);
        }

        public static T ShouldBeAtMost<T>(this T actual, T expected)
        {
            return actual.AssertAwesomely(Is.LessThanOrEqualTo(expected), actual, expected);
        }

        #endregion

        #region SameAs
        public static T ShouldBeSameAs<T>(this T actual, T expected)
        {
            return actual.AssertAwesomely(Is.SameAs(expected), actual, expected);
        }

        public static T ShouldNotBeSameAs<T>(this T actual, T expected)
        {
            return actual.AssertAwesomely(Is.Not.SameAs(expected), actual, expected);
        }
        #endregion
    }
}