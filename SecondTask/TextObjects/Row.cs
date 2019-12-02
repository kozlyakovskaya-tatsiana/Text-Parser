using System;
using System.Collections.Generic;

namespace SecondTask.TextObjects
{
    public class Row
    {
        public int Number { get; }

        public List<SentenceItem> Items { get; }

        public Row(int number, IEnumerable<SentenceItem> items)
        {
            Number = number;
            Items = new List<SentenceItem>(items);
        }

        public override string ToString()
        {
            return String.Concat(Items);
        }
    }
}
