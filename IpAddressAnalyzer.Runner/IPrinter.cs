using System.Collections;
using System.Collections.Generic;

namespace IpAddressAnalyzer.Runner
{
    interface IPrinter
    {
        int PageSize { get; }
        int AnalyzedRows { get; }
        double ExecutionTime { get; }
        ICollection<Person> Collection { get; }
        void Print();

    }
}