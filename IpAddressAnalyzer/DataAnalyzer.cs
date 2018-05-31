using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using CsvHelper;
using CsvHelper.Configuration;

namespace IpAddressAnalyzer
{
    public class DataAnalyzer : IDataAnalyzer
    {
        public delegate void ProgressHandler(int currentLine, EventArgs e, DataAnalyzer d);

        private readonly string _filePath;

        private int _currentLine;
        private readonly EventArgs e = null;

        public DataAnalyzer(string path)
        {
            People = new List<Person>();
            _filePath = path;
        }

        public List<Person> People { get; set; }
        public bool Continue { get; set; } = true;
        public int FileSize { get; private set; }
        public double ExecutionTime { get; set; }

        public int CurrentLine
        {
            get => _currentLine;
            private set
            {
                _currentLine = value;
                Progress?.Invoke(CurrentLine, e, this);
            }
        }

        public event ProgressHandler Progress;

        public void Analyze()
        {
            var fileInfo = new FileInfo(_filePath);
            if (fileInfo.Exists)
            {
                FileSize = File.ReadLines(_filePath).Count();
                using (var reader = new StreamReader(_filePath))
                {
                    using (var csv = new CsvReader(reader))
                    {
                        var stopwatch = Stopwatch.StartNew();
                        StartAnalyze(csv);
                        var time = stopwatch.Elapsed;
                        ExecutionTime = time.TotalSeconds;
                    }
                }
            }
            else
            {
                throw new FileNotFoundException("File not exist");
            }
        }

        private void StartAnalyze(CsvReader csv)
        {
            csv.Configuration.RegisterClassMap(typeof(PersonMap));
            csv.Configuration.HasHeaderRecord = false;
            var r = new Regex("(\\.10\\W)");
            CurrentLine = 0;
            while (csv.Read())
            {
                CurrentLine++;
                var value = csv.GetField(4);
                if (r.IsMatch(value))
                {
                    var person = csv.GetRecord<Person>();
                    People.Add(person);
                }
                if (!Continue)
                    break;
            }
        }
    }

    public sealed class PersonMap : ClassMap<Person>
    {
        public PersonMap()
        {
            Map(p => p.Name).Index(0);
            Map(p => p.Surname).Index(1);
            Map(p => p.Email).Index(2);
            Map(p => p.Country).Index(3);
            Map(p => p.IpAddress).Index(4);
        }
    }
}