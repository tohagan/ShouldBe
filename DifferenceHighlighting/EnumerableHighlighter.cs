using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace ShouldBe.DifferenceHighlighting
{
    /// <summary>
    /// Highlights differences between IEnumerables of the same type,
    /// marking differences with asterisks
    /// </summary>
    internal class EnumerableHighlighter : IHighlighter
    {
        private const int MaxElementsToShow = 1000;
        private readonly DifferenceHighlighter _differenceHighlighter;

        public EnumerableHighlighter(DifferenceHighlighter differenceHighlighter)
        {
            _differenceHighlighter = differenceHighlighter;
        }

        public EnumerableHighlighter() : this(new DifferenceHighlighter())
        {
        }

        #region IHighlighter
        public bool CanProcess<T1, T2>(T1 expected, T2 actual) 
        {
            return  expected != null && actual != null
                    && expected is IEnumerable
                    && !(expected is string)
                    && expected.GetType() == actual.GetType();
        }

        public string HighlightDifferences<T1, T2>(T1 expected, T2 actual)
        {
            return HighlightDifferences((IEnumerable)expected, (IEnumerable)actual);
        }
        #endregion

        private string HighlightDifferences(IEnumerable expected, IEnumerable actual)
        {
            var expectedList = expected as object[] ?? expected.Cast<object>().ToArray();
            var actualList = actual as object[] ?? actual.Cast<object>().ToArray();
            if (CanProcess(expectedList, actualList))
            {
                var highestCount = actualList.Length > expectedList.Length ? actualList.Length : expectedList.Length;

                return HighlightDifferencesBetweenLists(actualList, expectedList, highestCount);
            }

            return actual.Inspect();
        }

        private string HighlightDifferencesBetweenLists(object[] actualList, object[] expectedList, int highestListCount)
        {
            var returnMessage = new StringBuilder("\n{\n  ");

            for (var listItem = 0; listItem < highestListCount; listItem++)
            {
                if (listItem >= MaxElementsToShow)
                {
                    returnMessage.Append("...");
                    break;
                }

                returnMessage.Append(GetComparedItemString(actualList, expectedList, listItem));

                if (listItem < highestListCount - 1)
                {
                    returnMessage.Append(",\n  ");
                }
            }

            return returnMessage.Append("\n}\n").ToString();
        }

        private string GetComparedItemString(object[] actualList, object[] expectedList, int itemPosition)
        {
            if (expectedList.Length <= itemPosition)
            {
                return _differenceHighlighter.HighlightItem(actualList[itemPosition].Inspect());
            }

            if (actualList.Length <= itemPosition)
            {
                return DifferenceHighlighter.HighlightCharacter;
            }

            var el1 = actualList[itemPosition];
            var el2 = expectedList[itemPosition];

            if (el1?.Equals(el2) ?? el2 == null)
            {
                return el1.Inspect();
            }

            return _differenceHighlighter.HighlightItem(el1.Inspect());
        }
   }
}