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

        private void D_Progress(int currentLine, EventArgs e, DataAnalyzer d)
        {
            System.Console.Write($"\rAnalyzed {(currentLine * 100 / d.FileSize)}% of all data");
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Q)
                {
                    d.Continue = false;
                }
            }
        }
        public void SerializeData(IDataAnalyzer analyzer)
        {
            var jsonPAth = @"C:\analyzer\data.json";
            var xmlPAth = @"C:\analyzer\data.xml";
            var serializer = new DataSerializer(xmlPAth, jsonPAth, analyzer.People);
            var t = new Thread(serializer.SerializeToJson);
            t.Start();
            var t2 = new Thread(serializer.SerializeToXml);
            t2.Start();
        }
    }
}