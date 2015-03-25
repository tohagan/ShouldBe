using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;

namespace ShouldBe
{
    /// <summary>
    /// Extension methods to map types to string values.
    /// </summary>
    public static class StringHelpers
    {
        /// <summary>
        /// Convert a typed enumeration to a delimited string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string DelimitWith<T>(this IEnumerable<T> enumerable, string separator) where T : class
        {
            return string.Join(separator, enumerable.Select(i => Equals(i, default(T)) ? null : i.ToString()).ToArray());
        }

        /// <summary>
        /// converts an enumeration value to a EnumName.value string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Inspect(this Enum value)
        {
            return value.GetType().Name + "." + value;
        }

        /// <summary>
        /// Converts most object types to a string
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Inspect(this object value)
        {
            if (value == null)
                return "null";

            if (value is string)
                return "\"" + value + "\"";

            // bool.ToString() returns "True" or "False" which is not c# code.
            if (value is bool)
                return value.As<bool>() ? "true" : "false";

            if (value is IEnumerable)
            {
                object[] objects = Enumerable.Cast<object>(value.As<IEnumerable>()).ToArray();
                
                string result = "{" + objects.Select(o => o.Inspect()).DelimitWith(", ") + "}";

                if (result.Length > 30)
                {
                    // Split into multiple lines for improved readability
                    result = "\n{\n  " + objects.Select(o => o.Inspect()).DelimitWith(",\n  ") + "\n}\n";
                }

                return result;
            }

            if (value is Enum)
                return Inspect(value.As<Enum>());

            if (value is ConstantExpression)
            {
                return Inspect((object) value.As<ConstantExpression>().Value);
            }

            if (value is MemberExpression)
            {
                var member = value.As<MemberExpression>();
                var constant = member.Expression.As<ConstantExpression>();
                var info = member.Member.As<FieldInfo>();
                return info.GetValue(constant.Value).Inspect();
            }

            return value.ToString();
        }

        /// <summary>
        /// Replace camel cased identifiers with space delimited string.
        /// </summary>
        /// <param name="pascal"></param>
        /// <returns></returns>
        /// <remarks>Used convert assertion method names to failure descriptions</remarks>
        public static string CamelCasedToSpaced(this string pascal)
        {
            return Regex.Replace(pascal, @"([A-Z])", match => " " + match.Value.ToLower()).Trim();
        }

        /// <summary>
        /// Replaces single quotes with double quotes
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Quotify(this string input)
        {
            return input.Replace('\'', '"');
        }

        /// <summary>
        /// Remove whitespace chars.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string StripWhitespace(this string input)
        {
            return Regex.Replace(input, @"\s+", "");
        }
    }
}