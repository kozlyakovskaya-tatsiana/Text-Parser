using SecondTask.TextObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SecondTask.Tasks.Models
{
    public class Concordance : IConcordance
    {
        public Text Text { get; private set; }

        public Concordance(Text text)
        {
            Text = text;
        }

        public List<int> NumberStringsWhereWordConsists(Word word)
        {
            return Text.GetRows().Where(row => row.Items.Contains(word)).Select(row => row.Number).ToList();
        }

        public string CreateConcordance()
        {
            var dictionoryGroups = Text.SentenceItems.OfType<Word>().
                Distinct().
                Select(word => new Word(word.ValueToLower)).
                OrderBy(word => word).
                ToDictionary(word => word, NumberStringsWhereWordConsists).
                GroupBy(keyValue => keyValue.Key.ToString().First());

            var result = new StringBuilder();
            foreach (var group in dictionoryGroups)
            {
                result.Append(Char.ToUpper(group.Key) + Environment.NewLine);
                foreach (var item in group)
                {
                    result.Append(item.Key.ToString().PadRight(25, '.') + Text.GetFrequencyWord(item.Key) + ": ");
                    item.Value.ForEach(index => result.Append(index + " "));
                    result.Append(Environment.NewLine);
                }
            }
            return result.ToString();
        }
        public void WriteConcordanceToFile(string pathToFile)
        {
           File.WriteAllText(pathToFile,CreateConcordance());
        }
    }
}
