using System.Collections.Generic;
using System.Linq;

// Extension class must be in this namespace for ShouldBe.UnitTests to compile.

namespace ShouldBe.DifferenceHighlighting
{
    internal static class DifferenceHighlighterExtensions
    {
        internal static readonly List<IHighlighter> Highlighters = new List<IHighlighter> {
            new EnumerableHighlighter()
        };

        /// <summary>
        /// Compares an actual value against an expected one and creates
        /// a string with the differences highlighted
        /// </summary>
        internal static string HighlightDifferencesBetween<T1,T2>(this T1 actualValue, T2 expectedValue)
        {
            var validHighlighter = GetHighlighterFor(expectedValue, actualValue);

            if (validHighlighter == null)
            {
                return actualValue.Inspect();
            }

            return validHighlighter.HighlightDifferences(expectedValue, actualValue);
        }

        internal static bool CanGenerateDifferencesBetween<T1, T2>(this T1 actual, T2 expected)
        {
            return GetHighlighterFor(expected, actual) != null;
        }

        internal static IHighlighter GetHighlighterFor<T1, T2>(T1 expected, T2 actual)
        {
            return Highlighters.Where(x => x.CanProcess(expected, actual)).FirstOrDefault();
        }
    }
}
