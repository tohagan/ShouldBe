namespace ShouldBe.DifferenceHighlighting
{
    /// <summary/>
    public class DifferenceHighlighter
    {
        /// <summary/>
        public const string HighlightCharacter = "*";

        /// <summary/>
        public string HighlightItem(string item)
        {
            return HighlightCharacter + item + HighlightCharacter;
        }
    }
}