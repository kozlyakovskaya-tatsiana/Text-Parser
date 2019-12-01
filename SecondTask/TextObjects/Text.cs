using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace SecondTask.TextObjects
{
    public class Text : IEnumerable<Sentence>
    {
        public List<Sentence> Sentences { get; private set; }

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
        public void ReplaceWordInSentence(int indexSentence, int lengthWord, params SentenceItem[] elementsToInsert)
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
            return Sentences.
                SelectMany(sent => sent.Items.OfType<Word>()).
                Where(word => word.Equals(wordToCheck)).
                Count();
        }

        public List<string> StringsInText => ToString().Split(new string[] { "\n" }, StringSplitOptions.None).ToList();

        public List<int> NumberStringsOfWord(Word word)
        {
            return StringsInText.Where(str => Regex.IsMatch(str, @"\b" + word + @"\b", RegexOptions.IgnoreCase)).
                  Select(str => StringsInText.IndexOf(str)).ToList();
        }


        public void WriteConcordanceToFile(string pathToFile)
        {
            var groups = Sentences.SelectMany(sent => sent.DistinctWords).
                Distinct().Select(word => new Word(word.ToString().ToLower())).
                OrderBy(word => word).
                ToDictionary(word => word, NumberStringsOfWord).
                GroupBy(keyValue => keyValue.Key.ToString().First());

            using (StreamWriter writer = new StreamWriter(pathToFile))
            {
                foreach (var group in groups)
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
        }
    }
}

