using System.Configuration;
using SecondTask.Parser;
using SecondTask.TextObjects;
using SecondTask.WorkWithConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;

namespace SecondTask
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Text text = TextParser.ParseTextFile(ConfigurationManager.AppSettings["pathToSourceText"]);   
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
