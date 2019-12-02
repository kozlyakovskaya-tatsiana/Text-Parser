using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SecondTask.TextObjects
{
    public class Sentence : IEnumerable<SentenceItem>
    {
        public List<SentenceItem> Items { get; private set; }

        public bool IsInterrogative
        {
            get
            {
                return (Items.OfType<SentenceSeparator>().LastOrDefault(sent => sent.IsPunctuation)?.IsIssue).GetValueOrDefault();
            }
        }

        public int AmountOfWords => Items.OfType<Word>().Count();

        public IEnumerable<Word> DistinctWords => Items.OfType<Word>().Distinct();

        public Sentence()
        {
            Items = new List<SentenceItem>();
        }

        public Sentence(IEnumerable<SentenceItem> items) : this()
        {
            Items.AddRange(items);
        }

        public void RemoveWordsByCondition(Func<Word, bool> condition)
        {
            Items = Items.Where(item => !(item is Word && condition((Word)item))).ToList();
        }

        public void ReplaceWordOfGivenLengthByElements(int lengthWord, params SentenceItem[] elementsToInsert)
        {
            Items = Items.SelectMany(x => (x is Word && x.Length == lengthWord) ? elementsToInsert : new SentenceItem[] { x }).ToList();
        }

        public IEnumerable<Word> GetDistinctWordsByGivenLength(int lengthWord)
        {
            return DistinctWords.Where(word => word.Length == lengthWord);
        }

        public override string ToString()
        {
            return String.Concat(Items);
        }

        public IEnumerator<SentenceItem> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
