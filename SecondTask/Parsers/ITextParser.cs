using SecondTask.TextObjects;
using System.Collections.Generic;

namespace SecondTask.Parsers
{
    public interface ITextParser
    {
        Text ParseTextFile(string pathToText);

        List<SentenceItem> ParseText(string text);
    }
}
