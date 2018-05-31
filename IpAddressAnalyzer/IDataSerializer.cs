using System.Collections.Generic;

namespace IpAddressAnalyzer
{
    public interface IDataSerializer
    {
        string JsonPAth { get; set; }
        ICollection<Person> People { get; set; }
        string XmlPath { get; set; }

        void SerializeToJson();
        void SerializeToXml();
    }
}