using System.Configuration;
using SecondTask.Parsers;
using SecondTask.TextObjects;
using SecondTask.WorkWithConsole;
using System;
using SecondTask.Tasks.Models;

namespace SecondTask
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var textParser = new TextParser();
                var text = textParser.ParseTextFile(ConfigurationManager.AppSettings["pathToSourceText"]);
                var taskFirst = new TaskFirst(text);
                
                ConsoleWorker.Start(taskFirst, textParser);
                new Concordance(text).WriteConcordanceToFile(ConfigurationManager.AppSettings["pathToConcordance"]);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("Program has finished");
                Console.ReadKey();
            }


        }
    }
}
