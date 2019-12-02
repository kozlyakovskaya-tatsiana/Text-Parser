using SecondTask.TextObjects;
using System.Collections.Generic;
using System.Linq;


namespace SecondTask.Tasks.Models
{
    class TaskFirst: ITaskFirst
    {
        public Text Text { get; private set; }

        public TaskFirst(Text text)
        {
            Text = text;
        }

        // Task 1.
        public IEnumerable<Sentence> SentencesInAscendingOrder => Text.Sentences.OrderBy(sent => sent.AmountOfWords);

        // Task 2.
        public IEnumerable<Word> GetWordsFromIssueSentencesByGivenLength(int length)
        {
            return Text.Sentences.
                  Where(sent => sent.IsInterrogative).
                  SelectMany(sent => sent.GetDistinctWordsByGivenLength(length)).
                  Distinct();
        }

        // Task 3. 
        public void SentencesWithoutWordsStartWithConsonant(int lengthWord)
        {
            Text.Sentences.ForEach(sent => sent.RemoveWordsByCondition(word => word.Length == lengthWord && word.IsStartWithConsonant));
        }

        // Task 4.
        public void ReplaceWordOfGivenLengthInSentenceByElements(int indexSentence, int lengthWord, params SentenceItem[] elementsToInsert)
        {
            Text.Sentences[indexSentence].ReplaceWordOfGivenLengthByElements(lengthWord, elementsToInsert);
        }

    }
}
