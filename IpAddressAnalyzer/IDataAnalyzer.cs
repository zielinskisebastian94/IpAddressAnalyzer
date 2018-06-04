using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;

namespace IpAddressAnalyzer
{
    public interface IDataAnalyzer
    {
        bool Continue { get; set; }
        int CurrentLine { get; }
        double ExecutionTime { get; set; }
        long FileSize { get; }
        List<Person> People { get; set; }

        event DataAnalyzer.ProgressHandler Progress;

        void Analyze();
    }
}