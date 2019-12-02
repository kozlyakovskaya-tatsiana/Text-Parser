using SecondTask.Helpers;
using System.Linq;

namespace SecondTask.TextObjects
{
    public class SentenceSeparator : SentenceItem
    {
        public bool IsIssue => Value.Contains('?');

        public bool IsWhiteSpace => Value.Equals(" ");

        public bool IsPunctuation => Value.Any(HelpersForTextElements.Punctuations.Contains);

        public SentenceSeparator(string value) : base(value)
        {

        }
    }
}
