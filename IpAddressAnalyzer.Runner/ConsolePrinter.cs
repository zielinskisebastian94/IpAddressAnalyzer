using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpAddressAnalyzer.Runner
{
    class ConsolePrinter : IPrinter
    {
        public ConsolePrinter(int pageSize, double executionTime, ICollection<Person> collection, int rowsAnalyzed)
        {
            PageSize = pageSize;
            ExecutionTime = executionTime;
            Collection = collection;
            _collectionSize = Collection.Count;
            AnalyzedRows = rowsAnalyzed;
        }

        public int PageSize { get; }
        public int AnalyzedRows { get; }
        public double ExecutionTime { get; }
        public ICollection<Person> Collection { get; }
        private int _currentPage = 0;
        private readonly int _collectionSize;


        public void Print()
        {
            PrintFirstPage();
            var qPressed = true;
            while (qPressed)
            {
                var key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.N:
                        PrintNextPage();
                        break;
                    case ConsoleKey.P:
                        PrintPreviousPage();
                        break;
                    case ConsoleKey.U:
                        PrintFirstPage();
                        break;
                    case ConsoleKey.B:
                        PrintLastPage();
                            break;
                    case ConsoleKey.Q:
                        qPressed = false;
                            break;
                }
            } 
        }

        private void PrintNextPage()
        {
            if (_currentPage < (int) Math.Floor((decimal) _collectionSize / PageSize))
            {
                _currentPage = _collectionSize > _currentPage * PageSize ? ++_currentPage : _currentPage;
                Console.Write(PreparePage());
            }

        }

        private void PrintPreviousPage()
        {
            if (_currentPage > 0)
            {
                _currentPage = _currentPage == 0 ? 0 : --_currentPage;
                Console.Write(PreparePage());
            }

        }

        private void PrintFirstPage()
        {
            _currentPage = 0;
            Console.Write(PreparePage());

        }

        private void PrintLastPage()
        {
            _currentPage = (int)Math.Floor((decimal)_collectionSize / PageSize);
            Console.Write(PreparePage());

        }


        private string PreparePage(int increment = 1)
        {
            Console.Clear();
            StringBuilder page =new StringBuilder();
            page.AppendLine(PrepareAnalyzeInfo());
            var end = (_currentPage)* PageSize + PageSize;
            for (int i = _currentPage*PageSize; i <end; i++)
            {
                if (i > _collectionSize - 1 || i < 0)
                {
                    page.AppendLine("End of file");
                    break;
                }
                page.Append($"{i+1}: ");
                page.AppendLine(Collection.ElementAt(i).ToString());
            }
            return page.ToString();
        }

        private string PrepareAnalyzeInfo()
        {
            return $"Executed {AnalyzedRows} lines in {ExecutionTime} seconds. Rows matched {_collectionSize}. \r\n Use 'n', 'p', 'u', 'b' to navigete through result or 'q' to finish";
        }
    }
}
