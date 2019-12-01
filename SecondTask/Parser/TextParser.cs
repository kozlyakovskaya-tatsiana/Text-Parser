using SecondTask.TextObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SecondTask.Parser
{
    public static class TextParser
    {
        private static readonly Regex regSentence = new Regex(@"[A-Z0-9][^!\.\?]*(!|\.|\?)+(\s+)*", RegexOptions.Compiled);

        public static Text ParseTextFile(string pathToText)
        {
            /* string sourceText;
             using (StreamReader reader = new StreamReader(pathToText, System.Text.Encoding.Default))
             {
                 sourceText = reader.ReadToEnd();
             }*/
            string sourceText = File.ReadAllText(pathToText);
            string text = Regex.Replace(sourceText, @"(\t+)|([ ]+)", " ");
            text=Regex.Replace(text, @"(\t+)|([ ]+)", " ");
            MatchCollection matches = regSentence.Matches(text.ToString());

            var sentences = new List<Sentence>();
            foreach (Match match in matches)
            {
                sentences.Add(new Sentence (ParseText(match.Value)));
            }

            return new Text(sentences);
        }

        

        public static List<SentenceItem> ParseText(string text)
        {
            var items = new List<SentenceItem>();
            var buf = new StringBuilder();
            int pos = 0;
            bool inWord = false;
            int textSentenceLength = text.Length;
            char currElement;

            while (pos < textSentenceLength)
            {
                currElement = text[pos];
                if (Char.IsLetterOrDigit(currElement))
                {
                    if (!inWord && !String.IsNullOrEmpty(buf.ToString()))
                    {
                        items.Add(new SentenceSeparator(buf.ToString()));
                        buf.Clear();
                    }
                    buf.Append(currElement);
                    inWord = true;
                }
                else
                {
                    if (inWord)
                    {
                        items.Add(new Word(buf.ToString()));
                        buf.Clear();
                        inWord = false;
                        buf.Append(currElement);
                    }
                    else if (buf.ToString().Contains('.') || buf.ToString().Contains('!') || buf.ToString().Contains('?'))
                    {
                        if (Char.IsWhiteSpace(currElement))
                        {
                            items.Add(new SentenceSeparator(buf.ToString()));
                            buf.Clear();
                            items.Add(new SentenceSeparator(currElement.ToString()));
                        }
                        else buf.Append(currElement);
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(buf.ToString()))
                        {
                            items.Add(new SentenceSeparator(buf.ToString()));
                            buf.Clear();
                        }
                        buf.Append(currElement);
                    }
                }
                pos++;
            }
            if (Regex.IsMatch(buf.ToString(), @"\w+"))
                items.Add(new Word(buf.ToString()));
            else
                items.Add(new SentenceSeparator(buf.ToString()));

            return items;
        }

        
    }
}
