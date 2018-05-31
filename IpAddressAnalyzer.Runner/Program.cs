using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Core;

namespace IpAddressAnalyzer.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            string path;
            if (args.Length == 0)
            {
                Console.WriteLine("Please type a path to data file");
                path = Console.ReadLine();
            }
            else
            {
                path = args[0];
            }
            var analyzer = new DataAnalyzer(path);
            var helper = new DataHelper();
            helper.Subscribe(analyzer);
            analyzer.Analyze();
            helper.SerializeData(analyzer);
            var printer = new ConsolePrinter(20, analyzer.ExecutionTime, analyzer.People, analyzer.CurrentLine);
            printer.Print();
        }




    }
}
