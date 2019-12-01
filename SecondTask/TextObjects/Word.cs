using SecondTask.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondTask.TextObjects
{
    public class Word : SentenceItem,IComparable<Word>
    {
        public bool IsStartWithConsonant => HelpersForTextElements.Consonants.Contains(Char.ToLower(Value.First()));

        public bool IsNumber => Value.All(symbol => Char.IsDigit(symbol));

        public Word(string value):base(value)
        {

        }

        public int CompareTo(Word other)
        {
            return Value.CompareTo(other.Value);
        }
    }
}
