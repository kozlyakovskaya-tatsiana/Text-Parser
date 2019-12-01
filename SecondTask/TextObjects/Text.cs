using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

namespace SecondTask.TextObjects
{
    public class Text : IEnumerable<Sentence>
    {
        public List<Sentence> Sentences { get; private set; }

        public List<SentenceItem> SentenceItems => Sentences.SelectMany(sent => sent.Items).ToList();

        public Sentence this[int index] => Sentences[index];

        public int SentencesAmount => Sentences.Count();

        // Task 1.
        public IEnumerable<Sentence> SentencesInAscendingOrder => Sentences.OrderBy(sent => sent.WordsAmount);

        public Text()
        {
            Sentences = new List<Sentence>();
        }

        public Text(IEnumerable<Sentence> sentences) : this()
        {
            Sentences.AddRange(sentences);
        }

        // Task 2.
        public IEnumerable<Word> GetWordsFromIssueSents(int length)
        {
            return Sentences.
                  Where(sent => sent.IsInterrogative).
                  SelectMany(sent => sent.GetDistinctWordsByLength(length)).
                  Distinct();
        }
        // Task 3. 
        public void SentencesWithoutWordsStartConsonants(int lengthWord)
        {
            Sentences.ForEach(sent => sent.RemoveWordsBy(word => word.Length == lengthWord && word.IsStartWithConsonant));
        }

        // Task 4.
        public void ReplaceWordInSentenceByElements(int indexSentence, int lengthWord, params SentenceItem[] elementsToInsert)
        {
            Sentences[indexSentence].ReplaceWordOfLengthBy(lengthWord, elementsToInsert);
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

        // Concordance.
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
     

        public List<int> NumberStringsWhereWordConsists(Word word)
        {
            return GetRows().Where( row=> row.Items.Contains(word)).Select(row=>row.Number).ToList();
        }

        public void WriteConcordanceToFile(string pathToFile)
        {
            var dictionoryGroups = SentenceItems.OfType<Word>().
                Distinct().Select(word => new Word(word.ToString().ToLower())).
                OrderBy(word => word).
                ToDictionary(word => word, NumberStringsWhereWordConsists).
                GroupBy(keyValue => keyValue.Key.ToString().First());

            var result = new StringBuilder();
            foreach (var group in dictionoryGroups)
            {
                result.Append(Char.ToUpper(group.Key)+Environment.NewLine);
                foreach (var item in group)
                {
                    result.Append(item.Key + ".............." + GetFrequencyWord(item.Key) + ": ");
                    item.Value.ForEach(index => result.Append(index + " "));
                    result.Append(Environment.NewLine);
                }
            }
            File.WriteAllText(pathToFile, result.ToString());
        }
    }
}


// public List<string> RowsInText => ToString().Split(new string[] { "\n" }, StringSplitOptions.None).ToList();

/*  public List<int> NumberStringsWhereWordConsists(Word word)
  {
      return RowsInText.Where(str => Regex.IsMatch(str, @"\b" + word + @"\b", RegexOptions.IgnoreCase)).
            Select(str => RowsInText.IndexOf(str)).ToList();
  }*/

/*using (StreamWriter writer = new StreamWriter(pathToFile))
        {
            foreach (var group in dictionoryGroups)
            {
                writer.WriteLine(Char.ToUpper(group.Key));
                foreach (var item in group)
                {
                    writer.Write(item.Key + ".............." + GetFrequencyWord(item.Key) + ": ");
                    item.Value.ForEach(index => writer.Write($"{index} "));
                    writer.WriteLine();
                }
            }
        }
        */