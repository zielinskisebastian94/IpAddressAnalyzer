using System.Collections.Generic;

namespace IpAddressAnalyzer
{
    public interface IDataAnalyzer
    {
        bool Continue { get; set; }
        int CurrentLine { get; }
        double ExecutionTime { get; set; }
        int FileSize { get; }
        List<Person> People { get; set; }

        event DataAnalyzer.ProgressHandler Progress;

        void Analyze();
    }
}