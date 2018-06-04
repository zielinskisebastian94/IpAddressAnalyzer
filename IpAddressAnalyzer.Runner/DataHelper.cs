using System;
using System.Threading;

namespace IpAddressAnalyzer.Runner
{
    public class DataHelper : IDataHelper
    {
        public void Subscribe(IDataAnalyzer d)
        {
            d.Progress += D_Progress;
        }

        private void D_Progress(long bytesAnalyzed, EventArgs e, DataAnalyzer d)
        {
            System.Console.Write($"\rAnalyzed {d.PercentAnalyzed}% of all data");
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Q)
                {
                    d.Continue = false;
                }
            }
        }
        public DataSerializer SerializeData(IDataAnalyzer analyzer)
        {
            var jsonPAth = @"C:\analyzer\data.json";
            var xmlPAth = @"C:\analyzer\data.xml";
            return new DataSerializer(xmlPAth, jsonPAth);

        }
    }
}