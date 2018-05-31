using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Formatting = Newtonsoft.Json.Formatting;

namespace IpAddressAnalyzer
{
    public class DataSerializer : IDataSerializer
    {
        public DataSerializer(string xmlPath, string jsonPAth, ICollection<Person> people)
        {
            Directory.CreateDirectory(@"c:\analyzer");
            XmlPath = xmlPath;
            JsonPAth = jsonPAth;
            People = people;
        }

        public string XmlPath { get; set; }
        public string JsonPAth { get; set; }
        public ICollection<Person> People { get; set; }

        public void SerializeToJson()
        {
            using (FileStream fs = File.Create(JsonPAth))
            using (StreamWriter sw = new StreamWriter(fs))
            using (JsonWriter jw = new JsonTextWriter(sw))
            {
                jw.Formatting = Formatting.Indented;

                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(jw, People);
            }
        }
        public void SerializeToXml()
        {
            var settings = new XmlWriterSettings();
            settings.Indent = true;
            
            using (FileStream fs = File.Create(XmlPath))
            using (StreamWriter sw = new StreamWriter(fs))
            using (XmlWriter xw = XmlTextWriter.Create(sw, settings))
            {
                
                XmlSerializer xml = new XmlSerializer(typeof(List<Person>));
                xml.Serialize(xw, People);
            }
        }
    }
}