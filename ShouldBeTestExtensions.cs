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

        /// <summary>
        /// Asserts that the <paramref name="actual"/> and <paramref name="expected"/> values are equal. 
        /// Your .NET compiler and IDE checks that they are of also of the same type.
        /// Can be used assert that a value is expected to be null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="actual"></param>
        /// <param name="expected"></param>
        /// <returns></returns>
        public static T ShouldBe<T>(this T actual, T expected)
        {
            return actual.AssertAwesomely(Is.EqualTo(expected), actual, expected);
        }

        /// <summary>
        /// Asserts that the <paramref name="actual"/> and <paramref name="expected"/> values are not equal. 
        /// Your .NET compiler and IDE checks that they are of also of the same type.
        /// Can be used assert that a value is expected to be not null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="actual"></param>
        /// <param name="expected"></param>
        /// <returns></returns>
        public static T ShouldNotBe<T>(this T actual, T expected)
        {
            return actual.AssertAwesomely(Is.Not.EqualTo(expected), actual, expected);
        }

        /// <summary>
        /// Asserts that the <paramref name="actual"/> and <paramref name="expected"/> values are equal within a given tolerance. 
        /// Your .NET compiler and IDE checks that they are of also of the same type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="actual"></param>
        /// <param name="expected"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static T ShouldBe<T>(this T actual, T expected, T tolerance)
        {
            return actual.AssertAwesomely(Is.EqualTo(expected).Within(tolerance), actual, expected);
        }

        /// <summary>
        /// Asserts that the <paramref name="actual"/> and <paramref name="expected"/> values are not equal within a given tolerance. 
        /// Your .NET compiler and IDE checks that they are of also of the same type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="actual"></param>
        /// <param name="expected"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static T ShouldNotBe<T>(this T actual, T expected, T tolerance)
        {
            return actual.AssertAwesomely(Is.Not.EqualTo(expected).Within(tolerance), actual, expected);
        }
        #endregion

        #region TypeOf
        /// <summary>
        /// Asserts that the value is of a given type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="actual"></param>
        /// <returns></returns>
        public static T ShouldBeTypeOf<T>(this object actual)
        {
            return (T) ShouldBeTypeOf(actual, typeof(T));
        }

        /// <summary>
        /// Asserts that the value is of a given type <paramref name="type"/>.
        /// </summary>
        /// <param name="actual"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object ShouldBeTypeOf(this object actual, Type type)
        {
            return actual.AssertAwesomely(Is.TypeOf(type), actual, type);
        }

        /// <summary>
        /// Asserts that the value is not of a given type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="actual"></param>
        /// <returns></returns>
        public static object ShouldNotBeTypeOf<T>(this object actual)
        {
            return ShouldNotBeTypeOf(actual, typeof(T));
        }

        /// <summary>
        /// Asserts that the value is not of a given type <paramref name="type"/>.
        /// </summary>
        /// <param name="actual"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object ShouldNotBeTypeOf(this object actual, Type type)
        {
            return actual.AssertAwesomely(Is.Not.TypeOf(type), actual, type);
        }
        #endregion

        #region InstanceOf
        /// <summary>
        /// Asserts that the value is an instance of type <typeparamref name="T"/> or a derived type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="actual"></param>
        /// <returns></returns>
        public static T ShouldBeInstanceOf<T>(this object actual)
        {
            ShouldBeInstanceOf(actual, typeof(T));
            return (T)actual;
        }

        /// <summary>
        /// Asserts that the value is an instance of <paramref name="type"/> or a derived type.
        /// </summary>
        /// <param name="actual"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object ShouldBeInstanceOf(this object actual, Type type)
        {
            actual.AssertAwesomely(Is.InstanceOf(type), actual, type);
            return actual;
        }

        /// <summary>
        /// Asserts that the value is not an instance of type <typeparamref name="T"/> or a derived type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="actual"></param>
        /// <returns></returns>
        public static object ShouldNotBeInstanceOf<T>(this object actual)
        {
            ShouldNotBeInstanceOf(actual, typeof(T));
            return actual;
        }

        /// <summary>
        /// Asserts that the value is not an instance of <paramref name="type"/> or a derived type.
        /// </summary>
        /// <param name="actual"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object ShouldNotBeInstanceOf(this object actual, Type type)
        {
            actual.AssertAwesomely(Is.Not.InstanceOf(type), actual, type);
            return actual;
        }
        #endregion

        #region AssignableFrom
        /// <summary>
        /// Asserts that the value is assignable from the type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="actual"></param>
        /// <returns></returns>
        public static object ShouldBeAssignableFrom<T>(this object actual)
        {
            return ShouldBeAssignableFrom(actual, typeof(T));
        }

        /// <summary>
        /// Asserts that the value is assignable from the <paramref name="type"/>.
        /// </summary>
        /// <param name="actual"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object ShouldBeAssignableFrom(this object actual, Type type)
        {
            return actual.AssertAwesomely(Is.AssignableFrom(type), actual, type);
        }

        /// <summary>
        /// Asserts that the value is not assignable from the type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="actual"></param>
        /// <returns></returns>
        public static object ShouldNotBeAssignableFrom<T>(this object actual)
        {
            return ShouldNotBeAssignableFrom(actual, typeof(T));
        }

        /// <summary>
        /// Asserts that the value is not assignable from the <paramref name="type"/>.
        /// </summary>
        /// <param name="actual"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object ShouldNotBeAssignableFrom(this object actual, Type type)
        {
            return actual.AssertAwesomely(Is.Not.AssignableFrom(type), actual, type);
        }
        #endregion

        #region AssignableTo
        /// <summary>
        /// Asserts that the value is assignable to the type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="actual"></param>
        /// <returns></returns>
        public static T ShouldBeAssignableTo<T>(this object actual)
        {
            return (T)ShouldBeAssignableTo(actual, typeof(T));
        }

        /// <summary>
        /// Asserts that the value is assignable to the <paramref name="type"/>.
        /// </summary>
        /// <param name="actual"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object ShouldBeAssignableTo(this object actual, Type type)
        {
            return actual.AssertAwesomely(Is.AssignableTo(type), actual, type);
        }

        /// <summary>
        /// Asserts that the value is not assignable to the type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="actual"></param>
        /// <returns></returns>
        public static object ShouldNotBeAssignableTo<T>(this object actual)
        {
            return ShouldNotBeAssignableTo(actual, typeof(T));
        }

        /// <summary>
        /// Asserts that the value is not assignable to the <paramref name="type"/>.
        /// </summary>
        /// <param name="actual"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object ShouldNotBeAssignableTo(this object actual, Type type)
        {
            return actual.AssertAwesomely(Is.Not.AssignableTo(type), actual, type);
        }
        #endregion

        #region GreaterThan / AtLeast
        /// <summary>
        /// Asserts that the value is greater than <paramref name="expected"/>.
        /// Your .NET compiler and IDE checks that they are of also of the same type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="actual"></param>
        /// <param name="expected"></param>
        /// <returns></returns>
        public static T ShouldBeGreaterThan<T>(this T actual, T expected)
        {
            return actual.AssertAwesomely(Is.GreaterThan(expected), actual, expected);
        }

        /// <summary>
        /// Asserts that the value is greater than or equal to <paramref name="expected"/>.
        /// Your .NET compiler and IDE checks that they are of also of the same type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="actual"></param>
        /// <param name="expected"></param>
        /// <returns></returns>
        public static T ShouldBeGreaterThanOrEqualTo<T>(this T actual, T expected)
        {
            return actual.AssertAwesomely(Is.GreaterThanOrEqualTo(expected), actual, expected);
        }

        /// <summary>
        /// Asserts that the value at least <paramref name="expected"/>.
        /// Your .NET compiler and IDE checks that they are of also of the same type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="actual"></param>
        /// <param name="expected"></param>
        /// <returns></returns>
        public static T ShouldBeAtLeast<T>(this T actual, T expected)
        {
            return actual.AssertAwesomely(Is.GreaterThanOrEqualTo(expected), actual, expected);
        }

        #endregion

        #region LessThan / AtMost
        /// <summary>
        /// Asserts that the value at less than <paramref name="expected"/>.
        /// Your .NET compiler and IDE checks that they are of also of the same type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="actual"></param>
        /// <param name="expected"></param>
        /// <returns></returns>
        public static T ShouldBeLessThan<T>(this T actual, T expected)
        {
            return actual.AssertAwesomely(Is.LessThan(expected), actual, expected);
        }

        /// <summary>
        /// Asserts that the value at less than or equal to <paramref name="expected"/>.
        /// Your .NET compiler and IDE checks that they are of also of the same type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="actual"></param>
        /// <param name="expected"></param>
        /// <returns></returns>
        public static T ShouldBeLessThanOrEqualTo<T>(this T actual, T expected)
        {
            return actual.AssertAwesomely(Is.LessThanOrEqualTo(expected), actual, expected);
        }

        /// <summary>
        /// Asserts that the value is at most <paramref name="expected"/>.
        /// Your .NET compiler and IDE checks that they are of also of the same type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="actual"></param>
        /// <param name="expected"></param>
        /// <returns></returns>
        public static T ShouldBeAtMost<T>(this T actual, T expected)
        {
            return actual.AssertAwesomely(Is.LessThanOrEqualTo(expected), actual, expected);
        }

        #endregion

        #region SameAs
        /// <summary>
        /// Asserts that the value is the same object reference as <paramref name="expected"/>.
        /// Your .NET compiler and IDE checks that they are of also of the same type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="actual"></param>
        /// <param name="expected"></param>
        /// <returns></returns>
        public static T ShouldBeSameAs<T>(this T actual, T expected)
        {
            return actual.AssertAwesomely(Is.SameAs(expected), actual, expected);
        }

        /// <summary>
        /// Asserts that the value is not the same object reference as <paramref name="expected"/>.
        /// Your .NET compiler and IDE checks that they are of also of the same type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="actual"></param>
        /// <param name="expected"></param>
        /// <returns></returns>
        public static T ShouldNotBeSameAs<T>(this T actual, T expected)
        {
            return actual.AssertAwesomely(Is.Not.SameAs(expected), actual, expected);
        }
        #endregion
    }
}