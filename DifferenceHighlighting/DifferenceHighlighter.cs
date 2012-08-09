namespace ShouldBe.DifferenceHighlighting
{
    internal class DifferenceHighlighter
    {
        internal const string HighlightCharacter = "*";

        internal string HighlightItem(string item)
        {
            return HighlightCharacter + item + HighlightCharacter;
        }
    }
}