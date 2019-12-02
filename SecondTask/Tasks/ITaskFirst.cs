using SecondTask.TextObjects;
using System.Collections.Generic;

namespace SecondTask.Tasks
{
    public interface ITaskFirst
    {
        Text Text { get; }

        // Task 1.
        IEnumerable<Sentence> SentencesInAscendingOrder { get; }

        // Task 2.
        IEnumerable<Word> GetWordsFromIssueSentencesByGivenLength(int length);

        // Task 3. 
        void SentencesWithoutWordsStartWithConsonant(int lengthWord);

        // Task 4.
        void ReplaceWordOfGivenLengthInSentenceByElements(int indexSentence, int lengthWord, params SentenceItem[] elementsToInsert);
    }
}
