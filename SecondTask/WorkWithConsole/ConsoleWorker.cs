using SecondTask.Parsers;
using SecondTask.Tasks;
using SecondTask.TextObjects;
using System;
using System.Linq;

namespace SecondTask.WorkWithConsole
{
    public static class ConsoleWorker
    {
        private const string menu =
            @"Choose an item
             1.Print all sentences of the given text in ascending order of the number of words in each of them.
             2.In all question sentences of the text find and print without repetition of the word of the given length.
             3.Delete all words of a given length beginning with a consonant letter from the text.
             4.In some sentence of the text of the word of the given length to replace the specified substring
             5.Print text
             Press another key to finish work";

        public static void Start(ITaskFirst taskFirst, ITextParser textParser)
        {
            do
            {
                Console.Clear();
                Console.WriteLine(menu);
                switch (Console.ReadLine())
                {
                    case "1":
                        taskFirst.SentencesInAscendingOrder.ToList().ForEach(sent => Console.Write(sent));
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case "2":
                        Console.WriteLine("Input the length of the word");
                        if (int.TryParse(Console.ReadLine(), out int length))
                        {
                            if (length <= 0)
                                Console.WriteLine("The length must be bigger than 0");
                            else
                            {
                                Console.WriteLine("Words:");
                               taskFirst.GetWordsFromIssueSentencesByGivenLength(length).ToList().ForEach(word => Console.WriteLine(word));
                            }
                        }
                        else
                            Console.WriteLine("Incorrect input");
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case "3":
                        Console.WriteLine("Input the length of the word");
                        if (int.TryParse(Console.ReadLine(), out int lengthWord))
                        {
                            if (lengthWord <= 0)
                                Console.WriteLine("The length must be bigger than 0");
                            else
                            {
                                taskFirst.SentencesWithoutWordsStartWithConsonant(lengthWord);
                                Console.WriteLine(taskFirst.Text);
                            }
                        }
                        else Console.WriteLine("Incorrect input");
                        Console.ReadKey();
                        break;
                    case "4":
                        Console.WriteLine("Input the srtrig to insert");
                        string inserting = Console.ReadLine();
                        Console.WriteLine("Input the index of sentence (start with 0)");
                        if (int.TryParse(Console.ReadLine(), out int indexSentence))
                        {
                            if (indexSentence >= 0 && indexSentence < taskFirst.Text.AmountOfSentences)
                            {
                                Console.WriteLine("Input the length of the word");
                                if (int.TryParse(Console.ReadLine(), out int wordLength))
                                {
                                    if (wordLength <= 0)
                                        Console.WriteLine("The length must be bigger than 0");
                                    else
                                    {
                                        taskFirst.ReplaceWordOfGivenLengthInSentenceByElements(indexSentence, wordLength, textParser.ParseText(inserting).ToArray());
                                        Console.WriteLine(taskFirst.Text);
                                    }
                                }
                                else
                                    Console.WriteLine("Incorrect input");
                            }
                            else
                                Console.WriteLine("There is no sentence with this index in text");
                        }
                        else
                            Console.WriteLine("Incorrect input");
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case "5":
                        Console.WriteLine(taskFirst.Text);
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                        break;
                    default:
                        Console.WriteLine("You have finished work");
                        return;
                }
            } while (true);
        }
    }
}
