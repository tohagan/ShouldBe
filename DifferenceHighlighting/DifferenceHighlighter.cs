namespace ShouldBe.DifferenceHighlighting
{
    public class DifferenceHighlighter
    {
        public const string HighlightCharacter = "*";

        public string HighlightItem(string item)
        {
            return HighlightCharacter + item + HighlightCharacter;
        }
    }
}