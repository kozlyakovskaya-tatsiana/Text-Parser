using System.Configuration;
using SecondTask.Parser;
using SecondTask.TextObjects;
using SecondTask.WorkWithConsole;
using System;

namespace SecondTask
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Text text = TextParser.ParseTextFile(ConfigurationManager.AppSettings["pathToSourceText"]);
                text.GetRows().ForEach(row => Console.WriteLine(row));
                ConsoleWorker.Start(text);
                text.WriteConcordanceToFile(ConfigurationManager.AppSettings["pathToConcordance"]);
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
