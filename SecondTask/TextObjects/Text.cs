using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SecondTask.TextObjects
{
    public class Text : IEnumerable<Sentence>
    {
        public List<Sentence> Sentences { get; private set; }

        public List<SentenceItem> SentenceItems => Sentences.SelectMany(sent => sent.Items).ToList();

        public Sentence this[int index] => Sentences[index];

        public int AmountOfSentences => Sentences.Count();

        public Text()
        {
            Sentences = new List<Sentence>();
        }

        public Text(IEnumerable<Sentence> sentences) : this()
        {
            Sentences.AddRange(sentences);
        }

        public int GetFrequencyWord(Word wordToCheck)
        {
            return SentenceItems.OfType<Word>().Where(word => word.Equals(wordToCheck)).Count();
        }

        public List<Row> GetRows()
        {
            var rows = new List<Row>();
            int stringNumber = 0;
            var rowItems = new List<SentenceItem>();
            foreach (var item in SentenceItems)
            {
                if (!(item.ToString().Equals("\n")))
                {
                    rowItems.Add(item);
                }
                else
                {
                    rows.Add(new Row(stringNumber, rowItems));
                    stringNumber++;
                    rowItems.Clear();
                }
            }
            return rows;
        }

        public override string ToString()
        {
            return String.Concat(Sentences);
        }

        public IEnumerator<Sentence> GetEnumerator()
        {
            return Sentences.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}
