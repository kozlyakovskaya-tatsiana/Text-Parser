using SecondTask.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondTask.TextObjects
{
    public class SentenceSeparator : SentenceItem
    {
        public bool IsIssue => Value.Contains('?');

        public bool IsWhiteSpace => Value.Equals(" ");

        public bool IsPunctuation => Value.Any(HelpersForTextElements.Punctuations.Contains);

        public SentenceSeparator(string value):base(value)
        {

        }
    }
}
